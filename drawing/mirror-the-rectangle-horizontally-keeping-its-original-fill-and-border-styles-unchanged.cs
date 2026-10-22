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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find rectangle shapes.
            foreach (Page page in diagram.Pages)
            {
                // Use the Shapes collection to access each shape by its ID.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and that the master name is "Rectangle".
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Mirror the rectangle horizontally by setting the FlipX cell to TRUE.
                        // This operation does not affect fill or line styles.
                        shape.XForm.FlipX.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}