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
        public string SvgPathNameless { get; }
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
            SvgPathNameless=SvgPath.Remove(nameindex, SvgName.Length);
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
            myDoc.Load(SvgPathNameless+"conf.xml");

            // Conversion is a good idea?

            HTMLSizePX[0] = Convert.ToDouble(myDoc["produkt"]["szerokosc"].InnerText);
            HTMLSizePX[1] = Convert.ToDouble(myDoc["produkt"]["wysokosc"].InnerText);

            SizePX[0] = Convert.ToDouble(myDoc["produkt"]["szerokoscPDF"].InnerText);
            SizePX[1] = Convert.ToDouble(myDoc["produkt"]["wysokoscPDF"].InnerText);
            //Console.WriteLine(details);
            //ConvertToPdf();
        }
        
        public void GetSvgWithAbsolutePaths() {
            string temp = File.ReadAllText(SvgPath, Encoding.UTF8);
            string toFind = "xlink:href=\"d";
            //int index = 0;
            List<int> indexes = temp.AllIndexesOf(toFind);
            for (int i = 0; i<indexes.Count;i++) {
                temp = temp.Insert(indexes[i] + 12+ (SvgPathNameless.Length+8)*i, @"file:///" + SvgPathNameless);
            }
            /*            while ((index = temp.IndexOf(toFind, index, false ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) != -1)
                        {
                            temp.Insert(index+10,SvgPathNameless);
                        }*/
            File.Delete(SvgPath);
            using (StreamWriter sw = new StreamWriter(SvgPath))
            {
                sw.Write(temp);
            }
            /*string temp = File.ReadAllText(SvgPath, Encoding.UTF8);
            var foundIndexes = new List<int>();
            string toFind = "xlink:href=\"d";
            int loltemp = temp.IndexOf(toFind);
            
            if (loltemp != -1)
            {

                Console.WriteLine(temp[loltemp+11]);
            }
            Console.WriteLine();*/
        }

        public void PrepareSvg()
        {
            string svgCode = File.ReadAllText(SvgPath, Encoding.UTF8);
            int matrixStartIndex = svgCode.IndexOf("matrix");
            int matrixLength = matrixStartIndex;
            string matrixStr;
            string matrixzero = "matrix(1, 0, 0, 1, 0, 0)";

            if (matrixStartIndex != -1) {
                matrixLength = svgCode.Substring(matrixStartIndex).IndexOf('\"');
                matrixStr = svgCode.Substring(matrixStartIndex, matrixLength);
            }
            svgCode = svgCode.Remove(matrixStartIndex, matrixLength);
            svgCode = svgCode.Insert(matrixStartIndex, matrixzero);

            int widthStartIndex = svgCode.IndexOf("width");
            int widthLength = widthStartIndex;
            string widthStr = "";
            string widthHtml = "width=\""+HTMLSizePX[0]+"px\"";
            if (widthStartIndex != -1)
            {
                widthLength = svgCode.Substring(widthStartIndex+7).IndexOf('\"');
                widthStr = svgCode.Substring(widthStartIndex, widthLength+8);
            }
            svgCode = svgCode.Remove(widthStartIndex, widthLength+8);
            svgCode = svgCode.Insert(widthStartIndex, widthHtml);

            int heightStartIndex = svgCode.IndexOf("height");
            int heightLength = widthStartIndex;
            string heightStr = "";
            string heightHtml = "height=\"" + HTMLSizePX[1] + "px\"";
            if (heightStartIndex != -1)
            {
                heightLength = svgCode.Substring(heightStartIndex + 8).IndexOf('\"');
                heightStr = svgCode.Substring(heightStartIndex, heightLength + 9);
            }
            svgCode = svgCode.Remove(heightStartIndex, heightLength+9);
            svgCode = svgCode.Insert(heightStartIndex, heightHtml);
            File.Delete(SvgPath);
            using (StreamWriter sw = new StreamWriter(SvgPath))
            {
                sw.Write(svgCode);
            }
            /*string temp = File.ReadAllText(SvgPath, Encoding.UTF8);
            var foundIndexes = new List<int>();
            string toFind = "xlink:href=\"d";
            int loltemp = temp.IndexOf(toFind);
            
            if (loltemp != -1)
            {

                Console.WriteLine(temp[loltemp+11]);
            }
            Console.WriteLine();*/
        }

        public PdfDocument ConvertToPdf() {
            PrepareConversion();
            //PrepareSvg();

            //            GetSvgWithAbsolutePaths();
            string svgCode = File.ReadAllText(SvgPath, Encoding.UTF8);

            //SizePX[0] = 266.46;
            //SizePX[1] = 153.07;

            //HTMLSizePX[0] = 510;
            //HTMLSizePX[1] = 291.997;
            var Renderer = new IronPdf.ChromePdfRenderer();
            Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.Custom;
            /*Renderer.RenderingOptions.PaperOrientation = IronPdf.Rendering.PdfPaperOrientation.Portrait;*/
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
               /*            Renderer.RenderingOptions.ViewPortHeight =Convert.ToInt32(SizePX[1])-5;

                           Renderer.RenderingOptions.ViewPortWidth = Convert.ToInt32(SizePX[0])-5;*/
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
            //Renderer.RenderingOptions.SetCustomPaperSizeinMilimeters(94, 94);
            Renderer.RenderingOptions.SetCustomPaperSizeinPixelsOrPoints(SizePX[0],SizePX[1], 72);
            //Renderer.RenderingOptions.SetCustomPaperSizeinPixelsOrPoints(250, 250, 72);
            PdfDocument pdf = Renderer.RenderHtmlAsPdf("<head><meta charset='UTF-8'/></head><body style='margin:0;padding:0;margin-bottom:0;padding-bottom:0;'><img style='width: " + HTMLSizePX[0] + "; height:" + HTMLSizePX[1] + "' src='" + SvgPath + "'/></div>"); //.SaveAs(AppFolderPath+"\\z_out\\"+ ZamowienieName +"\\"+ ProductNumber+"\\"+ SvgName.Substring(0,SvgName.Length - 4) + ".pdf");
            //PdfDocument pdf = Renderer.RenderHtmlAsPdf("<html><head><meta charset='UTF-8' /></head><body style='margin:0;padding:0;'><div style='border:1px solid black; width:"+HTMLSizePX[0]+"; height:"+HTMLSizePX[1]+";'>"+svgCode +"</div></body></html>");
            //PdfDocument pdf = Renderer.RenderHtmlAsPdf("<html><head><meta charset='UTF-8' /></head><body style='margin:0;padding:0;'></body></html>");
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