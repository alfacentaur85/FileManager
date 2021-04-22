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

        static int _nestingLevel;

        static int _counterElementsPerPage;

        static int countElementsPerPage = 6;

        static bool flagRecursive = true;

        static bool CheckCountCurrentElements(int countElementsPerPage)
        {
            if (countElementsPerPage <= _counterElementsPerPage)
            {
                Console.WriteLine("Please press any key to continue or ESC to abort...");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.KeyChar != '\u001b')
                {
                    _counterElementsPerPage = 0;
                    return true;
                }
                else
                {
                    flagRecursive = false;
                    _counterElementsPerPage = countElementsPerPage;
                    return false;
                }
            }
            else
            {
                return true;
            }
        }


        static void WalkDirectoryTree(System.IO.DirectoryInfo root)
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
                    if (CheckCountCurrentElements(countElementsPerPage))
                    {
                        _counterElementsPerPage++;
                        Console.WriteLine("{0}{1}", new String("").PadLeft(_nestingLevel + incrementSpace, ' '), fi.Name);
                    }
                    else
                    {
                        return;
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();
                 
                if (subDirs.Length != 0)
                {
                    _nestingLevel += incrementSpace;
                }

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    if (flagRecursive)
                    {
                        if (CheckCountCurrentElements(countElementsPerPage))
                        {
                            _counterElementsPerPage++;
                            Console.WriteLine("{0}[{1}]", new String("").PadLeft(_nestingLevel, ' '), dirInfo.Name);
                            // Resursive call for each subdirectory.
                            WalkDirectoryTree(dirInfo);

                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }

        }



        public static void ShowStructRootDirectory(System.IO.DirectoryInfo root)
        {
            _nestingLevel = 0;

            _counterElementsPerPage = 1;

            Console.WriteLine("[{0}]", root.FullName);

            WalkDirectoryTree(root);

        }
    }  
}
