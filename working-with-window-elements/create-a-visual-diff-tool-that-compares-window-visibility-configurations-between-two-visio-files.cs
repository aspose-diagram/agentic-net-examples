using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get file paths from command‑line arguments or ask the user.
            string filePath1;
            string filePath2;

            if (args.Length >= 2)
            {
                filePath1 = args[0];
                filePath2 = args[1];
            }
            else
            {
                Console.Write("Enter path to the first Visio file: ");
                filePath1 = Console.ReadLine()?.Trim() ?? string.Empty;

                Console.Write("Enter path to the second Visio file: ");
                filePath2 = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            // Load the two diagrams.
            Diagram diagram1;
            Diagram diagram2;
            try
            {
                diagram1 = new Diagram(filePath1);
                diagram2 = new Diagram(filePath2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading diagrams: {ex.Message}");
                return;
            }

            // Perform the comparison.
            CompareWindowVisibility(diagram1, diagram2);
        }

        /// <summary>
        /// Compares the visibility‑related window settings of two diagrams and writes differences to the console.
        /// </summary>
        static void CompareWindowVisibility(Diagram d1, Diagram d2)
        {
            int count1 = d1.Windows.Count;
            int count2 = d2.Windows.Count;

            if (count1 != count2)
            {
                Console.WriteLine($"Window count differs: Diagram1 has {count1}, Diagram2 has {count2}.");
                // Continue comparison for the overlapping range.
            }

            int max = Math.Max(count1, count2);
            for (int i = 0; i < max; i++)
            {
                // Retrieve windows if they exist; otherwise treat missing window as null.
                Window w1 = i < count1 ? d1.Windows[i] : null;
                Window w2 = i < count2 ? d2.Windows[i] : null;

                Console.WriteLine($"--- Comparing Window #{i + 1} ---");

                if (w1 == null || w2 == null)
                {
                    Console.WriteLine("One of the diagrams does not contain this window.");
                    continue;
                }

                // Compare each visibility property.
                CompareProperty("ShowGrid", w1.ShowGrid, w2.ShowGrid);
                CompareProperty("ShowGuides", w1.ShowGuides, w2.ShowGuides);
                CompareProperty("ShowRulers", w1.ShowRulers, w2.ShowRulers);
                CompareProperty("ShowPageBreaks", w1.ShowPageBreaks, w2.ShowPageBreaks);
                CompareProperty("ShowConnectionPoints", w1.ShowConnectionPoints, w2.ShowConnectionPoints);
                CompareProperty("DynamicGridEnabled", w1.DynamicGridEnabled, w2.DynamicGridEnabled);
            }
        }

        /// <summary>
        /// Writes a difference message if the two BOOL values are not equal.
        /// </summary>
        static void CompareProperty(string propertyName, BOOL value1, BOOL value2)
        {
            if (value1 != value2)
            {
                string val1 = value1 == BOOL.True ? "True" : "False";
                string val2 = value2 == BOOL.True ? "True" : "False";
                Console.WriteLine($"{propertyName}: Diagram1 = {val1}, Diagram2 = {val2}");
            }
        }
    }