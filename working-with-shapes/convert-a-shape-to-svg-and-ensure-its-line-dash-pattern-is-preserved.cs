using System;
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
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                Shape shape = page.Shapes[0];

                // Set the line dash pattern to a predefined dash style
                // This ensures the dash pattern is preserved when exporting to SVG
                shape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Define the output SVG file path
                string svgOutputPath = "shape.svg";

                // Export the shape to SVG using default options
                shape.ToSvg(svgOutputPath, new SVGSaveOptions());

                Console.WriteLine($"Shape exported to SVG at: {svgOutputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }