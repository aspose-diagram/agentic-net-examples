using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";

                // Output SVG file path
                string svgPath = "output.svg";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("The page contains no shapes.");
                }

                // Retrieve the first shape (you can replace this with your own selection logic)
                Shape shape = page.Shapes.GetShape(0);

                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                {
                    throw new Exception("The selected shape is marked as deleted.");
                }

                // Set the line dash pattern to a standard dash
                shape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Configure SVG save options to preserve custom line patterns
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Ensure custom line patterns (e.g., dash) are saved
                    IsSavingCustomLinePattern = true,
                    // Optional: fit the SVG to the viewport
                    SVGFitToViewPort = true
                };

                // Export the specific shape to SVG
                shape.ToSvg(svgPath, svgOptions);

                Console.WriteLine($"Shape exported to SVG successfully: {svgPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }