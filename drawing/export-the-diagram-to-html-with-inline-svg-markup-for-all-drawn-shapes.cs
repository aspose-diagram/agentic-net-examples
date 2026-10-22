using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare a temporary folder for per‑shape SVG files
        string svgFolder = Path.Combine(Path.GetDirectoryName(outputHtmlPath) ?? "", "svg_temp");
        try
        {
            if (!Directory.Exists(svgFolder))
                Directory.CreateDirectory(svgFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary SVG folder: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes, exporting each shape to an individual SVG file
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Enumerate shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    // Changed: use Equals to get a bool result (shape.Del is BOOL)
                    if (shape.Del.Equals(BOOL.True))
                        continue;

                    // Build a unique file name for the shape's SVG
                    string svgFileName = $"page{pageIndex + 1}_shape{shape.ID}.svg";
                    string svgFilePath = Path.Combine(svgFolder, svgFileName);

                    // Configure SVG save options (default options are sufficient for per‑shape export)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    svgOptions.SVGFitToViewPort = true; // Preserve viewbox (bool, not BOOL)

                    // Export the shape to SVG
                    shape.ToSvg(svgFilePath, svgOptions);
                }
            }

            // Build the final HTML with inline SVG markup
            using (StreamWriter htmlWriter = new StreamWriter(outputHtmlPath, false))
            {
                htmlWriter.WriteLine("<!DOCTYPE html>");
                htmlWriter.WriteLine("<html lang=\"en\">");
                htmlWriter.WriteLine("<head>");
                htmlWriter.WriteLine("    <meta charset=\"UTF-8\">");
                htmlWriter.WriteLine("    <title>Visio Diagram Export with Inline SVG</title>");
                htmlWriter.WriteLine("</head>");
                htmlWriter.WriteLine("<body>");
                htmlWriter.WriteLine("    <h1>Visio Diagram Exported as Inline SVG</h1>");

                // Embed each generated SVG file directly into the HTML
                string[] svgFiles = Directory.GetFiles(svgFolder, "*.svg");
                foreach (string svgPath in svgFiles)
                {
                    string svgContent = File.ReadAllText(svgPath);
                    htmlWriter.WriteLine("    <div class=\"shape-svg\">");
                    htmlWriter.WriteLine(svgContent);
                    htmlWriter.WriteLine("    </div>");
                    htmlWriter.WriteLine("    <hr/>");
                }

                htmlWriter.WriteLine("</body>");
                htmlWriter.WriteLine("</html>");
            }

            // Clean up temporary SVG files
            try
            {
                Directory.Delete(svgFolder, true);
            }
            catch
            {
                // If cleanup fails, ignore – the files are temporary.
            }

            Console.WriteLine($"HTML export completed successfully: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}
