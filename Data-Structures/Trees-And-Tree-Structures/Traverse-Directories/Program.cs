namespace Traverse_Directories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class Program
    {
        public static Folder TraverseDir(string path)
        {
            DirectoryInfo di = new DirectoryInfo(@path);
            var files = new List<File>();
            var folders = new List<Folder>();
            foreach (var fi in di.EnumerateFiles())
            {
                files.Add(new File(fi.Name, fi.Length));
            }

            foreach (var fo in di.EnumerateDirectories())
            {
                var folder = new Folder(fo.Name);
                TraverseDir(ref folder, string.Format(@"{0}\{1}", path, fo.Name));
                folders.Add(folder);
            }

            return new Folder(path, files.ToArray(), folders.ToArray());
        }

        public static void TraverseDir(ref Folder dir, string path) 
        {
            DirectoryInfo di = new DirectoryInfo(@path);
            var files = new List<File>();
            var folders = new List<Folder>();
            foreach (var fi in di.EnumerateFiles())
            {
                files.Add(new File(fi.Name, fi.Length));
            }

            foreach (var fo in di.EnumerateDirectories())
            {
                var folder = new Folder(fo.Name);
                TraverseDir(ref folder, string.Format(@"{0}\{1}", path, fo.Name));
                folders.Add(folder);
            }

            dir.Files = files.ToArray();
            dir.ChildFolders = folders.ToArray();
        }

        static void Main(string[] args)
        {
            var dir = TraverseDir(@"C:\data");
            dir.TraverseFolder();
            long size = 0;
            size = dir.GetFilesSizeOf("db");
            Console.WriteLine(size);
        }
    }
}
