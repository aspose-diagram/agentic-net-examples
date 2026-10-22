using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputHtmlPath>");
            return;
        }

        string diagramPath = args[0];
        string htmlOutputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Prepare the HTML output file.
            using (StreamWriter htmlWriter = new StreamWriter(htmlOutputPath, false))
            {
                // Write basic HTML header.
                htmlWriter.WriteLine("<!DOCTYPE html>");
                htmlWriter.WriteLine("<html>");
                htmlWriter.WriteLine("<head>");
                htmlWriter.WriteLine("<meta charset=\"UTF-8\">");
                htmlWriter.WriteLine("<title>Combined SVG Shapes</title>");
                htmlWriter.WriteLine("</head>");
                htmlWriter.WriteLine("<body>");

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Generate a temporary SVG file for the current shape.
                        string tempSvgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".svg");
                        SVGSaveOptions svgOptions = new SVGSaveOptions();

                        // Export the shape to SVG.
                        shape.ToSvg(tempSvgPath, svgOptions);

                        // Read the SVG content.
                        string svgContent = File.ReadAllText(tempSvgPath);

                        // Embed the SVG into the HTML document.
                        htmlWriter.WriteLine("<div class=\"shape-svg\">");
                        htmlWriter.WriteLine(svgContent);
                        htmlWriter.WriteLine("</div>");

                        // Clean up the temporary SVG file.
                        try
                        {
                            File.Delete(tempSvgPath);
                        }
                        catch (Exception deleteEx)
                        {
                            Console.Error.WriteLine($"Warning: Unable to delete temporary file '{tempSvgPath}'. {deleteEx.Message}");
                        }
                    }
                }

                // Write HTML footer.
                htmlWriter.WriteLine("</body>");
                htmlWriter.WriteLine("</html>");
            }

            Console.WriteLine($"Successfully created HTML file with embedded SVGs: {htmlOutputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}