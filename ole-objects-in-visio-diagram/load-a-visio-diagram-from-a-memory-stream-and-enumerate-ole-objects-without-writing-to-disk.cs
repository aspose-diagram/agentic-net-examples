using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Read the Visio file into a byte array.
            byte[] fileBytes = File.ReadAllBytes(inputPath);

            // Load the diagram from a memory stream (no disk I/O for loading).
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // The Diagram constructor that accepts a Stream is valid.
                Diagram diagram = new Diagram(memoryStream);

                // Enumerate all pages in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Enumerate all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // OLE objects are represented by foreign data.
                        if (shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Output basic information about the OLE object.
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, OLE Data Size: {shape.ForeignData.ObjectData.Length} bytes");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or enumeration.
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }
}