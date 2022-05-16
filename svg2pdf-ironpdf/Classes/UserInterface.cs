using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace svg2pdf_ironpdf
{
    public static class UserInterface
    {
        public static void UserInteraction() {
            var converter = new StandartConversion();
            converter.StartConversion();
        }
    }
}
