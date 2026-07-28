using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Identify the shape to modify (example: shape with ID = 1)
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Convert 2.5 centimeters to inches (1 cm = 0.393700787 inches)
            double heightInInches = 2.5 * 0.393700787;

            // Set the shape's height using the SetHeight method
            shape.SetHeight(heightInInches);
            Console.WriteLine($"Shape height set to {heightInInches} inches (2.5 cm).");

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
