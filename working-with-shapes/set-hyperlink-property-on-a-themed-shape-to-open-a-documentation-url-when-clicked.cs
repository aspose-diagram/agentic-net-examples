using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Find the first shape on the first page (as a representative themed shape).
            // In a real scenario you might locate a shape by its name, master, or theme.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape.
            // Shapes are accessed via GetShape(long id) where the id is returned by AddShape or stored in the collection.
            // Here we use the first shape's ID from the collection.
            long firstShapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(firstShapeId);

            // Create a new hyperlink that points to the documentation URL.
            Hyperlink link = new Hyperlink();
            link.Name = "DocumentationLink";
            link.Address.Value = "https://example.com/documentation";

            // Add the hyperlink to the shape's Hyperlinks collection.
            shape.Hyperlinks.Add(link);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Hyperlink added and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}