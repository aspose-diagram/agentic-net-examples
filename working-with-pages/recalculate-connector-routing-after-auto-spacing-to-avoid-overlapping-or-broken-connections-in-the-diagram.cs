using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Configure auto‑spacing options (horizontal and vertical distances)
                AutoSpaceOptions autoSpaceOpts = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2, // distance in inches
                    DistanceInVertical = 2    // distance in inches
                };

                // Apply auto‑spacing to all shapes on the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOpts);

                // After spacing, re‑layout the page to recalculate connector routing
                LayoutOptions layoutOpts = new LayoutOptions
                {
                    EnlargePage = false // keep page size unchanged
                };
                diagram.Layout(layoutOpts);

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }