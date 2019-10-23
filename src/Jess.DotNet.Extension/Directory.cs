using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Jess.DotNet.Extension
{
    public static class Directory
    {
        /// <summary>
        /// Copy Files in Directory
        /// targetdir can not in sourcedir
        /// </summary>
        /// <param name="sourcedir"></param>
        /// <param name="targetdir"></param>
        public static void Copy(string sourcedir, string targetdir)
        {
            try
            {
                if (!System.IO.Directory.Exists(sourcedir))
                {
                    throw new DirectoryNotFoundException($"[{sourcedir}] not found!");
                }
                if (!System.IO.Directory.Exists(targetdir))
                {
                    System.IO.Directory.CreateDirectory(targetdir);
                }

                string[] files = System.IO.Directory.GetFiles(sourcedir);
                foreach (string file in files)
                {
                    string pFilePath = Path.Combine(targetdir, Path.GetFileName(file));
                    if (File.Exists(pFilePath))
                        continue;
                    File.Copy(file, pFilePath, true);
                }

                string[] dirs = System.IO.Directory.GetDirectories(sourcedir);
                foreach (string dir in dirs)
                {
                    Copy(dir, Path.Combine(targetdir, Path.GetFileName(dir)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
