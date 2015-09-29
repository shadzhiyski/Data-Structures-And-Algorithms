using System;
namespace Areas_In_Matrix
{
    public class AreaInfo : IComparable<AreaInfo>
    {
        public int Index { get; set; }

        public int Size { get; set; }

        public int StartRow { get; set; }

        public int StartCol { get; set; }

        public int CompareTo(AreaInfo other)
        {
            return other.Size.CompareTo(Size);
        }
    }
}
