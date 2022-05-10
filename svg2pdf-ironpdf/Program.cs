using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace svg2pdf_ironpdf
{
    class Program
    {
        static void Main(string[] args)
        {

            /*            int count = 86;
                        //автоматізація цифри
                        string way = @"C:/Users/Saven/Desktop/zamowienie_nr_15661_2022-04-05131017/1/1.ve15661_strona_Strona_1_1z2_90x50_4+4_kreda_mat_350g_200_k.svg";
                        //автоматізація назви
                        //получаю інфу про файли в фолдері
                        //путь+назва в форі
                        //
                        string way1 = "C:\\Users\\Saven\\source\\repos\\svg2pdf-ironpdf\\zamowienie_nr_14975_2022-01-31155734\\1\\1.14975_1_wiz_kreda_mat_350g_4+4_matowa_90x50_250.svg";
                        var Renderer = new IronPdf.ChromePdfRenderer();
                        Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.Custom;
                        Renderer.RenderingOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;
                        Renderer.RenderingOptions.MarginTop = 2;
                        Renderer.RenderingOptions.MarginBottom = 2;
                        Renderer.RenderingOptions.MarginLeft = 2;
                        Renderer.RenderingOptions.MarginRight = 2;
                        Renderer.RenderingOptions.SetCustomPaperSizeinMilimeters(94, 54);
                        //автоматізація міліметрів +4

                        Renderer.RenderHtmlAsPdf("<body style='margin:0;padding:0;'><img style='width: 340.2; height: 189' src='" + way + "'/></div>").SaveAs("C:\\Users\\Saven\\Desktop\\svg" + count + ".pdf");
                        //автоматізація ретурну*/

            Console.WriteLine("start");
            UserInterface.UserInteraction();
            // ПИТАННЯ: НАПОМИНАННЯ:
            Console.WriteLine("end");
        }

    }
}
