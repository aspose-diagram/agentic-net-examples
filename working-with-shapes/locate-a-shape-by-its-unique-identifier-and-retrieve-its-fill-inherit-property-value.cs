using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file
        string filePath = "input.vsdx";
        // Verify the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Unique identifier of the shape to locate (replace with actual ID)
            long targetShapeId = 12345;

            // Retrieve the shape from the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Determine if the shape's fill values are inherited.
            // Compare each fill cell with its corresponding inherited value.
            bool isFillInherited =
                shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value &&
                shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

            string inheritValue = isFillInherited ? "True" : "False";

            Console.WriteLine($"Shape ID: {targetShapeId}");
            Console.WriteLine($"Fill.Inherit property value (derived): {inheritValue}");
        }
        catch (Exception ex)
        {
            // Output any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}