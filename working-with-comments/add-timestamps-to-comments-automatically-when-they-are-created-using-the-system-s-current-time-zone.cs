using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Define comment coordinates (example: 1.0, 1.0)
            double commentX = 1.0;
            double commentY = 1.0;

            // Base comment text (could be customized)
            string baseComment = "Automated comment";

            // Append timestamp using the system's current time zone
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
            string fullComment = $"{baseComment} (Created: {timestamp})";

            // Add the comment to the page at the specified coordinates
            page.AddComment(commentX, commentY, fullComment);

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Comment added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}