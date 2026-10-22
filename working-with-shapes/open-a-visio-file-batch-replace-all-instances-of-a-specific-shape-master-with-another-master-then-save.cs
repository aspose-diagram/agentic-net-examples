using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, source master name, target master name, output file
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <sourceMasterName> <targetMasterName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string sourceMasterName = args[1];
        string targetMasterName = args[2];
        string outputPath = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that use the source master
                List<long> shapesToReplace = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and compare its name
                    if (shape.Master != null && shape.Master.Name == sourceMasterName)
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Replace each identified shape with a new shape based on the target master
                foreach (long shapeId in shapesToReplace)
                {
                    // Retrieve the original shape
                    Shape originalShape = page.Shapes.GetShape(shapeId);
                    if (originalShape == null)
                        continue; // Safety check

                    // Store position and size of the original shape
                    double pinX = originalShape.XForm.PinX.Value;
                    double pinY = originalShape.XForm.PinY.Value;
                    double width = originalShape.XForm.Width.Value;
                    double height = originalShape.XForm.Height.Value;

                    // Mark the original shape as deleted
                    originalShape.Del = BOOL.True;

                    // Add a new shape using the target master at the same position
                    // The fourth parameter 'isCalculate' is set to false as per API requirements
                    long newShapeId = page.AddShape(pinX, pinY, targetMasterName, false);

                    // Retrieve the newly added shape to copy size (optional)
                    Shape newShape = page.Shapes.GetShape(newShapeId);
                    if (newShape != null)
                    {
                        newShape.XForm.Width.Value = width;
                        newShape.XForm.Height.Value = height;
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}