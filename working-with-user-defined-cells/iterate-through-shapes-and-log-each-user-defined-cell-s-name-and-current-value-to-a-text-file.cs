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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputLogPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the output directory exists.
            string? outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Open a StreamWriter for logging.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all user‑defined cells of the current shape.
                        foreach (User user in shape.Users)
                        {
                            // Build a log line containing shape ID, user‑defined cell name, value and optional prompt.
                            string line = $"ShapeID: {shape.ID}, UserNameU: {user.NameU}, Value: {user.Value?.Val ?? string.Empty}, Prompt: {user.Prompt?.Value ?? string.Empty}";
                            writer.WriteLine(line);
                        }
                    }
                }
            }

            Console.WriteLine($"User‑defined cells have been logged to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}