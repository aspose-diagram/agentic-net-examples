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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape (you can change the ID as needed).
            Shape shape = page.Shapes[0];

            // -----------------------------------------------------------------
            // Center the shape's text using TxtPin (TextXForm) properties.
            // -----------------------------------------------------------------
            // 1. Set the text block size to match the shape's size.
            shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;
            shape.TextXForm.TxtHeight.Value = shape.XForm.Height.Value;

            // 2. Position the text block's pin at the shape's geometric center.
            shape.TextXForm.TxtPinX.Value = shape.XForm.PinX.Value;
            shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;

            // 3. Set the local pin of the text block to its center.
            shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value / 2.0;
            shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value / 2.0;

            // Refresh the shape to apply geometry changes.
            shape.RefreshData();

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}