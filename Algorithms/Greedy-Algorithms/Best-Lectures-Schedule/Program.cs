namespace Best_Lectures_Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            List<ScheduleItem<int>> schedule = ReadSchedule();
            List<ScheduleItem<int>> bestLectures = FindBestLectures(schedule);

            Console.WriteLine();
            Console.WriteLine("Best Lectures:");
            foreach (var lecture in bestLectures)
            {
                Console.WriteLine(lecture);
            }
        }

        private static List<ScheduleItem<int>> FindBestLectures(
            List<ScheduleItem<int>> schedule)
        {
            var bestLectures = new List<ScheduleItem<int>>();
            int lectureIndex = 0;
            while(lectureIndex < schedule.Count)
            {
                var lecture = schedule[lectureIndex];
                bestLectures.Add(lecture);

                // skipping all lectures starting before the end of choosen (including the choosen)
                while (lectureIndex < schedule.Count 
                    && schedule[lectureIndex].Start.CompareTo(lecture.End) < 0)
                { lectureIndex++; }
            }

            return bestLectures;
        }

        private static List<ScheduleItem<int>> ReadSchedule()
        {
            var schedule = new List<ScheduleItem<int>>();
            int lecturesCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            for (int i = 0; i < lecturesCount; i++)
            {
                var inputRow = Console.ReadLine();
                var inputParams = inputRow
                    .Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                var lectureName = inputParams[0];

                var durationParams = inputParams[1]
                    .Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                var start = int.Parse(durationParams[0]);
                var end = int.Parse(durationParams[1]);

                schedule.Add(new ScheduleItem<int>(lectureName, start, end));
            }

            return schedule.OrderBy(l => l.End).ToList();
        }
    }
}
