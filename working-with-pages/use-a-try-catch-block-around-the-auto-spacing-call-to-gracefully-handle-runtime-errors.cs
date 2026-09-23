using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input and output diagrams
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (or any specific page you need)
                Page page = diagram.Pages[0];

                // Configure auto-space options
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2,
                    DistanceInVertical = 2
                };

                // Attempt to auto-space shapes and handle any runtime errors gracefully
                try
                {
                    page.AutoSpaceShapes(page.Shapes, options);
                    Console.WriteLine("Auto-spacing completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Auto-spacing failed: {ex.Message}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }