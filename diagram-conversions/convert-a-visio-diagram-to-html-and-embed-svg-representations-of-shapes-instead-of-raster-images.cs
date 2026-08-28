using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";

                // Output HTML file path
                string htmlOutputPath = "output.html";

                // Folder to store generated SVG files for each shape
                string svgFolder = "svg_shapes";

                // Ensure the SVG output folder exists
                if (!Directory.Exists(svgFolder))
                {
                    Directory.CreateDirectory(svgFolder);
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // StringBuilder to construct the HTML content
                StringBuilder htmlBuilder = new StringBuilder();

                // Basic HTML header
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                htmlBuilder.AppendLine("    <title>Visio Diagram with SVG Shapes</title>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("    <h1>Visio Diagram Exported as HTML with SVG Shapes</h1>");

                // Iterate through each page and each shape
                foreach (Page page in diagram.Pages)
                {
                    htmlBuilder.AppendLine($"    <h2>Page: {page.Name}</h2>");
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Generate a unique SVG file name for the shape
                        string svgFileName = $"shape_{shape.ID}.svg";
                        string svgFilePath = Path.Combine(svgFolder, svgFileName);

                        // Export the shape to SVG
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg(svgFilePath, svgOptions);

                        // Embed the SVG in the HTML using <object> tag
                        htmlBuilder.AppendLine("    <div style=\"margin:10px; display:inline-block; text-align:center;\">");
                        htmlBuilder.AppendLine($"        <p>Shape ID: {shape.ID}</p>");
                        htmlBuilder.AppendLine($"        <object type=\"image/svg+xml\" data=\"{svgFolder}/{svgFileName}\" width=\"200\" height=\"200\"></object>");
                        htmlBuilder.AppendLine("    </div>");
                    }
                }

                // Close HTML tags
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the HTML content to the output file
                File.WriteAllText(htmlOutputPath, htmlBuilder.ToString());

                Console.WriteLine($"HTML file generated at: {Path.GetFullPath(htmlOutputPath)}");
                Console.WriteLine($"SVG files are stored in: {Path.GetFullPath(svgFolder)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }