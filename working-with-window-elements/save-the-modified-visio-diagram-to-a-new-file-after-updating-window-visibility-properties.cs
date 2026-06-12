using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram from file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one window to modify
                if (diagram.Windows.Count > 0)
                {
                    // Iterate through all windows and update visibility properties
                    foreach (Window window in diagram.Windows)
                    {
                        // Example: hide grid, guides, rulers, page breaks, connection points, and disable dynamic grid
                        window.ShowGrid = BOOL.False;
                        window.ShowGuides = BOOL.False;
                        window.ShowRulers = BOOL.False;
                        window.ShowPageBreaks = BOOL.False;
                        window.ShowConnectionPoints = BOOL.False;
                        window.DynamicGridEnabled = BOOL.False;
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }