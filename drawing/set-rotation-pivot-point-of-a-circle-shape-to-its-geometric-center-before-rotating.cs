using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, rotation angle in degrees
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <angleDegrees>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string angleArg = args[2];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(angleArg, out double rotationAngle))
        {
            Console.Error.WriteLine($"Invalid rotation angle: {angleArg}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find a circle (ellipse master)
            foreach (Page page in diagram.Pages)
            {
                // Use GetShape to retrieve each shape by its ID
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master (i.e., not a group or foreign object)
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        // Set the local pin (pivot) to the geometric center of the shape
                        // LocPinX/Y formulas use the shape's Width and Height cells
                        shape.XForm.LocPinX.Ufe.F = "Width*0.5";
                        shape.XForm.LocPinY.Ufe.F = "Height*0.5";

                        // Apply the desired rotation angle (degrees)
                        shape.XForm.Angle.Value = rotationAngle;

                        // If multiple circles exist and only the first should be rotated, uncomment the line below
                        // break;
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}