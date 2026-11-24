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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

            // Current timestamp to be written to each shape.
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the EventAfterUpdate (closest available) cell.
                    // Using EventXFMod as the nearest equivalent event cell.
                    // The formula stores the timestamp as a literal string.
                    shape.Event.EventXFMod.Ufe.F = $"\"{timestamp}\"";
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}