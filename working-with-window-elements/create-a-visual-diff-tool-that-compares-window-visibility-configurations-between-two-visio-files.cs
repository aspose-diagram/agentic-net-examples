using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt for the first Visio file path
            Console.Write("Enter path to the first Visio file: ");
            string firstPath = Console.ReadLine()?.Trim();

            // Prompt for the second Visio file path
            Console.Write("Enter path to the second Visio file: ");
            string secondPath = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(firstPath) || string.IsNullOrEmpty(secondPath))
            {
                Console.WriteLine("Both file paths must be provided.");
                return;
            }

            // Load the two diagrams
            Diagram diagram1 = new Diagram(firstPath);
            Diagram diagram2 = new Diagram(secondPath);

            // Compare window collections
            int count1 = diagram1.Windows.Count;
            int count2 = diagram2.Windows.Count;

            Console.WriteLine($"\nWindow count - File1: {count1}, File2: {count2}");

            int minCount = Math.Min(count1, count2);
            for (int i = 0; i < minCount; i++)
            {
                Window w1 = diagram1.Windows[i];
                Window w2 = diagram2.Windows[i];

                Console.WriteLine($"\nComparing Window #{i + 1} (ID {w1.ID} vs {w2.ID}):");

                CompareProperty("ShowGrid", w1.ShowGrid, w2.ShowGrid);
                CompareProperty("ShowGuides", w1.ShowGuides, w2.ShowGuides);
                CompareProperty("ShowRulers", w1.ShowRulers, w2.ShowRulers);
                CompareProperty("ShowPageBreaks", w1.ShowPageBreaks, w2.ShowPageBreaks);
                CompareProperty("DynamicGridEnabled", w1.DynamicGridEnabled, w2.DynamicGridEnabled);
                CompareProperty("ShowConnectionPoints", w1.ShowConnectionPoints, w2.ShowConnectionPoints);
                CompareProperty("WindowState", w1.WindowState, w2.WindowState);
                CompareProperty("WindowWidth", w1.WindowWidth, w2.WindowWidth);
                CompareProperty("WindowHeight", w1.WindowHeight, w2.WindowHeight);
            }

            // Report extra windows if counts differ
            if (count1 > minCount)
            {
                Console.WriteLine($"\nFile1 has {count1 - minCount} extra window(s) not present in File2.");
            }
            if (count2 > minCount)
            {
                Console.WriteLine($"\nFile2 has {count2 - minCount} extra window(s) not present in File1.");
            }
        }

        // Generic comparison helper for BOOL and other value types
        static void CompareProperty<T>(string name, T value1, T value2)
        {
            if (!Equals(value1, value2))
            {
                Console.WriteLine($"  Difference in {name}: File1 = {value1}, File2 = {value2}");
            }
        }
    }