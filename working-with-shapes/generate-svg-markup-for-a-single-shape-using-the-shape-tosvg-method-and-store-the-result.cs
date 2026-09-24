using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                // Ensure there is at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                Shape shape = page.Shapes[0];

                // Define the output SVG file path
                string svgPath = "shape_output.svg";

                // Create SVG save options (default options are sufficient)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to SVG file
                shape.ToSvg(svgPath, svgOptions);

                // Read the generated SVG markup into a string
                string svgMarkup = File.ReadAllText(svgPath);

                // Store or use the SVG markup as needed (example: output to console)
                Console.WriteLine("SVG markup for the shape:");
                Console.WriteLine(svgMarkup);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }