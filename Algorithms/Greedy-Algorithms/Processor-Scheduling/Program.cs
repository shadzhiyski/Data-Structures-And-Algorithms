namespace Processor_Scheduling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = ReadTasks();
            var optimalSchedule = FindOptimalSChedule(tasks);
            Console.WriteLine(string.Join("->", optimalSchedule.Select(t => t.TaskIndex)));
        }

        private static List<Task> FindOptimalSChedule(List<Task> tasks)
        {
            var optimalSchedule = new List<Task>();
            int taskIndex = -1, step = 0;
            while(++taskIndex < tasks.Count)
            {
                if(tasks[taskIndex].StepsCount > step)
                {
                    step = tasks[taskIndex].StepsCount;
                    optimalSchedule.Add(tasks[taskIndex]);
                }
            }

            return optimalSchedule;
        }

        private static List<Task> ReadTasks()
        {
            var tasks = new List<Task>();
            int tasksCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            for (int i = 0; i < tasksCount; i++)
            {
                var inputRow = Console.ReadLine();
                var inputParams = inputRow
                    .Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                tasks.Add(new Task(int.Parse(inputParams[0]), int.Parse(inputParams[1]), i + 1));
            }

            return tasks
                .OrderBy(t => t.StepsCount)
                .ThenByDescending(t => t.Value)
                .ToList();
        }
    }
}
