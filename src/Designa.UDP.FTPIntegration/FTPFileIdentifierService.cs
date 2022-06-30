using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Designa.UDP.FTPIntegration
{
    public class FTPFileIdentifierService
    {
        public static int GenerateWorkShiftNumberService(string filePath)
        {
            var dirs = System.IO.Directory.GetDirectories(filePath, "*", SearchOption.AllDirectories).ToList();
            if (dirs.Count == 0)
            {
                Directory.CreateDirectory(filePath + "//1");
                return 1;
            }
            else
            {
                var directories = dirs.Select(x => x.Split("\\")[x.Split("\\").Length - 1]).Select(x=> Convert.ToInt32(x)).ToList();
                var maxDir = directories.Max();
                Directory.CreateDirectory(filePath + "//" + (maxDir + 1));
                return maxDir + 1;
            }
        }
    }
}
