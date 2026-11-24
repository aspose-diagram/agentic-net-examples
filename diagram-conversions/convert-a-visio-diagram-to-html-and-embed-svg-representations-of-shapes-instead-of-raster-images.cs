using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : "output";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Ensure the output directory exists.
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory \"{outputFolder}\": {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // StringBuilder to accumulate the final HTML content.
                StringBuilder htmlBuilder = new StringBuilder();

                // Basic HTML header.
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html>");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("<meta charset=\"utf-8\" />");
                htmlBuilder.AppendLine("<title>Visio Diagram Export</title>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");

                // Iterate through each page in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    htmlBuilder.AppendLine($"<h2>Page {pageIndex + 1}</h2>");

                    // Iterate through each shape on the current page.
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Build a unique file name for the SVG of this shape.
                        string svgFileName = $"page{pageIndex + 1}_shape{shape.ID}.svg";
                        string svgFilePath = Path.Combine(outputFolder, svgFileName);

                        // Export the shape to SVG.
                        try
                        {
                            SVGSaveOptions svgOptions = new SVGSaveOptions();
                            // Use default options; no additional properties are required.
                            shape.ToSvg(svgFilePath, svgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Error exporting shape ID {shape.ID} to SVG: {ex.Message}");
                            continue; // Skip this shape but continue processing others.
                        }

                        // Read the generated SVG content.
                        string svgContent;
                        try
                        {
                            svgContent = File.ReadAllText(svgFilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Error reading SVG file \"{svgFilePath}\": {ex.Message}");
                            continue;
                        }

                        // Embed the SVG inline within a div.
                        htmlBuilder.AppendLine("<div style=\"margin:10px; display:inline-block; border:1px solid #ccc;\">");
                        htmlBuilder.AppendLine(svgContent);
                        htmlBuilder.AppendLine("</div>");
                    }
                }

                // Close HTML tags.
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the final HTML file.
                string htmlOutputPath = Path.Combine(outputFolder, "diagram.html");
                try
                {
                    File.WriteAllText(htmlOutputPath, htmlBuilder.ToString(), Encoding.UTF8);
                    Console.WriteLine($"HTML export completed successfully: {htmlOutputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error writing HTML file \"{htmlOutputPath}\": {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file \"{inputPath}\": {ex.Message}");
        }
    }
}