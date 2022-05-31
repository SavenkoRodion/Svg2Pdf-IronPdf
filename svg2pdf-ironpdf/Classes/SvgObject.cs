using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using System.Xml;
using System.IO;

namespace svg2pdf_ironpdf
{
    public class SvgObject : BaseConversion
    {
        public string SvgName { get; }
        public string SvgPath { get; }
        public string SvgPathNameLess { get; }
        public double[] SizeMM { get; set; } = new double[2];
        public double[] SizePX { get; set; } = new double[2];
        public double[] HTMLSizePX { get; set; } = new double[2];
        //public string ZamowienieName {get;}
        //public int ProductNumber { get; }

        public SvgObject(string svgPath) {
            SvgPath = svgPath;
            SvgName = GetFileName(SvgPath);
            // FIX THIS
            int nameindex = SvgPath.IndexOf(SvgName);
            SvgPathNameLess=SvgPath.Remove(nameindex, SvgName.Length);
            //ZamowienieName = zamowienieName;
            //ProductNumber = productNumber+1;
        }
        public string Test() {
            int index = SvgName.LastIndexOf('x');
            //bool isSvg = int.TryParse(GetFileName(fileName + "")[0] + "", out int uselessVar);
            Console.WriteLine(SvgName);
            if (index == -1)
            {
                Console.WriteLine(index);
            }
            else {
                Console.WriteLine(SvgName[index]);
            }
            
            
            return SvgName;
        }
        public void PrepareConversion() {
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(SvgPathNameLess+"conf.xml");

            // Conversion is a good idea?

            HTMLSizePX[0] = Convert.ToDouble(myDoc["produkt"]["szerokosc"].InnerText);
            HTMLSizePX[1] = Convert.ToDouble(myDoc["produkt"]["wysokosc"].InnerText);

            SizePX[0] = Convert.ToDouble(myDoc["produkt"]["szerokoscPDF"].InnerText);
            SizePX[1] = Convert.ToDouble(myDoc["produkt"]["wysokoscPDF"].InnerText);
            //Console.WriteLine(details);
            //ConvertToPdf();
        }
        public int CountDpi() {
            
            return 120;
        }
        public PdfDocument ConvertToPdf() {
            PrepareConversion();
            SizeMM[0] = 297;
            SizeMM[1] = 210;

            string temp = File.ReadAllText(SvgPath,Encoding.UTF8);
            var foundIndexes = new List<int>();
            string toFind = "xlink:href=\"";
            int loltemp = temp.IndexOf(toFind);
            if (loltemp != -1) {
                Console.WriteLine(temp[loltemp]);
            }
            Console.WriteLine();
            //SizePX[0] = 266.46;
            //SizePX[1] = 153.07;

            //HTMLSizePX[0] = 510;
            //HTMLSizePX[1] = 291.997;
            var Renderer = new IronPdf.ChromePdfRenderer();
            Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.Custom;
            //Renderer.RenderingOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;
            Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;
            Renderer.RenderingOptions.MarginTop = 0;
            Renderer.RenderingOptions.MarginBottom = 0;
            Renderer.RenderingOptions.MarginLeft = 0;
            Renderer.RenderingOptions.MarginRight = 0;
            //Renderer.RenderingOptions.DPI=25;
            Renderer.RenderingOptions.FitToPaper = true;
            Renderer.RenderingOptions.ViewPortHeight = 640;
            Renderer.RenderingOptions.ViewPortWidth = 480;
            //PdfDocument pdf2 = Renderer.RenderHTMLFileAsPdf(@"C:\Users\Saven\Desktop\PDFTests\htmltmp.html");
            //pdf2.SaveAs(@"C:\Users\Saven\Desktop\PDFTests\out.pdf");
            /*            var Renderer = new IronPdf.ChromePdfRenderer();
                        Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.Custom;
                        //Renderer.RenderingOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;
                        Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;
                        Renderer.RenderingOptions.MarginTop = 0;
                        Renderer.RenderingOptions.MarginBottom = 0;
                        Renderer.RenderingOptions.MarginLeft = 0;
                        Renderer.RenderingOptions.MarginRight = 0;
                        //Renderer.RenderingOptions.DPI=300;
                        Renderer.RenderingOptions.ViewPortHeight = 1920;
                        Renderer.RenderingOptions.ViewPortWidth = 1080;
                        //Renderer.RenderingOptions.SetCustomPaperSizeinMilimeters(216, 303);
                        Renderer.RenderingOptions.FitToPaperWidth = true;*/
            Console.WriteLine(SizePX[0]+" , "+ SizePX[1]);
            Console.WriteLine(HTMLSizePX[0]+" , "+ HTMLSizePX[1]);
            Console.WriteLine(SvgPath);
            //Renderer.RenderingOptions.SetCustomPaperSizeinMilimeters(94, 54);
            Renderer.RenderingOptions.SetCustomPaperSizeinPixelsOrPoints(SizePX[0],SizePX[1], 72);
            //Renderer.RenderingOptions.SetCustomPaperSizeinPixelsOrPoints(250, 250, 72);
            PdfDocument pdf = Renderer.RenderHtmlAsPdf("<body style='margin:0;padding:0;'><img style='width: " + HTMLSizePX[0] + "; height:" + HTMLSizePX[1] + "' src='" + SvgPath + "'/></div>"); //.SaveAs(AppFolderPath+"\\z_out\\"+ ZamowienieName +"\\"+ ProductNumber+"\\"+ SvgName.Substring(0,SvgName.Length - 4) + ".pdf");
            //PdfDocument pdf = Renderer.RenderHtmlAsPdf("<html><head><meta charset='UTF-8' /></head><body style='margin:0;padding:0;'>" + temp+ "</body></html>");
            //PdfDocument pdf = Renderer.RenderHtmlAsPdf("<body style='margin:0; padding:0;'><div style='width:500px; height: 500px; border:1px solid black; background-color: grey; display:inline-block'></div><div style='width:500px; height: 500px; border:1px solid black; background-color: grey; display:inline-block'></div><div style='width:500px; height: 500px; border:1px solid black; background-color: grey; display:inline-block'></div></body>");
            return pdf;
        }
    }
}
// obsluga wyjatkow
// consolelog
// xml - xml reader

// sciezki na bezwzgledne
// matrix w conf.xml zmienic na jedynki, przedzielic wysokosc/wysokoscPDF


//Znalezc odpowiednie wymiare z illustratora zeby 
//