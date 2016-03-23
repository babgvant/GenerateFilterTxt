using GenerateFilterTxt.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenerateFilterTxt
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (Directory.Exists(args[0]) && File.Exists(args[1]))
                {
                    string aVer = "1.0.0";

                    using(StreamReader sr = new StreamReader(args[1]))
                    {
                        string aText = sr.ReadToEnd();
                        Match verMatch = Regex.Match(aText, Settings.Default.VersionRegex, RegexOptions.IgnoreCase);
                        if(verMatch.Success)
                            aVer = verMatch.Groups["version"].Value;
                    }

                    foreach (string fName in Directory.GetFiles(args[0]))
                    {
                        FileInfo fi = new FileInfo(fName);
                        using(StreamWriter sw = new StreamWriter(Path.ChangeExtension(fName, "txt"),false))
                        {
                            sw.WriteLine(fi.LastWriteTime);
                            sw.WriteLine(aVer);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("directorypath pathtoassemblyfile");
                }
            }
        }
    }
}
