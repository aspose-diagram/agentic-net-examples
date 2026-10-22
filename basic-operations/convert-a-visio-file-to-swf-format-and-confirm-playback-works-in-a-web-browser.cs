using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output directory.
        string outputDir = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);
        if (string.IsNullOrEmpty(outputDir))
        {
            outputDir = Directory.GetCurrentDirectory();
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Unable to create output directory '{outputDir}'. {ex.Message}");
                return;
            }
        }

        // Build output file paths.
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string swfPath = Path.Combine(outputDir, fileNameWithoutExt + ".swf");
        string htmlPath = Path.Combine(outputDir, fileNameWithoutExt + ".html");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Export the diagram to SWF format.
            diagram.Save(swfPath, SaveFileFormat.Swf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            return;
        }

        // Verify that the SWF file was created.
        if (!File.Exists(swfPath))
        {
            Console.Error.WriteLine($"Error: SWF file was not created at {swfPath}");
            return;
        }

        // Generate a simple HTML page that embeds the SWF for browser playback verification.
        try
        {
            string relativeSwf = Path.GetFileName(swfPath); // Use relative path for HTML.
            string htmlContent = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>SWF Playback Test - {fileNameWithoutExt}</title>
</head>
<body>
    <h2>SWF Playback Test</h2>
    <object data=""{relativeSwf}"" type=""application/x-shockwave-flash"" width=""800"" height=""600"">
        <param name=""movie"" value=""{relativeSwf}"" />
        <embed src=""{relativeSwf}"" width=""800"" height=""600"" type=""application/x-shockwave-flash""></embed>
        Your browser does not support SWF playback.
    </object>
    <p>If the SWF does not display, ensure that your browser has Flash support enabled.</p>
</body>
</html>";
            File.WriteAllText(htmlPath, htmlContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating HTML file: {ex.Message}");
            return;
        }

        // Inform the user of successful conversion and where to view the result.
        Console.WriteLine($"Conversion successful.");
        Console.WriteLine($"SWF file: {swfPath}");
        Console.WriteLine($"HTML test page: {htmlPath}");
        Console.WriteLine("Open the HTML file in a web browser to verify SWF playback.");
    }
}