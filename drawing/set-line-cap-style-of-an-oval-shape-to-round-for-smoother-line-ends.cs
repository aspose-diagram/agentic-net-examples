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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True) continue;

                    // Identify oval shapes.
                    // 1. By master name (most reliable).
                    // 2. Fallback: by shape name containing "Oval".
                    bool isOval = false;
                    if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                    {
                        if (shape.Master.Name.Equals("Oval", StringComparison.OrdinalIgnoreCase))
                            isOval = true;
                    }
                    if (!isOval && !string.IsNullOrEmpty(shape.NameU))
                    {
                        if (shape.NameU.IndexOf("Oval", StringComparison.OrdinalIgnoreCase) >= 0)
                            isOval = true;
                    }

                    if (isOval)
                    {
                        // Set the line cap style to round for smoother line ends.
                        shape.Line.LineCap.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}