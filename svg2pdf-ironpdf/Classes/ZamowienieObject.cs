using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void PrepareProductArray() {
            
            for (int i=0;i<ProductArray.Length;i++) {
                string[] svgNames = new string[0];
                string[] fileNames = GetFolderFiles(ProductPaths[i]);
                foreach (string fileName in fileNames)
                {
                    bool isSvg = int.TryParse(fileName[0] + "", out int uselessVar); // only needed svg starts with number, so this is my way to check is first char a number
                    Console.WriteLine(fileName[0]);
                    if (isSvg)
                    {
                        Array.Resize<string>(ref svgNames, svgNames.Length+1);
                        svgNames[svgNames.Length-1] = fileName;
                        Console.WriteLine(fileName);
                    }
                }
                Console.WriteLine(svgNames);
            }
        }

    }
}
