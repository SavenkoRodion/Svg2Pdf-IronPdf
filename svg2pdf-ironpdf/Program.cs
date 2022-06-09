using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IronPdf;

namespace svg2pdf_ironpdf
{
    class Program
    {
        static void Main(string[] args)
        {
            bool is_licensed = IronPdf.License.IsLicensed;
            Console.WriteLine("start");
            Console.WriteLine(is_licensed);
            UserInterface.UserInteraction();
            Console.WriteLine("end");
        }

    }
}
