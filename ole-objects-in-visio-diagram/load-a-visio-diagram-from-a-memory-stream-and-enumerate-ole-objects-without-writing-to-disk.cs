using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio file into a memory buffer
            byte[] fileBytes = File.ReadAllBytes(inputPath);

            // Create a MemoryStream from the buffer
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Load the diagram from the memory stream
                using (Diagram diagram = new Diagram(memoryStream))
                {
                    // Enumerate all pages in the diagram
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Enumerate all shapes on the current page
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            // Check if the shape contains embedded OLE data
                            if (shape.ForeignData != null &&
                                shape.ForeignData.ObjectData != null &&
                                shape.ForeignData.ObjectData.Length > 0)
                            {
                                // Output details about the OLE object
                                Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, " +
                                                  $"OLE Source: {shape.ForeignData.ObjectSourceFullName}, " +
                                                  $"Data Size: {shape.ForeignData.ObjectData.Length} bytes");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}