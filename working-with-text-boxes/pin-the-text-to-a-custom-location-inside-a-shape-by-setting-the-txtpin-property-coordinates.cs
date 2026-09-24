using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: PinX, PinY, master name, isCalculate (bool)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add new text
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Pinned Text"));

            // Set custom text pin coordinates (relative to the shape)
            // These values are in inches; adjust as needed
            shape.TextXForm.TxtPinX.Value = 0.5; // X offset within the shape
            shape.TextXForm.TxtPinY.Value = 0.3; // Y offset within the shape

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
