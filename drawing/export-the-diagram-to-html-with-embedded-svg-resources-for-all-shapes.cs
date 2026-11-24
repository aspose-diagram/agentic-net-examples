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
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputHtml = "output.html";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a StringBuilder for the HTML content
                StringBuilder htmlBuilder = new StringBuilder();

                // Write basic HTML header
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("    <title>Visio Diagram Export</title>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("    <h1>Visio Diagram Export with Embedded SVG Shapes</h1>");

                // Temporary directory for intermediate SVG files
                string tempDir = Path.Combine(Path.GetTempPath(), "AsposeDiagramSvgExport");
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }

                int pageIndex = 0;
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    htmlBuilder.AppendLine($"    <h2>Page {pageIndex + 1}: {page.Name}</h2>");

                    // Iterate through each shape on the page
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                        {
                            continue;
                        }

                        // Build a unique temporary SVG file path for the shape
                        string svgFileName = $"shape_page{pageIndex}_shape{i}.svg";
                        string svgPath = Path.Combine(tempDir, svgFileName);

                        // Export the shape to SVG
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg(svgPath, svgOptions);

                        // Read the SVG content
                        string svgContent = File.ReadAllText(svgPath);

                        // Embed the SVG into the HTML
                        htmlBuilder.AppendLine("    <div style=\"margin:10px; border:1px solid #ccc; padding:5px;\">");
                        htmlBuilder.AppendLine($"        <!-- Shape ID: {shape.ID}, Name: {shape.Name} -->");
                        htmlBuilder.AppendLine(svgContent);
                        htmlBuilder.AppendLine("    </div>");

                        // Delete the temporary SVG file
                        try
                        {
                            File.Delete(svgPath);
                        }
                        catch
                        {
                            // If deletion fails, ignore and continue
                        }
                    }

                    pageIndex++;
                }

                // Close HTML body
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the final HTML to the output file
                File.WriteAllText(outputHtml, htmlBuilder.ToString());

                // Clean up temporary directory
                try
                {
                    Directory.Delete(tempDir, true);
                }
                catch
                {
                    // If cleanup fails, ignore
                }

                Console.WriteLine($"Diagram exported to HTML with embedded SVG: {outputHtml}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}