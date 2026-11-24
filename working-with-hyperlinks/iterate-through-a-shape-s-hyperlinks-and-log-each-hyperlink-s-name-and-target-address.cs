using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection.
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink of the shape.
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Retrieve hyperlink name; may be null.
                        string linkName = link.Name ?? string.Empty;

                        // Retrieve hyperlink address; the Address property is a wrapper with a Value field.
                        string linkAddress = link.Address != null && link.Address.Value != null
                            ? link.Address.Value
                            : string.Empty;

                        // Log the shape ID, hyperlink name, and target address.
                        Console.WriteLine($"Shape ID: {shape.ID}, Hyperlink Name: {linkName}, Address: {linkAddress}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}