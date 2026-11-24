using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: VisualDiff <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Guard against missing files
        if (!File.Exists(filePath1))
        {
            Console.Error.WriteLine($"File not found: {filePath1}");
            return;
        }
        if (!File.Exists(filePath2))
        {
            Console.Error.WriteLine($"File not found: {filePath2}");
            return;
        }

        try
        {
            // Load both Visio diagrams
            Diagram diagram1 = new Diagram(filePath1);
            Diagram diagram2 = new Diagram(filePath2);

            // Retrieve window collections
            var windows1 = diagram1.Windows;
            var windows2 = diagram2.Windows;

            int count1 = windows1.Count;
            int count2 = windows2.Count;

            // Report if window counts differ
            if (count1 != count2)
            {
                Console.WriteLine($"Window count differs: Diagram1 has {count1}, Diagram2 has {count2}");
            }

            int minCount = Math.Min(count1, count2);

            // Compare each corresponding window
            for (int i = 0; i < minCount; i++)
            {
                Window w1 = windows1[i];
                Window w2 = windows2[i];

                bool hasDifference = false;

                // Helper local function to compare a BOOL property
                void CompareProperty(string name, BOOL val1, BOOL val2)
                {
                    if (val1 != val2)
                    {
                        hasDifference = true;
                        Console.WriteLine($"  - {name}: Diagram1 = {val1}, Diagram2 = {val2}");
                    }
                }

                Console.WriteLine($"Comparing Window {i}:");

                // Compare visibility related properties
                CompareProperty("ShowGrid", w1.ShowGrid, w2.ShowGrid);
                CompareProperty("ShowGuides", w1.ShowGuides, w2.ShowGuides);
                CompareProperty("ShowRulers", w1.ShowRulers, w2.ShowRulers);
                CompareProperty("ShowPageBreaks", w1.ShowPageBreaks, w2.ShowPageBreaks);
                CompareProperty("DynamicGridEnabled", w1.DynamicGridEnabled, w2.DynamicGridEnabled);
                CompareProperty("ShowConnectionPoints", w1.ShowConnectionPoints, w2.ShowConnectionPoints);

                if (!hasDifference)
                {
                    Console.WriteLine("  No differences found.");
                }
            }

            // If one diagram has extra windows, list them
            if (count1 > minCount)
            {
                Console.WriteLine($"Diagram1 has additional windows starting at index {minCount}:");
                for (int i = minCount; i < count1; i++)
                {
                    Console.WriteLine($"  - Window {i} (ID={windows1[i].ID})");
                }
            }
            if (count2 > minCount)
            {
                Console.WriteLine($"Diagram2 has additional windows starting at index {minCount}:");
                for (int i = minCount; i < count2; i++)
                {
                    Console.WriteLine($"  - Window {i} (ID={windows2[i].ID})");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}