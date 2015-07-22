using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GetFileList
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <param name="filepaths"></param>
        static void GetFilePaths(DirectoryInfo dirInfo, List<string> filepaths)
        {
            DirectoryInfo[] subdirInfos = dirInfo.GetDirectories();
            if (subdirInfos != null)
            {
                foreach (DirectoryInfo subdirInfo in subdirInfos)
                {
                    try
                    {
                        GetFilePaths(subdirInfo, filepaths);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        
                    }
                }
            }

            FileInfo[] fileInfos = dirInfo.GetFiles();
            if (fileInfos != null)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    filepaths.Add(fileInfo.FullName);
                    Console.WriteLine(fileInfo.FullName);
                }
            }
        }

        static void Main(string[] args)
        {
            // temp variable
            string line;
            int index;
            DirectoryInfo dirInfo;

            // common variable
            bool loop = true;
            string message = null;
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();

            // routine start
            Console.WriteLine();
            while (loop)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("===== menu =====");
                    Console.WriteLine("1. add directory");
                    Console.WriteLine("2. remove directory");
                    Console.WriteLine("3. show directory list");
                    Console.WriteLine("4. quit");
                    Console.Write("> ");

                    message = null;
                    line = Console.ReadLine();
                    index = int.Parse(line);
                    switch (index)
                    {
                        case 1:
                            Console.WriteLine();
                            Console.Write("Path: ");
                            line = Console.ReadLine();
                            if (Directory.Exists(line) == false)
                            {
                                throw new Exception("invalid path " + line);
                            }
                            dirInfo = new DirectoryInfo(line);
                            dirInfos.Add(dirInfo);
                            break;

                        case 2:
                            Console.Write("Remove at: ");
                            line = Console.ReadLine();
                            index = int.Parse(line);
                            dirInfos.RemoveAt(index);
                            break;

                        case 3:
                            List<string> filePathList = new List<string>();
                            foreach (DirectoryInfo _dirInfo in dirInfos)
                            {
                                GetFilePaths(_dirInfo, filePathList);
                            }
                            foreach (string filepath in filePathList)
                            {
                                Console.WriteLine(filepath);
                            }
                            Console.WriteLine("press any key to continue...");
                            Console.ReadKey();
                            break;

                        case 4:
                            loop = false;
                            break;

                        default:
                            message = string.Format("invalid index {0}", index);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                finally
                {
                    Console.Clear();
                    Console.WriteLine(message);
                }
            }
        }
    }
}
