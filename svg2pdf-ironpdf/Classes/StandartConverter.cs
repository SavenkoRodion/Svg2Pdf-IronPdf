using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using svg2pdf_ironpdf.Classes;

namespace svg2pdf_ironpdf
{
    class StandartConverter : BaseConverter // ПИТАННЯ: Наслідування декількох клас в ООП це нормально?
    {
        public void UnZip(string[] zips)
        {
            foreach (string zip in zips) {
                ZipFile.ExtractToDirectory(zip, AppFolderPath + "z_tmp\\");
            }
        }

        public void GetZamowieniaSvgPath(string whichDir) {
            Console.WriteLine("GetZamowieniaSvgPath");
            string[] zamowieniaPaths = GetFolderDirs(whichDir); //array with only zamowienia paths
            int[] ileProduktow = new int[zamowieniaPaths.Length]; //create empty array with number of produkts of each zamowienie
            string [] shortPaths = ReduceAppFolderPath(zamowieniaPaths); // create new array with zamowienia paths without AppFolderPath
            for (int i = 0; i < ileProduktow.Length; i++) {
                ileProduktow[i] = GetFolderDirs(shortPaths[i]).Length; // fills array with number of produkts of each zamowienie
                //Console.WriteLine(i);
                /*                foreach (string lolek in lol) {
                                    Console.WriteLine(lolek);
                                }*/
                string[] produktFiles = GetFolderFiles(shortPaths[i] + "\\" + ileProduktow[i]); // array with produkt path
                string[] shortPathProduktFiles = ReduceAppFolderPath(produktFiles); // create new array with produkt paths without AppFolderPath
                string[] fileNames = GetFileNames(shortPathProduktFiles);
                //Console.WriteLine(shortPaths[i] + "\\" + ileProduktow[i]);
                Console.WriteLine("Z1");
                foreach (string produktFile in fileNames) {
                    Console.WriteLine(produktFile);

/*                    string help = "" + produktFile[0];
                    bool isNumeric = int.TryParse(help, out int n);
                    Console.WriteLine(produktFile);
                    if (isNumeric) {
                        Console.WriteLine("YES");
                    }*/

                }
            }
            // получаю стринг з путем
            // получаю обризаю по останний слеш
            // якщо перший знак цифра то це мій файл
        }
    }
}
