using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate the first rectangle shape by its master name
            Shape rectangle = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    rectangle = shape;
                    break;
                }
            }

            if (rectangle == null)
            {
                Console.Error.WriteLine("Rectangle shape not found.");
                return;
            }

            // NOTE: Aspose.Diagram does not expose a direct Wrap property on TextBlock.
            // Text wrapping is handled automatically based on the shape's width,
            // so no explicit property assignment is required.

            // Clear any existing text and add a long sentence to demonstrate wrapping
            rectangle.Text.Value.Clear();
            rectangle.Text.Value.Add(new Txt("This is a very long sentence that should automatically wrap inside the rectangle shape to improve readability."));

            // Define output file path
            string outputPath = "output.vsdx";

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with text wrapping enabled.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}