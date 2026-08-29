using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that both input and output file paths are provided.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputCsvPath>");
            return;
        }

        // Assign input and output paths from command‑line arguments.
        string inputPath = args[0];
        // Guard: ensure the Visio file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure the directory for the CSV exists (create if necessary).
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Open a StreamWriter for the CSV output (overwrites existing file).
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header.
                writer.WriteLine("ShapeID,Text");

                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the plain text of the shape.
                        string text = shape.Text.Value.Text;

                        // Normalize text: replace line breaks and commas to keep CSV well‑formed.
                        if (!string.IsNullOrEmpty(text))
                        {
                            text = text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
                            text = text.Replace(",", " "); // Simple comma removal.
                        }

                        // Escape double quotes by doubling them, then wrap the field in quotes.
                        string escapedText = $"\"{text.Replace("\"", "\"\"")}\"";

                        // Write the shape ID and its text to the CSV.
                        writer.WriteLine($"{shape.ID},{escapedText}");
                    }
                }
            }

            // Inform the user that processing completed successfully.
            Console.WriteLine($"Extraction completed. CSV saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during loading or processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}