using System;
using System.Collections.Generic;
using System.Linq;
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
            int[] ileProduktow = new int[zamowieniaPaths.Length]; //array with number of produkts of each zamowienie
            string [] shortPaths = ReduceAppFolderPath(zamowieniaPaths); // removes from path AppFolderPath
            for (int i = 0; i < ileProduktow.Length; i++) {
                ileProduktow[i] = GetFolderDirs(shortPaths[i]).Length;
                //Console.WriteLine(i);
                /*                foreach (string lolek in lol) {
                                    Console.WriteLine(lolek);
                                }*/
                string[] produktFiles = GetFolderFiles(shortPaths[i] + "\\" + ileProduktow[i]);
                string[] shortPathProduktFiles = ReduceAppFolderPath(produktFiles);
                //Console.WriteLine(shortPaths[i] + "\\" + ileProduktow[i]);
                foreach (string produktFile in shortPathProduktFiles) {
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
