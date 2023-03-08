using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Pagos;
using Aramis.Api.FlowService.Interfaces;
using Aramis.Api.Repository.Interfaces.Pagos;
using Aramis.Api.Repository.Models;
using AutoMapper;

namespace Aramis.Api.FlowService.Application
{
    public class PagosService : IPagosService
    {
        private readonly IPagosRepository _repository;
        private readonly IMapper _mapper;

        public PagosService(IPagosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ConciliacionCliente> ConciliacionClienteAsync(string clienteId)
        {
            try
            {
                List<BusOperacion> op = await Task.FromResult(_repository.Operaciones.GetImpagasByClienteId(clienteId)); 
                List<CobReciboDetalle> dets = await Task.FromResult(_repository.Recibos.GetCuentaCorrientesByCliente(clienteId));
                List<CobRecibo> rec = await Task.FromResult(_repository.Recibos.GetSinImputarByCLiente(clienteId));
                ConciliacionCliente conciliacionCliente = new()
                {
                    OperacionesImpagas = _mapper.Map<List<BusOperacionesDto>>(op),
                    DetallesCuentaCorriente = _mapper.Map<List<CobReciboDetalleDto>>(dets),
                    RecibosSinImputar = _mapper.Map<List<CobReciboDto>>(rec)
                };
                return conciliacionCliente;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public bool ImputarRecibo(string reciboId)
        {
            IsImputado(reciboId);

            var reciboimputar = _repository.Recibos.Get(reciboId);
            decimal disponible = reciboimputar.CobReciboDetalles.Sum(x => x.Monto);

            List<BusOperacion>? consaldos = _repository.Operaciones.GetImpagasByClienteId(reciboimputar.ClienteId.ToString()).OrderBy(x => x.Fecha).ToList();
            if (!consaldos.Any()) throw new Exception("No hay documentos para imputar");

            foreach (BusOperacion consaldo in consaldos)
            {
                List<CobReciboDetalle>? detallescc = _repository.Recibos.GetCuentaCorrientesByCliente(consaldo.ClienteId.ToString());

                if (detallescc != null && disponible > 0)
                {
                    foreach (CobReciboDetalle detallecc in detallescc!)
                    {
                        if (disponible >= detallecc.Monto)
                        {
                            detallecc.Cancelado = true;
                            disponible -= detallecc.Monto;

                            CobCuentum? cuenta = _repository.Cuentas.Get().FirstOrDefault(x => x.Id.Equals(_repository.TipoPagos.Get().FirstOrDefault(x => x.Id.Equals(detallecc.Tipo))!.CuentaId));
                            if (cuenta is null)
                            {
                                throw new Exception("Existe un error en las cuentas");
                            }

                            cuenta.Saldo -= detallecc.Monto;
                            _repository.Cuentas.Update(cuenta);
                            _repository.Recibos.Update(detallecc);
                            BusOperacionPagoDto pagoDto = new()
                            {
                                Id = Guid.NewGuid(),
                                OperacionId = consaldo.Id,
                                ReciboId = Guid.Parse(reciboId)
                            };
                            _repository.OperacionPagos.Add(_mapper.Map<BusOperacionPago>(pagoDto));

                            if (disponible == 0)
                            { 
                                return _repository.Save();
                            }
                        }
                        else
                        { 
                            detallecc.Monto -=disponible; 
                            CobCuentum? cuenta = _repository.Cuentas.Get().FirstOrDefault(x => x.Id.Equals(_repository.TipoPagos.Get().FirstOrDefault(x => x.Id.Equals(detallecc.Tipo))!.CuentaId));
                            if (cuenta is null)
                            {
                                throw new Exception("Existe un error en las cuentas");
                            }

                            cuenta.Saldo -= disponible;
                            _repository.Cuentas.Update(cuenta); 
                             
                            CobReciboDetallesInsert detallecctoinsert = _mapper.Map<CobReciboDetallesInsert>(detallecc);
                            detallecctoinsert.Cancelado = true;
                            detallecctoinsert.Id = Guid.NewGuid();
                            detallecctoinsert.Monto = disponible;
                            disponible = 0;
                            _repository.Recibos.Update(detallecc);
                            _repository.Recibos.Add(_mapper.Map<CobReciboDetalle>(detallecctoinsert));
                            BusOperacionPagoDto pagoDto = new()
                            {
                                Id = Guid.NewGuid(),
                                OperacionId = consaldo.Id,
                                ReciboId = Guid.Parse(reciboId)
                            };
                            _repository.OperacionPagos.Add(_mapper.Map<BusOperacionPago>(pagoDto));
                            return _repository.Recibos.Save();
                        }
                    }
                }
            }

            if (disponible > 0)
            {
                CobReciboInsert reciboSaldo = new()
                {
                    ClienteId = reciboimputar.ClienteId,
                    Fecha = reciboimputar.Fecha,
                    Operador = reciboimputar.Operador,
                    Id = Guid.NewGuid()
                };

                List<CobReciboDetalle>? pendientes = reciboimputar.CobReciboDetalles!.OrderByDescending(x => x.Monto).ToList();
                foreach (CobReciboDetalle detPendiente in pendientes.ToList())
                {
                    if (disponible > detPendiente.Monto)
                    {
                        disponible -= detPendiente.Monto;
                    }
                    else
                    {
                        detPendiente.Monto -= disponible;
                        _repository.Recibos.Update(_mapper.Map<CobReciboDetalle>(detPendiente));
                        reciboSaldo.Detalles!.Add(new CobReciboDetallesInsert()
                        {
                            ReciboId = reciboSaldo.Id,
                            Id = Guid.NewGuid(),
                            Cancelado = true,
                            CodAut = detPendiente.CodAut,
                            Monto = disponible,
                            Observacion = detPendiente.Observacion,
                            PosId = detPendiente.PosId,
                            Tipo = detPendiente.Tipo
                        });
                    }
                }
                _repository.Recibos.Add(_mapper.Map<CobRecibo>(reciboSaldo));
                return _repository.Save();
            }
            return true;
        }

        public async Task<bool> NuevoPago(PagoInsert pago)
        {
            ValidReciboNotCero(pago.ReciboId.ToString());

            IsImputado(pago.ReciboId.ToString());

            ValidReciboNotEqualPagos(pago.ReciboId.ToString(), pago.Operaciones);

            _repository.Operaciones.PagarOperaciones(pago.Operaciones);

            foreach (var item in pago.Operaciones)
            {
                BusOperacionPagoDto op = new()
                {
                    Id = Guid.NewGuid(),
                    OperacionId = Guid.Parse(item),
                    ReciboId = pago.ReciboId
                };
                _repository.OperacionPagos.Add(_mapper.Map<BusOperacionPago>(op));
            }
            return await Task.FromResult(_repository.Save());
        }

        private void ValidReciboNotEqualPagos(string reciboId, List<string> ops)
        {
            List<BusOperacionesDto> operaciones = _mapper.Map<List<BusOperacionesDto>>(_repository.Operaciones.Get(ops));

            if (!_repository.Recibos.Get(reciboId).CobReciboDetalles.Sum(x => Math.Round(x.Monto, 2)).Equals(operaciones.Sum(x => Math.Round(x.Total, 2))))
            {
                throw new Exception("Existe una diferencia en los pagos ingresados");
            }

        }

        private void ValidReciboNotCero(string reciboId)
        {
            if (_repository.Recibos.Get(reciboId).CobReciboDetalles.Sum(x => x.Monto) == 0)
            {
                throw new Exception("No puede insertarse un Recibo en 0");
            }
        }
        private void IsImputado(string reciboId)
        {
            if (_repository.OperacionPagos.Get().Where(x => x.ReciboId.Equals(reciboId)).Any())
            {
                throw new Exception("Ese Recibo ya ha sido imputado");
            }
        }
    }
}
