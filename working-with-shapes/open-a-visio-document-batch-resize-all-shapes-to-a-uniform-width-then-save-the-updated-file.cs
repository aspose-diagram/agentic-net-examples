using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [uniformWidth]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Optional uniform width argument; default to 2.0 if not provided or invalid.
        double uniformWidth = 2.0;
        if (args.Length >= 3 && double.TryParse(args[2], out double parsedWidth))
        {
            uniformWidth = parsedWidth;
        }

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply the uniform width to the shape.
                        shape.XForm.Width.Value = uniformWidth;
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram resized and saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}