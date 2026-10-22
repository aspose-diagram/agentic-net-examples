using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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

            // Process shapes on the first page.
            Page page = diagram.Pages[0];

            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Apply a 90‑degree rotation to the shape's text.
                // TxtAngle expects radians.
                shape.TextXForm.TxtAngle.Value = Math.PI / 2.0;

                // Reposition text to the left side of the shape.
                // According to Aspose.Diagram guidelines:
                //   TxtLocPinX = TxtWidth (align left)
                //   TxtPinX   = 0 (origin at left edge)
                shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value;
                shape.TextXForm.TxtPinX.Value = 0;

                // Refresh shape data to apply changes.
                shape.RefreshData();
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}