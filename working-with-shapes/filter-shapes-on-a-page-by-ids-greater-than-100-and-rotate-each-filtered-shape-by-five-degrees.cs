using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Process the first page (index 0)
            Page page = diagram.Pages[0];

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Filter shapes with ID greater than 100
                if (shape.ID > 100)
                {
                    // Rotate the shape by adding 5 degrees to its current angle
                    // Angle is stored in degrees directly
                    shape.XForm.Angle.Value = shape.XForm.Angle.Value + 5.0;
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}