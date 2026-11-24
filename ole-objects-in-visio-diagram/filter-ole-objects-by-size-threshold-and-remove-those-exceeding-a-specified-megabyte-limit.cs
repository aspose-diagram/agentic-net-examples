using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <sizeLimitInMB>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string sizeLimitArg = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse the size limit (megabytes) to bytes.
        if (!double.TryParse(sizeLimitArg, out double sizeLimitMb) || sizeLimitMb < 0)
        {
            Console.Error.WriteLine("Error: Size limit must be a non‑negative number representing megabytes.");
            return;
        }
        long sizeLimitBytes = (long)(sizeLimitMb * 1024 * 1024);

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            int totalRemoved = 0;

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes that need to be removed to avoid modifying the collection during iteration.
                List<Shape> shapesToRemove = new List<Shape>();

                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        long oleSize = shape.ForeignData.ObjectData.Length;

                        // If the OLE object's size exceeds the threshold, mark it for removal.
                        if (oleSize > sizeLimitBytes)
                        {
                            shapesToRemove.Add(shape);
                        }
                    }
                }

                // Remove the identified shapes from the page.
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                    totalRemoved++;
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Processing complete. Removed {totalRemoved} OLE object(s) exceeding {sizeLimitMb} MB.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}