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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <sourceMasterName> <targetMasterName> <outputVisioPath>");
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

            // Verify that the target master exists in the diagram
            bool targetMasterExists = false;
            foreach (Master master in diagram.Masters)
            {
                if (master.Name == targetMasterName)
                {
                    targetMasterExists = true;
                    break;
                }
            }

            if (!targetMasterExists)
            {
                Console.Error.WriteLine($"Target master \"{targetMasterName}\" not found in the diagram.");
                return;
            }

            // Iterate through each page and replace shapes that use the source master
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that need to be replaced to avoid modifying the collection during iteration
                List<long> shapesToReplace = new List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a master and compare its name
                    if (shape.Master != null && shape.Master.Name == sourceMasterName)
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Perform replacement for each collected shape
                foreach (long shapeId in shapesToReplace)
                {
                    // Retrieve the original shape
                    Shape oldShape = page.Shapes.GetShape(shapeId);

                    // Preserve geometric properties
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Mark the old shape as deleted
                    oldShape.Del = BOOL.True;

                    // Add a new shape using the target master at the same location
                    long newShapeId = page.AddShape(pinX, pinY, targetMasterName, false);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Apply the original size to the new shape
                    newShape.XForm.Width.Value = width;
                    newShape.XForm.Height.Value = height;
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