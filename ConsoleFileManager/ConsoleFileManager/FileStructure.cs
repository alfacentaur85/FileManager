using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileManager
{
    public static class FileStructure
    {
        

        

        const short incrementSpace = 2;

        static int NestingLevel { get; set; }

        public static void ShowStructRootDirectory(System.IO.DirectoryInfo root)
        {
            NestingLevel = 0;

            Console.WriteLine("[{0}]", root.FullName);

            WalkDirectoryTree(root);

        }

        public static void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {   

            //array of DirectoryInfo
            System.IO.DirectoryInfo[] subDirs = null;

            if (root != null)
            {

                //array of FileInfo
                System.IO.FileInfo[] files = null;

                // First, process all the files directly under this folder
                try
                {
                    files = root.GetFiles("*.*");
                }
                // This is thrown if even one of the files requires permissions greater
                // than the application provides.
                catch (UnauthorizedAccessException e)
                {
                    // This code just writes out the message and continues to recurse.
                    // You may decide to do something different here. For example, you
                    // can try to elevate your privileges and access the file again.
                    Logs.log.Add(e.Message);
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                

                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree(). 

                    Console.WriteLine("{0}{1}", new String("").PadLeft(NestingLevel + incrementSpace, ' '), fi.Name);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                if (subDirs.Length != 0)
                {
                    NestingLevel += incrementSpace;
                }

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    Console.WriteLine("{0}[{1}]", new String("").PadLeft(NestingLevel, ' '), dirInfo.Name);

                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo);

                    
                }
                
            }
        }


    }
}
