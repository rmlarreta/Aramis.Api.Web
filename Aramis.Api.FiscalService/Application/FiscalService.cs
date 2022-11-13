using AfipServiceReference;
using AfipWsfeClient;
using Aramis.Api.Commons.ModelsDto.Fiscales;
using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.FiscalService.Interfaces;
using Aramis.Api.Repository.Interfaces;
using Aramis.Api.Repository.Interfaces.Operaciones;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FiscalService.Application
{
    public class FiscalService : IFiscalService
    {
        private readonly IOperacionesRepository _repository;
        private readonly IGenericRepository<SystemEmpresa> _empresa;
        private readonly IMapper _mapper;
        public FiscalService(IOperacionesRepository repository, IGenericRepository<SystemEmpresa> empresa, IMapper mapper)
        {
            _repository = repository;
            _empresa = empresa;
            _mapper = mapper;
        }
        public async Task<BusOperacionesDto> GenerarFactura(List<BusDetallesOperacionesDto> busDetalles)
        {
            foreach (IGrouping<Guid, BusDetallesOperacionesDto>? docs in busDetalles.GroupBy(x => x.OperacionId))
            {
                if (!_repository.Get(docs.Key.ToString()!).Estado.Name.Equals("ENTREGADO"))
                {
                    throw new Exception($"El documento {docs} no puede ser Facturado en el estado actual");
                }
            }

            BusOperacion? firstOp = _repository.Get(busDetalles.First().OperacionId.ToString());

            DocumentoFiscal documentoFiscal = new();
            documentoFiscal.TipoComprobante = firstOp.Cliente.RespNavigation.Name!.Equals("MONOTRIBUTO") || firstOp.Cliente.RespNavigation.Name.Equals("RESPONSABLE INSCRIPTO") ? 1 : 6;
            documentoFiscal.TipoDocumento = firstOp.Cliente.Cui!.Equals("0") ? 99 : 80;
            documentoFiscal.DocumentoCliente = Convert.ToInt64(firstOp.Cliente.Cui);
            documentoFiscal.Exento = (decimal)busDetalles.Sum(x => x.TotalExento)!;
            documentoFiscal.Neto = (decimal)busDetalles.Sum(x => x.TotalNeto)!;
            documentoFiscal.Internos = (decimal)busDetalles.Sum(x => x.TotalInternos)!;
            documentoFiscal.Neto10 = (decimal)busDetalles.Sum(x => x.TotalNeto10)!;
            documentoFiscal.Iva10 = (decimal)busDetalles.Sum(x => x.TotalIva10)!;
            documentoFiscal.Neto21 = (decimal)busDetalles.Sum(x => x.TotalNeto21)!;
            documentoFiscal.Iva21 = (decimal)busDetalles.Sum(x => x.TotalIva21)!;
            documentoFiscal.IvaTotal = (decimal)busDetalles.Sum(x => x.TotalIva)!;
            documentoFiscal.TotalComprobante = (decimal)busDetalles.Sum(x => x.Total)!;
            List<AlicIva> alicIvas = new();

            if (documentoFiscal.Iva10 > 0)
            {
                alicIvas.Add(new() { Id = 4, BaseImp = (double)documentoFiscal.Neto10, Importe = (double)documentoFiscal.Iva10 });
            }

            if (documentoFiscal.Iva21 > 0)
            {
                alicIvas.Add(new() { Id = 5, BaseImp = (double)documentoFiscal.Neto21, Importe = (double)documentoFiscal.Iva21 });
            }
            documentoFiscal.Alicuotas = alicIvas;

            if (documentoFiscal.Internos > 0)
            {
                List<Tributo> tributo = new();
                tributo.Add(new() { Id = 4, BaseImp = (double)documentoFiscal.TotalComprobante, Importe = (double)documentoFiscal.Internos });
                documentoFiscal.Tributo = tributo;
            }

            FECAESolicitarResponse? cae = await GetCae(documentoFiscal);
            if (cae == null)
            {
                throw new Exception($"No hay comunicación con el Servidor AFIP");
            }

            if (cae.Body.FECAESolicitarResult.Errors != null)
            {
                throw new Exception($"No se obtuvo aprobación desde AFIP. {cae.Body.FECAESolicitarResult.Errors.First().Msg}");
            }

            if (cae.Body.FECAESolicitarResult.FeCabResp.Resultado != "A")
            {
                throw new Exception($"No se obtuvo aprobación desde AFIP. Verifique los errores");
            }

            if (cae.Body.FECAESolicitarResult.FeCabResp.Resultado == "A")
            {
                List<BusOperacionDetalle> actualizarFacturados = new();
                foreach (BusDetallesOperacionesDto? det in busDetalles) //trer los detalles originales
                {
                    BusOperacionDetalle? data = _repository.Get(det.OperacionId.ToString()).BusOperacionDetalles!.Where(x => x.Id.Equals(det.Id)).FirstOrDefault();
                    if (data != null)
                    {
                        data.Facturado += det.Cantidad;
                        actualizarFacturados.Add(data);
                    }
                }

                _repository.UpdateDetalles(actualizarFacturados);
                List<BusDetalleOperacionesInsert> detallesFiscales = new();
                BusOperacion factura = new();
                factura.Id = Guid.NewGuid();
                foreach (BusDetallesOperacionesDto? d in busDetalles)
                {
                    detallesFiscales.Add(new BusDetalleOperacionesInsert
                    {
                        Id = Guid.NewGuid(),
                        OperacionId = factura.Id,
                        Facturado = 0,
                        Codigo = d.Codigo,
                        Detalle = d.Detalle,
                        Cantidad = d.Cantidad,
                        Internos = d.Internos,
                        IvaValue = d.IvaValue,
                        ProductoId = d.ProductoId,
                        Rubro = d.Rubro,
                        Unitario = d.Unitario
                    });
                }
                _repository.InsertDetalles(_mapper.Map<List<BusDetalleOperacionesInsert>, List<BusOperacionDetalle>>(detallesFiscales));
                factura.TipoDocId = _repository.GetTipos().Where(x => x.TipoAfip.Equals(documentoFiscal.TipoComprobante)).First().Id;
                factura.Pos = cae.Body.FECAESolicitarResult.FeCabResp.PtoVta;
                factura.Numero = (int)cae.Body.FECAESolicitarResult.FeDetResp.First().CbteDesde;
                factura.ClienteId = firstOp.ClienteId;
                factura.Fecha = DateTime.Now;
                factura.Vence = DateTime.Now;
                factura.Razon = firstOp.Razon;
                factura.Operador = firstOp.Operador;
                factura.EstadoId = firstOp.EstadoId;
                factura.CodAut = cae.Body.FECAESolicitarResult.FeDetResp.First().CAE;
                _repository.Insert(factura);
                List<BusObservacionesDto> busObservaciones = new();
                foreach (IGrouping<Guid, BusDetallesOperacionesDto>? docs in busDetalles.GroupBy(x => x.OperacionId))
                {
                    busObservaciones.Add(new()
                    {
                        Fecha = DateTime.Now,
                        Operador = firstOp.Operador,
                        OperacionId = factura.Id,
                        Id = Guid.NewGuid(),
                        Observacion = $"Remito {_repository.Get(docs.Key.ToString()!).Numero}"
                    });
                }
                _repository.InsertObservaciones(_mapper.Map<List<BusObservacionesDto>, List<BusOperacionObservacion>>(busObservaciones));
                _repository.Save();
                BusOperacionesDto? dto = _mapper.Map<BusOperacion, BusOperacionesDto>(_repository.Get(factura.Id.ToString()));
                IEnumerable<SystemEmpresa>? emp = _empresa.Get().Take(1);
                dto.CuitEmpresa = emp.OrderBy(x => x.Id).First().Cuit;
                dto.DomicilioEmpresa = emp.OrderBy(x => x.Id).First().Domicilio;
                dto.RazonEmpresa = emp.OrderBy(x => x.Id).First().Razon;
                dto.RespoEmpresa = emp.OrderBy(x => x.Id).First().Respo;
                dto.Fantasia = emp.OrderBy(x => x.Id).First().Fantasia;
                dto.Iibb = emp.OrderBy(x => x.Id).First().Iibb;
                dto.Inicio = emp.OrderBy(x => x.Id).First().Inicio;
                return dto;
            }
            return null!;
        }

        private async Task<FECAESolicitarResponse> GetCae(DocumentoFiscal documento)
        {
            try
            {
                #region Get Login Ticket
                //Get Login Ticket
                LoginCmsClient? loginClient = new() { IsProdEnvironment = _repository.GetIndexs().Production };
                WsaaTicket? ticket = await loginClient.LoginCmsAsync("wsfe",
                                                             "Certificados/certificado.p12",
                                                             "1234",
                                                             true);
                #endregion 
                #region wsfeClient

                WsfeClient? wsfeClient = new()
                {
                    IsProdEnvironment = _repository.GetIndexs().Production,
                    Cuit = long.Parse(_empresa.Get().First().Cuit.Replace("-", "")),
                    Sign = ticket.Sign,
                    Token = ticket.Token
                };
                int compNumber = wsfeClient.FECompUltimoAutorizadoAsync(_empresa.Get().First().PtoVenta, documento.TipoComprobante)
                    .Result.Body.FECompUltimoAutorizadoResult.CbteNro + 1;

                #endregion

                #region Build WSFE Request
                //Build WSFE FECAERequest          
                FECAERequest? feCaeReq = new()
                {
                    FeCabReq = new FECAECabRequest { CantReg = 1, CbteTipo = documento.TipoComprobante, PtoVta = _empresa.Get().First().PtoVenta },
                    FeDetReq = new List<FECAEDetRequest> { new FECAEDetRequest { CbteDesde = compNumber, CbteHasta = compNumber, CbteFch = DateTime.Now.ToString("yyyyMMdd"), Concepto = 3, DocNro = documento.DocumentoCliente, DocTipo = documento.TipoDocumento, FchVtoPago = DateTime.Now.ToString("yyyyMMdd"), ImpNeto = (double)documento.Neto, ImpTotal = (double)documento.TotalComprobante, ImpIVA = (double)documento.IvaTotal, ImpOpEx = (double)documento.Exento, ImpTrib = (double)documento.Internos, FchServDesde = DateTime.Now.ToString("yyyyMMdd"), FchServHasta = DateTime.Now.ToString("yyyyMMdd"), Tributos = documento.Tributo, MonCotiz = 1, MonId = "PES", Iva = documento.Alicuotas } }
                };

                #endregion

                //Call WSFE FECAESolicitar
                FECAESolicitarResponse? compResult = await wsfeClient.FECAESolicitarAsync(feCaeReq);
                return compResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
