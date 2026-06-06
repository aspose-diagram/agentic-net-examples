using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramPageConfiguration
{
    // Simple container for orientation and scaling settings per page
    public class PageSettings
    {
        public PrintPageOrientationValue Orientation { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }

        public PageSettings(PrintPageOrientationValue orientation, double scaleX, double scaleY)
        {
            Orientation = orientation;
            ScaleX = scaleX;
            ScaleY = scaleY;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Define mapping: page index -> settings
                var pageConfig = new Dictionary<int, PageSettings>
                {
                    // Example: page 0 -> Landscape, 100% scaling
                    { 0, new PageSettings(PrintPageOrientationValue.Landscape, 1.0, 1.0) },

                    // Example: page 1 -> Portrait, 75% scaling
                    { 1, new PageSettings(PrintPageOrientationValue.Portrait, 0.75, 0.75) },

                    // Add more mappings as needed
                };

                // Load an existing Visio diagram (replace with actual path)
                string inputPath = "input.vsdx";
                using (var diagram = new Diagram(inputPath))
                {
                    // Apply configuration to each page that has an entry in the dictionary
                    foreach (var kvp in pageConfig)
                    {
                        int pageIndex = kvp.Key;
                        PageSettings settings = kvp.Value;

                        // Validate page index
                        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                        {
                            Console.WriteLine($"Warning: Page index {pageIndex} is out of range. Skipping.");
                            continue;
                        }

                        // Retrieve the page
                        Page page = diagram.Pages[pageIndex];

                        // Set orientation
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = settings.Orientation;

                        // Set scaling factors (values >0)
                        if (settings.ScaleX > 0 && settings.ScaleY > 0)
                        {
                            page.PageSheet.PrintProps.ScaleX.Value = settings.ScaleX;
                            page.PageSheet.PrintProps.ScaleY.Value = settings.ScaleY;
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Invalid scaling values for page {pageIndex}. Skipping scaling.");
                        }
                    }

                    // Save the modified diagram
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to {outputPath}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}