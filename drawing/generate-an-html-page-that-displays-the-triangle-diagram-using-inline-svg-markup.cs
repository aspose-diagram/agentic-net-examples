using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string visioPath = args[0];
        string htmlPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Create a temporary SVG file path.
            string tempSvgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".svg");

            // Export the diagram to SVG format.
            diagram.Save(tempSvgPath, SaveFileFormat.Svg);

            // Read the generated SVG markup.
            string svgContent = File.ReadAllText(tempSvgPath);

            // Build a simple HTML page that embeds the SVG inline.
            string htmlContent = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Triangle Diagram</title>
</head>
<body>
" + svgContent + @"
</body>
</html>";

            // Write the HTML file.
            File.WriteAllText(htmlPath, htmlContent);

            // Clean up the temporary SVG file.
            File.Delete(tempSvgPath);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}