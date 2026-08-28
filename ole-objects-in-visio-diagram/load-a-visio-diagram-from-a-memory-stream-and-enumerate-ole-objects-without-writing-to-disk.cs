using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the Visio file path: use first command‑line argument or a default placeholder.
        string visioFilePath = args.Length > 0 ? args[0] : "example.vsdx";
        // Guard to ensure the file exists before attempting to read it.
        if (!File.Exists(visioFilePath))
        {
            Console.Error.WriteLine($"File not found: {visioFilePath}");
            return;
        }

        // Load the Visio file bytes from disk (reading only, no writing involved).
        byte[] visioFileBytes = GetVisioFileBytes(visioFilePath);

        // Wrap Aspose.Diagram operations in a try/catch to capture any runtime errors.
        try
        {
            // Load the Visio diagram from the memory stream.
            using (MemoryStream memoryStream = new MemoryStream(visioFileBytes))
            {
                Diagram diagram = new Diagram(memoryStream);

                // Iterate through all pages and shapes to locate OLE (foreign) objects.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign (OLE) shape and that it contains an OLE object.
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Retrieve the embedded OLE binary data.
                            byte[] oleData = shape.ForeignData.ObjectData;

                            // Ensure the data is present before processing.
                            if (oleData != null && oleData.Length > 0)
                            {
                                Console.WriteLine("OLE object found:");
                                Console.WriteLine($"  Page Name : {page.Name}");
                                Console.WriteLine($"  Shape ID  : {shape.ID}");
                                Console.WriteLine($"  Data Size : {oleData.Length} bytes");
                            }
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

    // Reads the Visio file bytes from the specified path.
    static byte[] GetVisioFileBytes(string path)
    {
        // Guard to ensure the file exists (redundant if called after earlier check, but kept for safety).
        if (!File.Exists(path))
        {
            Console.Error.WriteLine($"File not found: {path}");
            return Array.Empty<byte>();
        }

        // Return the file contents as a byte array.
        return File.ReadAllBytes(path);
    }
}