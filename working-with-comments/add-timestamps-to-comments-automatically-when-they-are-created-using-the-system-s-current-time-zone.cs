using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0). Adjust as needed.
            Page page = diagram.Pages[0];

            // Define comment coordinates (in inches). Adjust as needed.
            double commentX = 1.0;
            double commentY = 1.0;

            // Build the comment text with a timestamp using the system's current time zone.
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
            string commentText = $"Comment added at {timestamp}";

            // Add the comment to the page at the specified coordinates.
            page.AddComment(commentX, commentY, commentText);

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}