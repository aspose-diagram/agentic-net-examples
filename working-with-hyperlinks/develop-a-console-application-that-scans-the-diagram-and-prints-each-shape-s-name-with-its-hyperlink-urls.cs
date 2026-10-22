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
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as a command-line argument.");
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
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Iterate through each hyperlink associated with the shape
                        foreach (Aspose.Diagram.Hyperlink hyperlink in shape.Hyperlinks)
                        {
                            // Retrieve the hyperlink address (URL or target)
                            string address = hyperlink.Address != null ? hyperlink.Address.Value : string.Empty;

                            // Output the shape name and its hyperlink URL
                            Console.WriteLine($"Shape: {shape.NameU} - Hyperlink: {address}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}