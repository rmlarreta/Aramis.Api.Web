using Aramis.Api.Commons.ModelsDto.Operaciones;
using Aramis.Api.Commons.ModelsDto.Reports;
using Aramis.Api.ReportService.Interfaces;
using Aramis.Api.Repository.Interfaces.Reports;
using Aramis.Api.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Text.Json;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Aramis.Api.ReportService.Application
{
    public class OperacionsReports : IOperacionsReports
    {
        private readonly IReportsRepository _repository;
        private readonly IMapper _mapper;

        public OperacionsReports(IReportsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public FileStreamResult FacturaReport(string id)
        {

            if (_repository.Operacions.Get(id).TipoDoc.TipoAfip.Equals(0)) throw new Exception("Operación no permitida");
            BusOperacionesDto? dto = _mapper.Map<BusOperacion, BusOperacionesDto>(_repository.Operacions.Get(id));
            if (dto == null)
            {
                return null!;
            }
            var empresa = _repository.Empresa.Get().First();
            dto.CuitEmpresa = empresa.Cuit;
            dto.DomicilioEmpresa = empresa.Domicilio;
            dto.RazonEmpresa = empresa.Razon;
            dto.Inicio = empresa.Inicio;
            dto.RespoEmpresa = empresa.Respo;
            dto.Fantasia = empresa.Fantasia;

            MemoryStream? stream = new();
            QrJson? QrJsonModel = new()
            {
                CodAut = dto.CodAut,
                Ctz = 1,
                Cuit = _repository.Empresa.Get().OrderBy(x => x.Cuit).FirstOrDefault()!.Cuit.Replace("-", ""),
                Fecha = dto.Fecha.ToString("yyyy-MM-dd"),
                Importe = dto.Total,
                Moneda = "PES",
                NroCmp = (int)dto.Numero!,
                NroDocRec = long.Parse(dto.Cui!),
                PtoVenta = dto.Pos,
                TipoCmp = (int)_repository.Operacions.Get(id).TipoDoc.TipoAfip!,
                TipoCodAut = "E",
                TipoDocRec = dto.Cui == "0" ? 99 : 86,
                Ver = 1,
                QrData = "https://www.afip.gob.ar/fe/qr/?p="
            };
            var qr = QrJson(QrJsonModel);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    //seteamos la página
                    page.Size(PageSizes.A4);
                    page.Margin(5, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));
                    page.DefaultTextStyle(x => x.FontFamily("Arial"));

                    //seteamos el encabezado 
                    page.Header()
                    .Column(column =>
                    {
                        column.Item().Height(35, Unit.Millimetre).Row(row =>
                        {
                            row.RelativeItem(1).Image("Images/logo.jpg");

                            row.RelativeItem(1).DefaultTextStyle(x => x.FontSize(12))
                                    .DefaultTextStyle(f => f.Bold())
                                    .AlignCenter()
                                    .Text(text =>
                             {
                                 text.Span("Página ");
                                 text.CurrentPageNumber();
                                 text.Span(" de ");
                                 text.TotalPages();
                             });

                            row.RelativeItem(1).Border(1, Unit.Point)
                                   .Grid(grid =>
                            {
                                grid.Columns(1);
                                grid.Item()
                            .DefaultTextStyle(x => x.FontSize(40))
                            .DefaultTextStyle(f => f.Bold())
                            .AlignCenter()
                            .PaddingVertical(1, Unit.Millimetre)
                            .Text(text =>
                            {
                                text.Span(_repository.Operacions.Get(id).TipoDoc.Code);
                            });
                                grid.Item()
                               .DefaultTextStyle(x => x.FontSize(10))
                               .AlignCenter()
                               .PaddingBottom(3, Unit.Millimetre)
                               .Text(text =>
                               {
                                   text.Span(_repository.Operacions.Get(id).TipoDoc.CodeExt);
                               });
                            });

                            row.Spacing(20);
                            row.RelativeItem(2)
                                    .Border(1, Unit.Point)
                                    .Padding(5, Unit.Millimetre)
                                    .DefaultTextStyle(x => x.FontSize(12))
                                    .DefaultTextStyle(f => f.Bold())
                                    .Grid(grid =>
                             {
                                 grid.Columns(1);
                                 grid.Item()
                                  .Text(text =>
                                  {
                                      text.AlignCenter();
                                      text.Span(_repository.Operacions.Get(id).TipoDoc.Name);
                                  });
                                 grid.Item()
                                  .Text(text =>
                                  {
                                      text.AlignRight();
                                      text.Span("\n FC: " + dto.Pos.ToString().PadLeft(4, '0') + " - " + dto.Numero.ToString().PadLeft(8, '0'));
                                  });
                                 grid.Item()
                                 .Text(text =>
                                 {
                                     text.DefaultTextStyle(f => f.NormalWeight());
                                     text.AlignRight();
                                     text.Span("FECHA: " + dto.Fecha.ToShortDateString());
                                 });
                             });

                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1)
                            .PaddingVertical(3, Unit.Millimetre)
                            .DefaultTextStyle(f => f.FontSize(10))
                            .DefaultTextStyle(t => t.FontColor("#1f66ff"))
                            .Text("Razón Social: " + dto.RazonEmpresa
                            + "\n" + dto.Fantasia
                            + "\n" + dto.DomicilioEmpresa
                            + "\n" + dto.RespoEmpresa
                            );

                            row.RelativeItem(1)
                           .PaddingVertical(3, Unit.Millimetre)
                           .DefaultTextStyle(f => f.FontSize(10))
                           .DefaultTextStyle(t => t.FontColor("#1f66ff"))
                           .Text("Cuit: " + dto.CuitEmpresa
                           + "\n" + "IIBB: " + dto.Iibb
                           + "\n" + "I. Actividades: " + dto.Inicio.ToShortDateString()
                           );
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeItem(1)
                           .DefaultTextStyle(f => f.FontSize(10))
                           .BorderTop(1, Unit.Millimetre).BorderColor("#858796")
                           .PaddingVertical(2, Unit.Millimetre)
                           .DefaultTextStyle(t => t.SemiBold())
                           .Text("Cliente: " + dto.Razon
                           + "\n" + dto.Domicilio
                           );

                            row.RelativeItem(1)
                            .DefaultTextStyle(f => f.FontSize(10))
                            .BorderTop(1, Unit.Millimetre).BorderColor("#858796")
                            .PaddingVertical(2, Unit.Millimetre)
                            .DefaultTextStyle(t => t.SemiBold())
                            .Text("Cuit: " + dto.Cui
                            + "\n" + "RESPONSABLE " + dto.Resp
                            );
                        });
                    });

                    //setemaos los detalles
                    page.Content()
                        .PaddingVertical(1, Unit.Millimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(10);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                            });
                            table.Cell().ColumnSpan(1)
                           .Background("#9ca4df")
                           .Text("Código");
                            table.Cell().ColumnSpan(1)
                             .Background("#9ca4df")
                           .Text("Detalle");
                            table.Cell().ColumnSpan(1)
                             .Background("#9ca4df")
                           .Text("Cantidad");
                            table.Cell().ColumnSpan(1)
                             .Background("#9ca4df")
                           .Text("Unitario");
                            table.Cell().ColumnSpan(1)
                             .Background("#9ca4df")
                           .Text("Iva");
                            table.Cell().ColumnSpan(1)
                             .Background("#9ca4df")
                           .Text("Sub Total");

                            foreach (BusDetallesOperacionesDto? c in dto.Detalles!)
                            {
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(c.Codigo);
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignLeft().Text(c.Detalle);
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(c.Cantidad);
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text("$ " + c.Unitario);
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text("% " + c.IvaValue);
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignRight().Text("$ " + Math.Round((decimal)c.Total!, 2));
                            }
                            table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                            table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text("OBSERVACIONES");
                            table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                            table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                            table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                            table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                            foreach (var o in dto.Observaciones)
                            {
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text("*");
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignLeft().Text(o.Observacion);
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                                table.Cell().Padding(0).DefaultTextStyle(x => x.FontSize(8)).DefaultTextStyle(x => x.NormalWeight()).AlignCenter().Text(" ");
                            }
                        });
                    //seteamos los footer                            
                    page.Footer()
                       .BorderTop(1, Unit.Point)
                       .Column(c =>
                       {
                           c.Item().BorderTop(1, Unit.Point);
                           c.Item().Row(r =>
                           {
                               r.ConstantItem(100)
                               .Image(qr, ImageScaling.FitArea);
                               r.RelativeItem()
                             .DefaultTextStyle(t => t.FontSize(10))
                             .DefaultTextStyle(x => x.Bold())
                             .Text("Neto Gravado: $ " + Math.Round((decimal)dto.TotalNeto!, 2)
                             + "\n" + "Excento: $ " + Math.Round((decimal)dto.TotalExento!, 2)
                             + "\n" + "IVA 10.5%: $ " + Math.Round((decimal)dto.TotalIva10!, 2)
                             + "\n" + "IVA 21.0%: $ " + Math.Round((decimal)dto.TotalIva21!, 2)
                             + "\n" + "Imp.Internos: $ " + Math.Round((decimal)dto.TotalInternos!, 2)
                             + "\n" + "\n" + "Ud. fue atendido por " + dto.Operador);
                               r.RelativeItem(2)
                              .DefaultTextStyle(t => t.FontSize(12))
                              .DefaultTextStyle(x => x.Bold())
                              .AlignRight()
                              .Text(text =>
                              {
                                  text.Line("TOTAL $: " + Math.Round((decimal)dto.Total!, 2));
                                  text.Line(dto.TotalLetras!).FontSize(8);
                                  text.Line("");
                                  text.Line("");
                                  text.Line("CAE Nº: " + dto.CodAut!).FontSize(8);
                                  text.Line("Fecha de Vto de CAE: " + dto.Vence.ToShortDateString()).FontSize(8);
                              });
                           });
                           c.Item().BorderTop(1, Unit.Point);
                           c.Item()
                           .Row(r =>
                           {
                               r.RelativeItem()
                              .DefaultTextStyle(t => t.FontSize(10))
                              .DefaultTextStyle(x => x.Bold())
                              .AlignLeft()
                              .Text(text =>
                              {
                                  text.Span("2022 © Desarrollado por Aramis Sistemas");
                              });
                               r.RelativeItem()
                            .DefaultTextStyle(t => t.FontSize(10))
                            .DefaultTextStyle(x => x.Bold())
                            .AlignLeft()
                            .Text(text =>
                            {
                                text.Span("Página ");
                                text.CurrentPageNumber();
                                text.Span(" de ");
                                text.TotalPages();
                            });
                           });
                       });
                });
            })
              .GeneratePdf(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        private static byte[] QrJson(QrJson? QrJsonModel)
        {

            string? QrString = JsonSerializer.Serialize(QrJsonModel);
            byte[]? plainTextBytes = System.Text.Encoding.UTF8.GetBytes(QrString);
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrJsonModel!.QrData + Convert.ToBase64String(plainTextBytes), QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
            return qrCodeAsBitmapByteArr;
        }
    }
}
