namespace Traverse_Directories
{
    public class File
    {
        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; set; }

        public long Size { get; set; }
    }
}
