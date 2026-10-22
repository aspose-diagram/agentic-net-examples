using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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

            // Get the active page (the page where the triangle resides).
            Page page = diagram.ActivePage;

            // Locate the first shape whose master name is "Triangle".
            Shape triangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master before accessing its name.
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                Console.Error.WriteLine("Triangle shape not found in the diagram.");
                return;
            }

            // Rotate the triangle 45 degrees clockwise around its center (pin point).
            // The Angle cell expects degrees directly.
            triangleShape.XForm.Angle.Value = 45.0;

            // Save the modified diagram. Using VSDX format for broad compatibility.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}