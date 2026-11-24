using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output HTML file path (second argument or default)
        string outputHtmlPath = args.Length > 1 ? args[1] : "output.html";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Temporary folder to store per‑shape SVG files
        string tempSvgFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramSvg");
        try
        {
            Directory.CreateDirectory(tempSvgFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary SVG folder: {ex.Message}");
            return;
        }

        // String builder for the final HTML content
        var htmlBuilder = new StringBuilder();
        htmlBuilder.AppendLine("<!DOCTYPE html>");
        htmlBuilder.AppendLine("<html>");
        htmlBuilder.AppendLine("<head><meta charset=\"UTF-8\"><title>Diagram Export</title></head>");
        htmlBuilder.AppendLine("<body>");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    int pageIndex = page.ID; // Use page ID as index for export options

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip hidden or deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Generate a unique temporary SVG file name for the shape
                        string shapeSvgPath = Path.Combine(tempSvgFolder, $"shape_{pageIndex}_{shape.ID}.svg");

                        // Configure SVG save options (export only the current shape)
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        svgOptions.PageIndex = pageIndex;   // export the current page context
                        svgOptions.ExportHiddenPage = false;   // do not export hidden pages
                        svgOptions.ExportGuideShapes = false;  // do not export guide shapes
                        svgOptions.SVGFitToViewPort = true;    // fit SVG to viewport

                        // Export the shape to SVG file
                        shape.ToSvg(shapeSvgPath, svgOptions);

                        // Read the generated SVG markup
                        string svgContent = File.ReadAllText(shapeSvgPath);

                        // Embed the SVG markup inline within the HTML
                        htmlBuilder.AppendLine("<div class=\"shape-container\">");
                        htmlBuilder.AppendLine(svgContent);
                        htmlBuilder.AppendLine("</div>");
                    }
                }
            }

            // Close the HTML body
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Write the final HTML file
            File.WriteAllText(outputHtmlPath, htmlBuilder.ToString());
            Console.WriteLine($"Diagram exported to HTML with inline SVG: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
        finally
        {
            // Clean up temporary SVG files
            try
            {
                if (Directory.Exists(tempSvgFolder))
                {
                    Directory.Delete(tempSvgFolder, true);
                }
            }
            catch
            {
                // Suppress cleanup errors
            }
        }
    }
}
