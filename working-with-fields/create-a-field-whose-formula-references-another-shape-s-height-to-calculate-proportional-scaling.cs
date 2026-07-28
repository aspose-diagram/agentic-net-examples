using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with actual path or pass via args)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Retrieve the first two shapes on the page to act as source and target
            // (In a real scenario you would locate shapes by name or ID)
            Shape[] shapes = new Shape[2];
            int index = 0;
            foreach (Shape s in page.Shapes)
            {
                if (index < 2)
                {
                    shapes[index++] = s;
                }
                else
                {
                    break;
                }
            }

            // Ensure we have two shapes to work with
            if (shapes[0] == null || shapes[1] == null)
            {
                Console.Error.WriteLine("The diagram does not contain at least two shapes.");
                return;
            }

            Shape targetShape = shapes[0];   // Shape that will receive the field
            Shape referenceShape = shapes[1]; // Shape whose Height will be referenced

            // Create a new field (text insertion field) for the target shape
            Field field = new Field();

            // Build a formula that multiplies the target shape's Height by the reference shape's Height
            // Visio formula syntax: Sheet.<ShapeID>!Height references another shape's Height cell
            field.Value.Ufev.F = $"Height*Sheet.{referenceShape.ID}!Height";

            // Optionally set a default display value (e.g., "0") to avoid empty field before evaluation
            field.Value.Val = "0";

            // Add the field to the target shape's Fields collection
            targetShape.Fields.Add(field);

            // Save the modified diagram to the output file using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}