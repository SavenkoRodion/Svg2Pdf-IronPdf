using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace svg2pdf_ironpdf
{
    public class StandartConversion : BaseConversion
    {
        
        public void StartConversion() {

            PrepareZamowieniaFolders();

        }

        public void PrepareZamowieniaFolders() {
            Directory.Delete(AppFolderPath + "\\z_tmp", true);
            Directory.CreateDirectory(AppFolderPath + "\\z_tmp");

            string[] zipNames = GetFolderFiles(AppFolderPath+"z_zip");
            UnZip(zipNames);
            PrepareZamowieniaObjects();
        }

        public void PrepareZamowieniaObjects() {
            string[] tmp = new string[1];
            tmp[0] = AppFolderPath;
            string[] zamowieniaPaths = GetFolderDirs(AppFolderPath+"\\z_tmp");
            int zamowieniaQty = zamowieniaPaths.Length;
            ZamowienieObject[] zamowieniaObjects = new ZamowienieObject[zamowieniaQty];
            for (int zamowienieIndex = 0; zamowienieIndex < zamowieniaQty; zamowienieIndex++) {
                zamowieniaObjects[zamowienieIndex] = new ZamowienieObject(zamowieniaPaths[zamowienieIndex]);
                Console.WriteLine(zamowieniaObjects[zamowienieIndex].ProductsNumber);
            }
        }
    }
}
