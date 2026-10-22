using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Hyperlinks collection.
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Iterate through each hyperlink of the shape.
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Log shape identifier, hyperlink name and target address.
                            Console.WriteLine(
                                $"Shape ID {shape.ID}, Name \"{shape.Name}\": " +
                                $"Hyperlink Name = \"{link.Name}\", " +
                                $"Address = \"{link.Address.Value}\"");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}