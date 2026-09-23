using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file
        string diagramPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is a foreign (OLE) object and has valid foreign data
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Retrieve the source file name (may include path or extension)
                        string sourceFileName = shape.ForeignData.ObjectSourceFullName ?? "Unknown";

                        // OLE creation date is not directly exposed by Aspose.Diagram.
                        // For audit purposes we note that this information is unavailable.
                        string creationDateInfo = "Creation date not available via Aspose.Diagram API";

                        // Output the metadata; Page.Name is a string, not a cell with .Value
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
                        Console.WriteLine($"  Source File: {sourceFileName}");
                        Console.WriteLine($"  Creation Date: {creationDateInfo}");
                        Console.WriteLine();
                    }
                }
            }

            // No modifications are made, so saving is optional and omitted.
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}