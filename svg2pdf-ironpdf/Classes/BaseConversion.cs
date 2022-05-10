using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace svg2pdf_ironpdf
{
    public class BaseConversion // ПИТАННЯ: UTIL?
    {
        public string AppFolderPath { get; } = Directory.GetCurrentDirectory() + "\\..\\..\\";

        // ПИТАННЯ: Розділення класи на робочі методи
        public string[] GetFolderFiles(string whichDir)
        {
            string[] folderFiles = Directory.GetFiles(whichDir);
            return folderFiles;
        }

        // ПИТАННЯ: Дублікат метод це нормально? Як це замінить?
        public string[] GetFolderDirs(string whichDir)
        {
            string[] folderFiles = Directory.GetDirectories(whichDir);
            return folderFiles;
        }

        public void UnZip(string[] zips)
        {
            Console.WriteLine("last");
            foreach (string zip in zips)
            {
                ZipFile.ExtractToDirectory(zip, AppFolderPath + "z_tmp\\");
            }
        }

        // ПИТАННЯ: Якщо метод впринципі той сам але різний ретурн чи шось, як змінить
        public string[] ReduceAppFolderPath(string[] paths)
        {
            string[] shortPaths = new string[paths.Length];
            for (int pathIndex = 0; pathIndex < paths.Length; pathIndex++)
            {
                // НАПОМИНАННЯ: НЕЯКІСНИЙ КОД, ЗАМІНИТИ НА ООП
                shortPaths[pathIndex] = paths[pathIndex].Replace(AppFolderPath, "");

            }
            return shortPaths;
        }

        public string[] GetFileNames(string[] paths)
        {
            string[] fileNames = new string[paths.Length];
            for (int pathIndex = 0; pathIndex < paths.Length; pathIndex++)
            {
                // НАПОМИНАННЯ: НЕЯКІСНИЙ КОД, ЗАМІНИТИ НА ООП

                //shortPaths[pathIndex] = paths[pathIndex].Replace(AppFolderPath, "");
                int IndexOfNameBeginning = paths[pathIndex].LastIndexOf('\\') + 1;
                fileNames[pathIndex] = paths[pathIndex].Substring(IndexOfNameBeginning);

            }
            return fileNames;
        }
    }
}
