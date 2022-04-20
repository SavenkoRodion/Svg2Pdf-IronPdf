using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace svg2pdf_ironpdf
{
    public static class MainInterface
    {
        public static void StandartConvert() {

            var converter = new StandartConverter();

            Directory.Delete(converter.AppFolderPath + "\\z_tmp", true);

            Directory.CreateDirectory(converter.AppFolderPath + "\\z_tmp");

            string[] zipNames = converter.GetFolderFiles("z_zip");
            
            converter.UnZip(zipNames);

            //string[] fileNames = converter.GetFolderDirs("z_tmp");

            converter.GetZamowieniaSvgPath("z_tmp");
            //string svgName = converter.GetSvgName(zipNames);
            
        }
    }
}
