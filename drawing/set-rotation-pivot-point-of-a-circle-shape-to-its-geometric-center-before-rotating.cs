using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [rotationAngle]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double rotationAngle = 45.0; // default rotation

        if (args.Length >= 3 && !double.TryParse(args[2], out rotationAngle))
        {
            Console.Error.WriteLine("Invalid rotation angle. Using default 45 degrees.");
            rotationAngle = 45.0;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            Shape targetShape = null;

            // Iterate through pages and shapes to find the first circle (ellipse master)
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];
                    // Ensure the shape has a master and check its name
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No circle (ellipse) shape found in the diagram.");
                return;
            }

            // Set the local pin (pivot) to the geometric center using UFE formulas
            // Width*0.5 and Height*0.5 represent the center of the shape
            targetShape.XForm.LocPinX.Ufe.F = "Width*0.5";
            targetShape.XForm.LocPinY.Ufe.F = "Height*0.5";

            // Apply the desired rotation angle (degrees)
            targetShape.XForm.Angle.Value = rotationAngle;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}