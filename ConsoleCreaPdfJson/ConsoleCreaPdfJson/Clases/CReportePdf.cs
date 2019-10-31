using ConsoleCreaPdfJson.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using ConsoleCreaPdfJson.Enumeradores;

namespace ConsoleCreaPdfJson.Clases
{
    class CReportePdf : IReporte
    {
        public void CrearReportePdf(EnumOrientacionHoja orientacion, string rutaCreacion, string titulo, DataTable dtDatos)
        {
            //Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(orientacion == EnumOrientacionHoja.vertical ? PageSize.LETTER : PageSize.LETTER.Rotate());
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaCreacion, FileMode.Create));
            writer.PageEvent = new PageEventHelper();
            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _tituloBolddFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font _bolddFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _saltoDeLineaFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 4, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            var parrafoTitulo = new Paragraph(titulo, _tituloBolddFont);
            parrafoTitulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(parrafoTitulo);

            var parrafoFecha = new Paragraph(DateTime.Now.ToString("dd-MMMM-yyyy", CultureInfo.CreateSpecificCulture("es-MX")), _bolddFont);
            parrafoFecha.Alignment = Element.ALIGN_RIGHT;
            doc.Add(parrafoFecha);
            doc.Add(new Paragraph(" ",_saltoDeLineaFont));

            // Creamos una tabla que contendrá el nombre, apellido y país
            // de nuestros visitante.
            PdfPTable tabla = new PdfPTable(dtDatos.Columns.Count);
            tabla.WidthPercentage = 100;
            tabla.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            foreach (DataColumn columna in dtDatos.Columns)
            {
                PdfPCell nombreColumna = new PdfPCell(new Phrase(columna.ColumnName, _bolddFont));
                nombreColumna.HorizontalAlignment = Element.ALIGN_CENTER;
                nombreColumna.VerticalAlignment = Element.ALIGN_MIDDLE;
                tabla.AddCell(nombreColumna);
            }

            //mostrar avance
            double porcentajeAvance =  Math.Round(100d / dtDatos.Rows.Count,2);
            double avanceTotal = 0;

            Console.Write("Avance: {0}%", avanceTotal);

            foreach (DataRow registro in dtDatos.Rows)
            {
                avanceTotal = Math.Round(avanceTotal + porcentajeAvance,2);
                foreach (var item in registro.ItemArray)
                {
                    // Llenamos la tabla con información
                    PdfPCell datoRegistro = new PdfPCell(new Phrase(item.ToString(), _standardFont));
                    datoRegistro.HorizontalAlignment = Element.ALIGN_CENTER;
                    datoRegistro.VerticalAlignment = Element.ALIGN_MIDDLE;
                    datoRegistro.UseAscender = true;
                    // Añadimos las celdas a la tabla
                    tabla.AddCell(datoRegistro);
                }
                Console.SetCursorPosition(8, Console.CursorTop);
                Console.Write("{0}%", avanceTotal);
            }
            Console.SetCursorPosition(8, Console.CursorTop);
            Console.WriteLine("100%   ");
            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            Console.WriteLine("Guardando en archivo pdf en el ruta: [{0}]",Path.GetDirectoryName(rutaCreacion));
            doc.Add(tabla);
            doc.Add(Chunk.NEWLINE);

            doc.Close();
            writer.Close();
        }

        public void prueba()
        {
            //Output file
            string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Table.pdf");

            using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (Document doc = new Document(PageSize.LETTER))
                {
                    using (PdfWriter w = PdfWriter.GetInstance(doc, fs))
                    {
                        doc.Open();

                        doc.NewPage();

                        //Create some long text to force a new page
                        string longText = String.Concat(Enumerable.Repeat("Lorem ipsum.", 40));

                        //Create our table using both THEAD and TH which iTextSharp currently ignores
                        string html = "<table>";
                        html += "<thead><tr><th>Header Row 1/Cell 1</th><th>Header Row 1/Cell 2</th></tr><tr><th>Header Row 2/Cell 1</th><th>Header Row 2/Cell 2</th></tr></thead>";
                        html += "<tbody>";

                        for (int i = 3; i < 200; i++)
                        {
                            html += "<tr>";
                            html += String.Format("<td>Data Row {0}</td>", i);
                            html += String.Format("<td>{0}</td>", longText);
                            html += "</tr>";
                        }
                        html += "</tbody>";
                        html += "</table>";

                        using (StringReader sr = new StringReader(html))
                        {
                            //Get our list of elements (only 1 in this case)
                            List<IElement> elements = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(sr, null);
                            foreach (IElement el in elements)
                            {
                                //If the element is a table manually set its header row count
                                if (el is PdfPTable)
                                {
                                    ((PdfPTable)el).HeaderRows = 2;
                                }
                                doc.Add(el);
                            }
                        }
                        doc.Close();
                    }
                }
            }

        }
    }

    public class PageEventHelper : PdfPageEventHelper
    {
        PdfContentByte cb;
        PdfTemplate template;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            #region Titulo
            //iTextSharp.text.Font _bolddFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            ////tbl footer
            //PdfPTable headerTbl = new PdfPTable(1);
            //headerTbl.TotalWidth = doc.PageSize.Width;

            ////Titulo
            //Chunk myHeder = new Chunk("", _bolddFont);
            //PdfPCell header = new PdfPCell(new Phrase(myHeder));
            //header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //header.HorizontalAlignment = Element.ALIGN_CENTER;
            //headerTbl.AddCell(header);


            //headerTbl.WriteSelectedRows(0, -1, 0, doc.Top + doc.TopMargin - 10, writer.DirectContent);
            #endregion

            #region Número de página
            BaseColor grey = new BaseColor(128, 128, 128);
            iTextSharp.text.Font font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, grey);
            //tbl footer
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = doc.PageSize.Width;

            //numero de la página
            Chunk myFooter = new Chunk("Página " + (doc.PageNumber), FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, grey));
            PdfPCell footer = new PdfPCell(new Phrase(myFooter));
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.HorizontalAlignment = Element.ALIGN_CENTER;
            footerTbl.AddCell(footer);


            footerTbl.WriteSelectedRows(0, -1, 0, doc.BottomMargin, writer.DirectContent);
            #endregion
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
