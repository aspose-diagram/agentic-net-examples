using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure an input file path is provided.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null)
                    {
                        // Access the embedded OLE binary data.
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Log a warning if the OLE data is null or empty.
                        if (oleData == null || oleData.Length == 0)
                        {
                            Console.WriteLine($"Warning: OLE object in shape ID {shape.ID} on page ID {page.ID} has empty data.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}