using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IronPdf;

namespace svg2pdf_ironpdf
{
    public class ZamowienieObject : BaseConversion
    {
        //public string zamowienieName;
        public string ZamowieniePath { get; set; }

        public int ProductsNumber { get; }

        public string[] ProductPaths { get; }

        public SvgObject[][] ProductArray { get; }

        
        public ZamowienieObject(string zamowieniePath) {
            ZamowieniePath = zamowieniePath;
            ProductPaths = GetFolderDirs(zamowieniePath);
            ProductsNumber = ProductPaths.Length;
            ProductArray = new SvgObject[ProductsNumber][];
            PrepareProductArray();
        }

        public void PrepareProductArray() { //do uporzadkowania

            for (int i = 0; i < ProductArray.Length; i++) {
                int count = 0;
                string[] fileNames = GetFolderFiles(ProductPaths[i]);
                foreach (string fileName in fileNames)
                {
                    bool isSvg = int.TryParse(GetFileName(fileName + "")[0] + "", out int uselessVar); // only needed svg starts with number, so this is my way to check is first char a number
                    if (isSvg)
                    {
                        count++;
                    }
                }
                ProductArray[i] = new SvgObject[count];
                for (int j = 0; j < count; j++) {
                    ProductArray[i][j] = new SvgObject(fileNames[j]);
                }
            }
            // change dir creation algorithm
            Directory.CreateDirectory("z_out\\" + GetFileName(ZamowieniePath));
            for (int i = 0; i<ProductArray.Length;i++) {
                Directory.CreateDirectory("z_out\\" + GetFileName(ZamowieniePath)+"\\"+(i+1));
                PdfDocument[] pdfArray = new PdfDocument[ProductArray[i].Length];
                for (int j = 0; j < ProductArray[i].Length; j++)
                {
                    Console.WriteLine("Conversion...");
                    pdfArray[j] = ProductArray[i][j].ConvertToPdf();
                }
                // fix this mess
                PdfDocument merged = pdfArray[0];
                for (int j = 1; j < ProductArray[i].Length; j++)
                {
                    merged = PdfDocument.Merge(pdfArray[0], pdfArray[j]);
                }
                merged.SaveAs(AppFolderPath + "\\z_out\\" + GetFileName(ZamowieniePath) + "\\" + (i+1) + "\\" + "output.pdf");
            }
        }

    }
}
