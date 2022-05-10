using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;

namespace svg2pdf_ironpdf
{
    public class SvgObject : BaseConversion
    {
        public string SvgName { get; }
        public string ProductPath { get; }
        public int[] SizeMM { get; } = new int[2];
        public int[] SizePX { get; } = new int[2];

        public void ConvertToPdf() {
            var Renderer = new IronPdf.ChromePdfRenderer();
            Renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.Custom;
            //Renderer.RenderingOptions.CssMediaType = IronPdf.PdfPrintOptions.PdfCssMediaType.Screen;
            Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Screen;
            Renderer.RenderingOptions.MarginTop = 2;
            Renderer.RenderingOptions.MarginBottom = 2;
            Renderer.RenderingOptions.MarginLeft = 2;
            Renderer.RenderingOptions.MarginRight = 2;
            Renderer.RenderingOptions.SetCustomPaperSizeinMilimeters(SizeMM[0], SizeMM[1]);
            Renderer.RenderHtmlAsPdf("<body style='margin:0;padding:0;'><img style='width: "+ SizePX[0]+"; height:"+ SizePX[1]+ "' src='" + ProductPath+SvgName + "'/></div>").SaveAs(AppFolderPath+"\\z_out\\"+SvgName+".pdf");

        }
    }
}
