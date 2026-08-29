using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Find all windows where rulers are hidden (ShowRulers == BOOL.False)
                var windowsToEnable = diagram.Windows
                                             .Where(w => w.ShowRulers == BOOL.False)
                                             .ToList();

                // Enable rulers for each filtered window
                foreach (var window in windowsToEnable)
                {
                    window.ShowRulers = BOOL.True;
                }

                // Optionally save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Updated {windowsToEnable.Count} window(s) to show rulers.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }