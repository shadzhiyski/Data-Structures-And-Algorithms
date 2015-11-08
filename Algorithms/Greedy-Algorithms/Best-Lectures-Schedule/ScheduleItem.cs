namespace Best_Lectures_Schedule
{
    using System;

    internal class ScheduleItem<TLimit>
        where TLimit : IComparable<TLimit>
    {
        public string ItemName { get; set; }
        public TLimit Start { get; set; }
        public TLimit End { get; set; }

        public ScheduleItem(string itemName, TLimit start, TLimit end)
        {
            ItemName = itemName;
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1} -> {2}", Start, End, ItemName);
        }
    }
}