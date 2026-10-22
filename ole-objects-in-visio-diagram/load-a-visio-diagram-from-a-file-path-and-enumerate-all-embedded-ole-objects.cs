using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command-line argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains embedded OLE data via ForeignData
                    if (shape.ForeignData != null)
                    {
                        // Retrieve the source information of the OLE object
                        string oleSource = shape.ForeignData.ObjectSourceFullName ?? "Unknown";

                        // Retrieve the raw OLE binary data (may be null)
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Output details about the OLE object
                        Console.WriteLine(
                            $"Page: {page.NameU} (ID: {page.ID}) | " +
                            $"Shape ID: {shape.ID} | " +
                            $"OLE Source: {oleSource} | " +
                            $"Data Size: {(oleData != null ? oleData.Length : 0)} bytes");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or enumeration to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}