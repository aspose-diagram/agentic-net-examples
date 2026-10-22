using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: VisualDiff <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Guard against missing input files.
        if (!File.Exists(filePath1))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath1}");
            return;
        }
        if (!File.Exists(filePath2))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath2}");
            return;
        }

        try
        {
            // Load the two Visio diagrams.
            Diagram diagram1 = new Diagram(filePath1);
            Diagram diagram2 = new Diagram(filePath2);

            // Retrieve window collections.
            var windows1 = diagram1.Windows;
            var windows2 = diagram2.Windows;

            // Compare window counts.
            if (windows1.Count != windows2.Count)
            {
                Console.WriteLine($"Window count differs: File1 has {windows1.Count}, File2 has {windows2.Count}");
            }

            // Determine the number of windows to compare (minimum of both counts).
            int compareCount = Math.Min(windows1.Count, windows2.Count);

            // Iterate through each window and compare visibility settings.
            for (int i = 0; i < compareCount; i++)
            {
                Window w1 = windows1[i];
                Window w2 = windows2[i];

                Console.WriteLine($"Comparing Window #{i + 1}:");

                // Helper local function to report a difference.
                void ReportDiff(string propName, BOOL val1, BOOL val2)
                {
                    if (val1 != val2)
                    {
                        Console.WriteLine($"  {propName} differs – File1: {val1}, File2: {val2}");
                    }
                }

                // Compare each visibility‑related property.
                ReportDiff("ShowGrid", w1.ShowGrid, w2.ShowGrid);
                ReportDiff("ShowGuides", w1.ShowGuides, w2.ShowGuides);
                ReportDiff("ShowRulers", w1.ShowRulers, w2.ShowRulers);
                ReportDiff("ShowPageBreaks", w1.ShowPageBreaks, w2.ShowPageBreaks);
                ReportDiff("DynamicGridEnabled", w1.DynamicGridEnabled, w2.DynamicGridEnabled);
                ReportDiff("ShowConnectionPoints", w1.ShowConnectionPoints, w2.ShowConnectionPoints);
            }

            // If there are extra windows in either file, list them.
            if (windows1.Count > compareCount)
            {
                Console.WriteLine($"File1 has {windows1.Count - compareCount} additional window(s) not present in File2.");
            }
            if (windows2.Count > compareCount)
            {
                Console.WriteLine($"File2 has {windows2.Count - compareCount} additional window(s) not present in File1.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}