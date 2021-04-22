using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleFileManager
{

    public static class ActionWithFileDir
    {

        static void CopyDir(string FromDir, string ToDir)
        {
            if (CreateDir(ToDir))
            {
                foreach (string s1 in Directory.GetFiles(FromDir))
                {
                    string s2 = ToDir + "\\" + Path.GetFileName(s1);
                    File.Copy(s1, s2, true);
                }
                foreach (string s in Directory.GetDirectories(FromDir))
                {
                    CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
                }
            }
            
        }

        static PathType checkPath(string path, SourceDestination sd)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return PathType.DIRECTORY;
                }
                else if (File.Exists(path))
                {
                    return PathType.FILE;
                }
                else
                {
                    if (sd == SourceDestination.SOURCE)
                    {
                        throw new Exception(string.Concat("Incorrect File or Directory: {0}", path));
                    }
                    return PathType.UNDEFIND;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Logs.log.Add(e.Message);
                return PathType.UNDEFIND;
            }
        }

        static bool CreateDir(string toPath)
        {
            try
            {
                Directory.CreateDirectory(toPath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Logs.log.Add(e.Message);
                return false;
            }
        }

        public static void Copy(string fromPath, string toPath)
        {
            PathType _fromPath = checkPath(fromPath, SourceDestination.SOURCE);
            PathType _toPath = checkPath(toPath, SourceDestination.DESTINATION);
            if (true)
            {
                if ( (_fromPath == PathType.DIRECTORY && CreateDir(toPath)) || (_toPath == PathType.DIRECTORY))
                {
                    try
                    {
                        CopyDir(fromPath, toPath);
                        Console.WriteLine(OperationResult.SUCCESSFULY.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Logs.log.Add(e.Message);
                    }
                    
                }
                else if (_fromPath == PathType.FILE)
                {
                    try
                    {
                        File.Copy(fromPath, toPath, true);
                        Console.WriteLine(OperationResult.SUCCESSFULY.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Logs.log.Add(e.Message);
                    }
                }
                else
                {
                    try
                    {
                        throw new Exception("Error: Paths must be the same type: File or Directory");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Logs.log.Add(e.Message);
                    }
                    
                }
            }
           
        }
        public static bool Del(string path)
        {
            return false;
        }
    }
}
