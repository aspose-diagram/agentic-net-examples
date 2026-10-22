using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: diagram path, shape name, output directory
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeNameU> <outputDirectory>");
            return;
        }

        string diagramPath = args[0];
        string shapeNameU = args[1];
        string outputDir = args[2];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (or active page) – adjust if needed
            Page page = diagram.Pages[0];

            // Locate the shape by its NameU property
            Shape targetShape = null;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shp = page.Shapes[i];
                if (shp.NameU == shapeNameU)
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with NameU '{shapeNameU}' not found on the first page.");
                return;
            }

            // ---------- Export the shape to HTML ----------
            string htmlPath = Path.Combine(outputDir, $"{shapeNameU}.html");
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            targetShape.ToHTML(htmlPath, htmlOptions);
            Console.WriteLine($"Shape exported to HTML: {htmlPath}");

            // ---------- Export the shape to PNG (fallback image) ----------
            string pngPath = Path.Combine(outputDir, $"{shapeNameU}.png");
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            targetShape.ToImage(pngPath, imgOptions);
            Console.WriteLine($"Shape exported to PNG (fallback image): {pngPath}");

            // ---------- Export the whole diagram to SWF ----------
            // Note: Aspose.Diagram does not provide a direct shape‑to‑SWF export,
            // so we export the entire diagram. The SWF will contain the shape.
            string swfPath = Path.Combine(outputDir, $"{shapeNameU}.swf");
            diagram.Save(swfPath, SaveFileFormat.Swf);
            Console.WriteLine($"Diagram exported to SWF: {swfPath}");

            // ---------- Generate an HTML wrapper that embeds the SWF with fallback ----------
            string wrapperHtmlPath = Path.Combine(outputDir, $"{shapeNameU}_wrapper.html");
            string wrapperHtmlContent = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Shape {shapeNameU} – SWF with fallback</title>
</head>
<body>
    <object data=""{Path.GetFileName(swfPath)}"" type=""application/x-shockwave-flash"" width=""800"" height=""600"">
        <!-- Fallback image for browsers that do not support SWF -->
        <img src=""{Path.GetFileName(pngPath)}"" alt=""Fallback image for shape {shapeNameU}"" width=""800"" height=""600"" />
        <p>Your browser does not support Flash content.</p>
    </object>
</body>
</html>";
            File.WriteAllText(wrapperHtmlPath, wrapperHtmlContent);
            Console.WriteLine($"HTML wrapper with embedded SWF created: {wrapperHtmlPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}