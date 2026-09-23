using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <VisioFilePath>");
            return;
        }

        // Retrieve the Visio file path from the first argument.
        string visioFilePath = args[0];
        // Guard: verify the file exists before proceeding.
        if (!File.Exists(visioFilePath))
        {
            Console.Error.WriteLine($"File not found: {visioFilePath}");
            return;
        }

        // Load the Visio file bytes from disk (simulating an external source).
        byte[] visioFileBytes = GetVisioFileBytes(visioFilePath);

        // Wrap Aspose operations in a try/catch to handle potential errors gracefully.
        try
        {
            // Load the diagram from a memory stream without writing to disk.
            using (MemoryStream stream = new MemoryStream(visioFileBytes))
            {
                Diagram diagram = new Diagram(stream);

                // Iterate through all pages and shapes to locate OLE (foreign) objects.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign (OLE) shape and has foreign data.
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            // Ensure the foreign data represents an embedded OLE object.
                            if (shape.ForeignData.ForeignType == ForeignType.Object)
                            {
                                // Retrieve the OLE binary data.
                                byte[] oleData = shape.ForeignData.ObjectData;
                                if (oleData != null && oleData.Length > 0)
                                {
                                    // Wrap the binary data in a MemoryStream for further processing if needed.
                                    using (MemoryStream oleStream = new MemoryStream(oleData))
                                    {
                                        Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}");
                                        Console.WriteLine($"OLE Object Size: Width={shape.ForeignData.ObjectWidth}, Height={shape.ForeignData.ObjectHeight}");
                                        Console.WriteLine($"OLE Data Length: {oleData.Length} bytes");
                                        // Additional processing of oleStream can be performed here.
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during diagram processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Reads the Visio file from the specified path and returns its byte array.
    static byte[] GetVisioFileBytes(string path)
    {
        // ReadAllBytes may throw, but the caller already verified the file exists.
        return File.ReadAllBytes(path);
    }
}