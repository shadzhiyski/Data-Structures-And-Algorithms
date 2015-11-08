namespace Processor_Scheduling
{
    internal class Task
    {
        public int TaskIndex { get; set; }
        public int Value { get; set; }
        public int StepsCount { get; set; }

        public Task(int value, int stepsCpunt, int taskIndex)
        {
            Value = value;
            StepsCount = stepsCpunt;
            TaskIndex = taskIndex;
        }
    }
}