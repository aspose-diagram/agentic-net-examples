using System;
using Aspose.Diagram;

namespace DiagramPageConfig
{
    // Represents orientation and scaling settings for a page.
    public class PageSettings
    {
        public PrintPageOrientationValue Orientation { get; set; }
        public double ScaleX { get; set; }   // 1.0 = 100%
        public double ScaleY { get; set; }   // 1.0 = 100%

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

                // Path to the source Visio file.
                string inputPath = "input.vsdx";

                // Load the diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Configuration: map page index to its settings.
                    var pageConfig = new System.Collections.Generic.Dictionary<int, PageSettings>
                    {
                        // Page 0: Landscape, 100% scaling.
                        { 0, new PageSettings(PrintPageOrientationValue.Landscape, 1.0, 1.0) },

                        // Page 1: Portrait, 75% scaling.
                        { 1, new PageSettings(PrintPageOrientationValue.Portrait, 0.75, 0.75) },

                        // Page 2: Landscape, 50% scaling.
                        { 2, new PageSettings(PrintPageOrientationValue.Landscape, 0.5, 0.5) }
                    };

                    // Apply settings to each configured page.
                    foreach (var kvp in pageConfig)
                    {
                        int pageIndex = kvp.Key;
                        PageSettings settings = kvp.Value;

                        // Ensure the page index exists.
                        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                        {
                            Console.WriteLine($"Warning: Page index {pageIndex} is out of range.");
                            continue;
                        }

                        // Retrieve the page.
                        Page page = diagram.Pages[pageIndex];

                        // Set orientation.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = settings.Orientation;

                        // Set scaling factors.
                        page.PageSheet.PrintProps.ScaleX.Value = settings.ScaleX;
                        page.PageSheet.PrintProps.ScaleY.Value = settings.ScaleY;
                    }

                    // Save the modified diagram.
                    string outputPath = "output_configured.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}