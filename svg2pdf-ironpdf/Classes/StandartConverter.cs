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
        public void CompareSumth(string[] produktFIles, string[] zamowieniaPaths) {
            string[] tmpShortPath = GetFileNames(produktFIles);
            string[] outFullPath = new string[produktFIles.Length];
            int my_i = 0;
            foreach (string fileName in produktFIles) {
                for (int i = 0; i < produktFIles.Length; i++)
                {
                    if (tmpShortPath[i] == fileName)
                    {
                        outFullPath[my_i] = zamowieniaPaths[i];
                        Console.WriteLine(outFullPath[my_i]);
                        my_i++;
                        break;
                    }
                }
            }
        }
        public string[][] GetZamowieniaSvgPath(string whichDir) {
            Console.WriteLine("GetZamowieniaSvgPath");
            string[] zamowieniaPaths = GetFolderDirs(whichDir); //array with only zamowienia paths
            int[] ileProduktow = new int[zamowieniaPaths.Length]; //create empty array with number of produkts of each zamowienie
            string [] shortPaths = ReduceAppFolderPath(zamowieniaPaths); // create new array with zamowienia paths without AppFolderPath
            string[][] toConversion = new string[ileProduktow.Length][];
            for (int produktIndex = 0; produktIndex < ileProduktow.Length; produktIndex++) {
                ileProduktow[produktIndex] = GetFolderDirs(shortPaths[produktIndex]).Length; // fills array with number of produkts of each zamowienie
                for (int tryIndex=0; tryIndex< ileProduktow[produktIndex];tryIndex++) {
                
                }
                string[] produktFiles = GetFolderFiles(shortPaths[produktIndex] + "\\" + ileProduktow[produktIndex]); // array with produkt path
                Console.WriteLine(shortPaths[produktIndex]);
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
                for (int i=0;i< fileNames.Length;i++) {
                    fileNames[i] = produktFiles[i];
                }
                foreach (string fileName in fileNames)
                {
                    //Console.WriteLine(fileName);
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
                    toConversion[produktIndex] = fileNames;
            }
            return toConversion;
        }
    }
}
