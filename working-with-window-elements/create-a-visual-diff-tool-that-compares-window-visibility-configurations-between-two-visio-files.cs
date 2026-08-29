using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioWindowDiff <firstVisioFile> <secondVisioFile>");
                return;
            }

            string firstPath = args[0];
            string secondPath = args[1];

            // Load the two Visio diagrams.
            Diagram diagram1 = new Diagram(firstPath);
            Diagram diagram2 = new Diagram(secondPath);

            // Verify that both diagrams contain windows.
            if (diagram1.Windows.Count == 0 && diagram2.Windows.Count == 0)
            {
                Console.WriteLine("Both diagrams contain no window definitions.");
                return;
            }

            // Compare the number of windows.
            if (diagram1.Windows.Count != diagram2.Windows.Count)
            {
                Console.WriteLine($"Window count differs: Diagram1 has {diagram1.Windows.Count}, Diagram2 has {diagram2.Windows.Count}");
            }

            int compareCount = Math.Min(diagram1.Windows.Count, diagram2.Windows.Count);

            // Iterate through each window and compare visibility settings.
            for (int i = 0; i < compareCount; i++)
            {
                Window w1 = diagram1.Windows[i];
                Window w2 = diagram2.Windows[i];

                // Helper to report a difference.
                void Report(string propName, object val1, object val2)
                {
                    Console.WriteLine($"Window index {i} (ID {w1.ID}) - {propName} differs: {val1} vs {val2}");
                }

                // Compare each visibility‑related property.
                if (w1.ShowGrid != w2.ShowGrid) Report(nameof(w1.ShowGrid), w1.ShowGrid, w2.ShowGrid);
                if (w1.ShowGuides != w2.ShowGuides) Report(nameof(w1.ShowGuides), w1.ShowGuides, w2.ShowGuides);
                if (w1.ShowRulers != w2.ShowRulers) Report(nameof(w1.ShowRulers), w1.ShowRulers, w2.ShowRulers);
                if (w1.ShowPageBreaks != w2.ShowPageBreaks) Report(nameof(w1.ShowPageBreaks), w1.ShowPageBreaks, w2.ShowPageBreaks);
                if (w1.ShowConnectionPoints != w2.ShowConnectionPoints) Report(nameof(w1.ShowConnectionPoints), w1.ShowConnectionPoints, w2.ShowConnectionPoints);
                if (w1.DynamicGridEnabled != w2.DynamicGridEnabled) Report(nameof(w1.DynamicGridEnabled), w1.DynamicGridEnabled, w2.DynamicGridEnabled);
            }

            Console.WriteLine("Comparison completed.");
        }
    }