using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Retrieve the OLE binary data.
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Validate that the OLE data is not null or empty.
                        if (oleData == null || oleData.Length == 0)
                        {
                            // Log a warning for empty OLE data.
                            Console.WriteLine($"Warning: OLE object in shape ID {shape.ID} on page ID {page.ID} has empty data.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}