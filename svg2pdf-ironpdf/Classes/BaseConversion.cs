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
        public string AppFolderPath { get; } = Directory.GetCurrentDirectory() + "\\";

        // ПИТАННЯ: Розділення класи на робочі методи
        public string[] GetFolderFiles(string whichDir)
        {
            string[] folderFiles = Directory.GetFiles(whichDir);
            return folderFiles;
        }

        public string[] GetFolderDirs(string whichDir)
        {
            string[] folderFiles = Directory.GetDirectories(whichDir);
            return folderFiles;
        }

        public void UnZip(string[] zips)
        {
            foreach (string zip in zips)
            {
                ZipFile.ExtractToDirectory(zip, AppFolderPath + "z_tmp\\");
            }
        }

        // ПИТАННЯ: Якщо метод впринципі той сам але різний ретурн чи шось, як змінить
        public string ReduceAppFolderPath(string path)
        {
            path = path.Replace(AppFolderPath, "");
            return path;
        }
        public string GetFileName(string path)
        {
            // НАПОМИНАННЯ: НЕЯКІСНИЙ КОД, ЗАМІНИТИ НА ООП

            string fileName;
            int IndexOfNameBeginning = path.LastIndexOf('\\') + 1;
            fileName = path.Substring(IndexOfNameBeginning);
            return fileName;
        }
    }
}
