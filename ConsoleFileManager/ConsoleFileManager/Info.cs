using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleFileManager
{
    public static class Info
    {

        static long sizeOfDir = 0;

        public static void GetInfo(String fileDir)
        {
            try
            {
                if (String.IsNullOrEmpty(fileDir))
                {
                    ShowInfo(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));
                }
                else if (Directory.Exists(fileDir))
                {
                    ShowInfo(new DirectoryInfo(fileDir));
                }
                else if (File.Exists(fileDir))
                {
                    ShowInfo(new FileInfo(fileDir));
                }
                else
                {
                    throw new Exception("Incorrect parameter");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Logs.log.Add(e.Message);
            }
            
        }

        static long GetSizeDir<T>(DirectoryInfo dir)
        {
            //array of DirectoryInfo
            DirectoryInfo[] subDirs = null;

            if (dir != null)
            {

                //array of FileInfo
                FileInfo[] files = null;

                // First, process all the files directly under this folder
                try
                {
                    files = dir.GetFiles("*.*");
                }
                catch (UnauthorizedAccessException e)
                {
                    Logs.log.Add(e.Message);
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                foreach (System.IO.FileInfo fi in files)
                {
                    sizeOfDir += fi.Length;
                }

                // Now find all the subdirectories under this directory.
                subDirs = dir.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {

                    // Resursive call for each subdirectory.
                    GetSizeDir<DirectoryInfo>(dirInfo);
                }

                

            }
            return sizeOfDir;
        }

        static long GetSizeFileDir<T>(T fileDir) where T : FileSystemInfo
        {
            if (fileDir is FileInfo)
            {
                return (fileDir as FileInfo).Length;
            }
            else
            {
                sizeOfDir = 0;

                return GetSizeDir<DirectoryInfo>(fileDir as DirectoryInfo);
            }
        }

        static void ShowInfo<T>(T FileDir) where T : FileSystemInfo
        {
            Console.WriteLine(FileDir.ToString());
            Console.WriteLine("Attributes: {0}", FileDir.Attributes.ToString());
            Console.WriteLine("Size: {0:N0} bytes",GetSizeFileDir(FileDir));

        }

        





    }
}
