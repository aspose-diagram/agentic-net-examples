using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Determine output directory (default to current directory)
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Error: Output directory does not exist at '{outputDir}'.");
            return;
        }

        // Define output SWF and HTML file paths
        string swfPath = Path.Combine(outputDir, "output.swf");
        string htmlPath = Path.Combine(outputDir, "preview.html");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as SWF
            diagram.Save(swfPath, SaveFileFormat.Swf);
            Console.WriteLine($"SWF file saved to: {swfPath}");

            // Generate a simple HTML page to embed the SWF for browser playback
            string htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>SWF Preview</title>
</head>
<body>
    <h2>SWF Preview</h2>
    <object width=""800"" height=""600"" data=""{Path.GetFileName(swfPath)}"" type=""application/x-shockwave-flash"">
        <embed src=""{Path.GetFileName(swfPath)}"" width=""800"" height=""600"" type=""application/x-shockwave-flash""></embed>
        Your browser does not support SWF playback.
    </object>
</body>
</html>";
            File.WriteAllText(htmlPath, htmlContent);
            Console.WriteLine($"HTML preview file saved to: {htmlPath}");
            Console.WriteLine("Open the HTML file in a web browser to verify SWF playback.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}