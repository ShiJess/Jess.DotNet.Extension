using System;
using Xunit;

namespace Jess.DotNet.Extension.Test
{
    public class DirectoryTests
    {
        [Fact]
        public void CopyTest()
        {
            string sourcedir = AppDomain.CurrentDomain.BaseDirectory;
            string targetdir = System.IO.Path.Combine(System.IO.Directory.GetParent(sourcedir).FullName, "target");

            Directory.Copy(sourcedir, targetdir);
        }
    }
}