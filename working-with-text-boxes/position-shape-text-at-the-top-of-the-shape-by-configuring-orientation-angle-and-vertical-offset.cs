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

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (adjust the ID as needed)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 1 not found.");
                return;
            }

            // Set text rotation angle (degrees -> radians)
            double angleDeg = 0; // No rotation
            shape.TextXForm.TxtAngle.Value = (Math.PI / 180) * angleDeg;

            // Position text at the top of the shape
            // Local pin Y at the top of the text block
            shape.TextXForm.TxtLocPinY.Value = 0;
            // Pin Y at the shape's height to align the text block with the top edge
            shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value;

            // Optional: replace existing text with a new string
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Top Aligned Text"));

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
