using System;
using System.Linq;
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
        public string[][] GetZamowieniaSvgPath(string whichDir) {
            Console.WriteLine("GetZamowieniaSvgPath");
            string[] zamowieniaPaths = GetFolderDirs(whichDir); //array with only zamowienia paths
            int[] ileProduktow = new int[zamowieniaPaths.Length]; //create empty array with number of produkts of each zamowienie
            string [] shortPaths = ReduceAppFolderPath(zamowieniaPaths); // create new array with zamowienia paths without AppFolderPath
            string[][][] toConversion = new string[zamowieniaPaths.Length][][];
            for (int zamowienieIndex = 0; zamowienieIndex < zamowieniaPaths.Length; zamowienieIndex++) {
                ileProduktow[zamowienieIndex] = GetFolderDirs(shortPaths[zamowienieIndex]).Length; // fills array with number of produkts of each zamowienie
                zamowieniaPaths[zamowienieIndex] = new string[ileProduktow[zamowienieIndex]][];
                for (int productIndex=0; productIndex< ileProduktow[zamowienieIndex];productIndex++) {
                    string[] produktFiles = GetFolderFiles(shortPaths[zamowienieIndex] + "\\" + ileProduktow[productIndex]); // array with produkt path
                    string[] shortPathProduktFiles = ReduceAppFolderPath(produktFiles); // create new array with produkt paths without AppFolderPath
                    string[] fileNames = GetFileNames(shortPathProduktFiles);
                    // ПИТАННЯ: Чи можна використовувати LINQ? Коли неможна?
                    foreach (string fileName in fileNames)
                    {
                        bool isSvg = int.TryParse(fileName[0] + "", out int uselessVar); // only needed svg starts with number, so this is my way to check is first char a number
                        if (!isSvg)
                        {
                            fileNames = fileNames.Where(val => val != fileName).ToArray();
                        }
                    }
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        fileNames[i] = produktFiles[i];
                    }
                    /*                for (int fileNameIndex = 0; fileNameIndex< fileNames.Length; fileNameIndex++) {
                                        bool isSvg = int.TryParse(fileNames[fileNameIndex] + "", out int uselessVar); // only svg starts with number, so this is my way to check is first char a number
                                        if (!isSvg)
                                        {
                                            fileNames = fileNames.Where(val => val != fileNames[fileNameIndex]).ToArray();
                                        }
                                        else
                                        {
                                            fileNames[fileNameIndex] = "lol";
                                        }
                                    }*/
                    toConversion[productIndex] = fileNames;
                }
            }
            return toConversion;
        }
    }
}
