using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio file
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Find the target shape (example: shape with ID 1)
            // Adjust the ID or selection logic as needed
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape contains at least one field
            if (shape.Fields.Count > 0)
            {
                // Get the first field
                Field field = shape.Fields[0];

                // Set the formula to calculate area (Width * Height)
                field.Value.Ufev.F = "Width*Height";

                // Optionally, clear any existing format strings
                field.Format.Val = "";
                field.Format.Ufev.F = "";
            }
            else
            {
                Console.WriteLine("The selected shape does not contain any fields.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
