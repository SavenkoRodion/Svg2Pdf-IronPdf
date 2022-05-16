using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using System.Xml;

namespace svg2pdf_ironpdf
{
    public class SvgObject : BaseConversion
    {
        public string SvgName { get; }
        public string SvgPath { get; }
        public double[] SizeMM { get; set; } = new double[2];
        public double[] SizePX { get; set; } = new double[2];
        public double[] HTMLSizePX { get; set; } = new double[2];

        public SvgObject(string svgPath) {
            SvgPath = svgPath;
            SvgName = GetFileName(SvgPath);
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
            SizeMM[0] = 90;
            SizeMM[1] = 50;
            SizePX[0] = 266.46;
            SizePX[1] = 153.07;

            HTMLSizePX[0] = 510;
            HTMLSizePX[1] = 291.997;
            ConvertToPdf();
        }
        public void ConvertToPdf() {
            var Renderer = new IronPdf.ChromePdfRenderer();
            Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.Custom;
            //Renderer.RenderingOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;
            Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;
            Renderer.RenderingOptions.MarginTop = 0;
            Renderer.RenderingOptions.MarginBottom = 0;
            Renderer.RenderingOptions.MarginLeft = 0;
            Renderer.RenderingOptions.MarginRight = 0;
            //Renderer.RenderingOptions.DPI = 72;
            //Renderer.RenderingOptions.SetCustomPaperSizeinMilimeters(SizeMM[0]+8, SizeMM[1]+8);
            Renderer.RenderingOptions.SetCustomPaperSizeinPixelsOrPoints(SizePX[0]+12, SizePX[1]+12,72);
            Renderer.RenderHtmlAsPdf("<body style='margin:0;padding:0;'><img style='width: "+ HTMLSizePX[0]+"; height:"+ HTMLSizePX[1]+ "' src='" + SvgPath + "'/></div>").SaveAs(AppFolderPath+"\\z_out\\"+SvgName+".pdf");

        }
    }
}
// obsluga wyjatkow
// consolelog
// xml - xml reader