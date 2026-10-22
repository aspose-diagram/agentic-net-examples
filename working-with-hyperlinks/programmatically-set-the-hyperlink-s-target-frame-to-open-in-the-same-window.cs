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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page in the diagram.
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Verify that the shape has a Hyperlinks collection.
            if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
            {
                // Iterate over each hyperlink and set it to open in the same window.
                for (int i = 0; i < shape.Hyperlinks.Count; i++)
                {
                    Hyperlink link = shape.Hyperlinks[i];

                    // The NewWindow property is a cell; set its value to FALSE.
                    link.NewWindow.Value = BOOL.False;
                }

                Console.WriteLine($"Updated {shape.Hyperlinks.Count} hyperlink(s) on shape ID {shape.ID} to open in the same window.");
            }
            else
            {
                Console.WriteLine("No hyperlinks found on the selected shape.");
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}