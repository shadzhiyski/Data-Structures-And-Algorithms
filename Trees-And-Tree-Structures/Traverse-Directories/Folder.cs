using System;
namespace Traverse_Directories
{
    public class Folder
    {
        public Folder(string name)
        {
            Name = name;
        }
        public Folder(string name, File[] files, Folder[] childFolders)
        {
            Name = name;
            Files = files;
            ChildFolders = childFolders;
        }

        public string Name { get; set; }

        public File[] Files { get; set; }

        public Folder[] ChildFolders { get; set; }

        public void TraverseFolder(int indent = 0) 
        {
            Console.WriteLine(
                string.Format("{0}{1}", new string(' ', indent * 4), Name));
            foreach (var file in Files)
            {
                Console.WriteLine(
                    string.Format("{0}{1} - {2: 0 000 000} bytes", 
                        new string(' ', indent * 4 + 2), 
                        file.Name,
                        file.Size));
            }
            Console.WriteLine(string.Format("{0}---", new string(' ', indent * 4 + 2)));

            foreach (var folder in ChildFolders)
            {
                folder.TraverseFolder(indent + 1);
            }
        }

        public long GetFilesSizeOf(string dirName)
        {
            long size = 0;
            GetFilesSizeOf(ref size, dirName);
            return size;
        }

        private void GetFilesSizeOf(ref long size, string dirName)
        {
            if (dirName.Equals(Name))
            {
                foreach (var file in Files)
                {
                    size += file.Size;
                }

                foreach (var folder in ChildFolders)
                {
                    folder.GetFilesSizeOfCurrentTree(ref size);
                }
            }
            else if(ChildFolders.Length > 0)
            {
                foreach (var folder in ChildFolders)
                {
                    folder.GetFilesSizeOf(ref size, dirName);
                }
            }
        }

        private void GetFilesSizeOfCurrentTree(ref long size)
        {
            foreach (var file in Files)
            {
                size += file.Size;
            }

            foreach (var folder in ChildFolders)
            {
                folder.GetFilesSizeOfCurrentTree(ref size);
            }
        }
    }
}
