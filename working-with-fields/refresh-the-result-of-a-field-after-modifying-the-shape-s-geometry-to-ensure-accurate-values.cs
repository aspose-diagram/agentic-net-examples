using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // -----------------------------------------------------------------
            // Modify the shape's geometry: change width and height (in inches)
            // -----------------------------------------------------------------
            shape.XForm.Width.Value = 2.0;   // Set width to 2 inches
            shape.XForm.Height.Value = 2.0;  // Set height to 2 inches

            // ---------------------------------------------------------------
            // Refresh the shape's data to recalculate any fields dependent on geometry
            // ---------------------------------------------------------------
            shape.RefreshData();

            // ---------------------------------------------------------------
            // (Optional) Demonstrate that a field value has been updated
            // ---------------------------------------------------------------
            if (shape.Fields != null && shape.Fields.Count > 0)
            {
                // Iterate through all fields and output their current values
                for (int i = 0; i < shape.Fields.Count; i++)
                {
                    Field field = shape.Fields[i];
                    Console.WriteLine($"Field IX={field.IX}, Value=\"{field.Value.Val}\"");
                }
            }
            else
            {
                Console.WriteLine("The shape contains no fields to display.");
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}