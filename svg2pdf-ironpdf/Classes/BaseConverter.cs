using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace svg2pdf_ironpdf.Classes
{
    public class BaseConverter // ПИТАННЯ: UTIL?
    {
        public string AppFolderPath { get; } = Directory.GetCurrentDirectory() + "\\..\\..\\";
        
        // ПИТАННЯ: Розділення класи на робочі методи
        public string[] GetFolderFiles(string whichDir)
        {
            string[] folderFiles = Directory.GetFiles(AppFolderPath + whichDir);
            return folderFiles;
        }

        // ПИТАННЯ: Дублікат метод це нормально? Як це замінить?
        public string[] GetFolderDirs(string whichDir)
        {
            string[] folderFiles = Directory.GetDirectories(AppFolderPath + whichDir);
            return folderFiles;
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
                int IndexOfNameBeginning = paths[pathIndex].LastIndexOf('\\')+1;
                fileNames[pathIndex] = paths[pathIndex].Substring(IndexOfNameBeginning);

            }
            return fileNames;
        }
    }
}
