using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, shape name, rotation angle (degrees), output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <shapeName> <angleDegrees> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string shapeName = args[1];
        string angleArg = args[2];
        string outputPath = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(angleArg, out double angleDegrees))
        {
            Console.Error.WriteLine($"Invalid rotation angle: {angleArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by its NameU property
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (shape.NameU == shapeName)
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
                Console.Error.WriteLine($"Shape with name '{shapeName}' not found.");
                return;
            }

            // Set the rotation angle (degrees) on the shape's XForm
            targetShape.XForm.Angle.Value = angleDegrees;

            // Save the modified diagram (using VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape '{shapeName}' rotated to {angleDegrees} degrees and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}