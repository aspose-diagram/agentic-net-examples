using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "rotated_circle.png";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the first page contains the circle shape
                Page page = diagram.Pages[0];

                // Find a shape that uses the "Ellipse" master (commonly used for circles)
                Shape circleShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        circleShape = shape;
                        break;
                    }
                }

                if (circleShape == null)
                {
                    Console.WriteLine("Circle shape not found.");
                    return;
                }

                // Set the local pivot (LocPin) to the geometric center of the shape
                // Width*0.5 and Height*0.5 evaluate to the center point
                circleShape.XForm.LocPinX.Ufe.F = "Width*0.5";
                circleShape.XForm.LocPinY.Ufe.F = "Height*0.5";

                // Rotate the shape by 45 degrees (Angle property expects degrees)
                circleShape.XForm.Angle.Value = 45;

                // Save the modified diagram as an image (PNG)
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Circle rotated and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }