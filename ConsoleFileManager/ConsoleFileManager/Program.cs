using System;
using System.Reflection;
using System.IO;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            /*FileStructure.ShowStructRootDirectory(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory@"D:\2"));*/
            Info.GetInfo(@"D:\2");
            
            /*Info.GetInfo(@"");*/

        }
    }
}
