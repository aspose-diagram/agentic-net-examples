using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether a triangle was found
            bool triangleFound = false;

            // Iterate through all pages and shapes to locate a triangle shape
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate shapes using the Shapes collection
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Ensure the shape has a master (template) before accessing its name
                    if (shape.Master != null && shape.Master.Name != null && shape.Master.Name.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        // Rotate the shape 45 degrees clockwise around its center (pin point)
                        shape.XForm.Angle.Value = 45.0;

                        triangleFound = true;
                        // Assuming only one triangle needs rotation; break out of loops
                        break;
                    }
                }

                if (triangleFound)
                    break;
            }

            if (!triangleFound)
            {
                Console.Error.WriteLine("No triangle shape was found in the diagram.");
                // Continue to save the diagram unchanged if desired, or exit
                // return;
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}