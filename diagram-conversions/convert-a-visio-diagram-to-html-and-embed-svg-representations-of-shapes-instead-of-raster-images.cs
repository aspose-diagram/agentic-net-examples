using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputHtmlPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a StringBuilder for the HTML content
            StringBuilder htmlBuilder = new StringBuilder();

            // Basic HTML header
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html lang=\"en\">");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("    <meta charset=\"UTF-8\">");
            htmlBuilder.AppendLine("    <title>Visio Diagram Export (SVG)</title>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("    <h1>Visio Diagram Exported as SVG Shapes</h1>");

            // Iterate through pages and shapes
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                htmlBuilder.AppendLine($"    <h2>Page {pageIndex + 1}</h2>");

                foreach (Shape shape in page.Shapes)
                {
                    // Generate a unique SVG file name for each shape
                    string svgFileName = $"page{pageIndex + 1}_shape{shape.ID}.svg";
                    string svgFilePath = Path.Combine(outputDir, svgFileName);

                    // Export the shape to SVG
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    // Optional: configure SVG options if needed (defaults are fine)
                    shape.ToSvg(svgFilePath, svgOptions);

                    // Read the SVG content to embed inline
                    string svgContent = File.ReadAllText(svgFilePath);

                    // Embed the SVG inline within a div for layout
                    htmlBuilder.AppendLine("    <div style=\"margin:10px; display:inline-block; border:1px solid #ccc;\">");
                    htmlBuilder.AppendLine(svgContent);
                    htmlBuilder.AppendLine("    </div>");
                }
            }

            // Close HTML tags
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Write the final HTML file
            File.WriteAllText(outputHtmlPath, htmlBuilder.ToString(), Encoding.UTF8);

            // Dispose diagram to free resources
            diagram.Dispose();

            Console.WriteLine($"HTML file with embedded SVG shapes has been created at: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}