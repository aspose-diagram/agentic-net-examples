using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that the required arguments (input and output file paths) are provided.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioFile> <outputVisioFile>");
            return;
        }

        // Assign the input file path and ensure the file exists.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign the output file path (no existence check needed, but ensure its directory exists).
        string outputPath = args[1];
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            // Create the output directory if it does not exist.
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram from the specified input file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the DistanceFromGround property to 15 points for the shape.
                    shape.ThreeDFormat.DistanceFromGround.Value = 15.0;
                }
            }

            // Save the modified diagram to the specified output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any exceptions that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}