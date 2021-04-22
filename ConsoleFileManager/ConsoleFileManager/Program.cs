using System;
using System.Reflection;
using System.IO;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStructure.ShowStructRootDirectory(new DirectoryInfo(/*AppDomain.CurrentDomain.BaseDirector*/@"D:\2"));
            Info.GetInfo(@"D:\2");

            /*Info.GetInfo(@"");*/

            ActionWithFileDir.Copy(@"D:\2", @"d:\3");
            /*ActionWithFileDir.Copy(@"D:\2", @"");*/
            /*ActionWithFileDir.Copy(@"d:\2\data\ОМС\RK05.xml", @"1111");*/
            /*ActionWithFileDir.Copy(@"d:\2\data\ОМС\RK05", @"1111");*/
            ActionWithFileDir.Del(@"d:\3");
            ActionWithFileDir.Del(@"546546546");
        }
    }
}
