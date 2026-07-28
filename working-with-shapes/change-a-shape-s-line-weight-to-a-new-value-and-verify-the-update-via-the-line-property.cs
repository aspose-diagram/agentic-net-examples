using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example: shape with ID 1)
            // Adjust the ID as needed for your specific diagram
            Shape shape = page.Shapes.GetShape(1);

            // Define the new line weight value (in points)
            double newLineWeight = 2.0;

            // Set the line weight using the DoubleValue property
            shape.Line.LineWeight.Value = newLineWeight;

            // Verify the update by reading back the value
            double actualLineWeight = shape.Line.LineWeight.Value;
            Console.WriteLine($"Line weight set to: {actualLineWeight}");

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
