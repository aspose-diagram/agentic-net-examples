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

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume the circle shape is on the first page and is the first shape.
            // Adjust the retrieval logic as needed for your specific diagram.
            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                throw new Exception("No shapes found on the first page.");
            }

            // Retrieve the shape (by index 0). Cast the returned shape ID to int for GetShape.
            long shapeId = page.Shapes[0].ID;
            Shape circle = page.Shapes.GetShape((int)shapeId);

            // Verify that the shape is an ellipse (circle) based on its master name.
            if (circle.Master == null || !circle.Master.Name.Contains("Ellipse"))
            {
                throw new Exception("The selected shape is not an ellipse/circle.");
            }

            // Set the local pin (pivot) to the geometric center of the shape.
            // Using formulas that calculate half of the width and height.
            circle.XForm.LocPinX.Ufe.F = "Width*0.5";
            circle.XForm.LocPinY.Ufe.F = "Height*0.5";

            // Rotate the shape 45 degrees (SetAngle expects radians).
            double angleDegrees = 45.0;
            double angleRadians = (Math.PI / 180.0) * angleDegrees;
            circle.SetAngle(angleRadians);

            // Save the modified diagram.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Optionally export the rotated circle as an image.
            string imagePath = "circle.png";
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the specific shape to an image.
            circle.ToImage(imagePath, imgOptions);

            Console.WriteLine("Rotation pivot set and shape rotated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
