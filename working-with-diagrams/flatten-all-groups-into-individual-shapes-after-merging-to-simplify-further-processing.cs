using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect all group shapes on the current page
                    var groupShapes = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        // Detect a group shape using the Type property
                        if (shape.Type == TypeValue.Group)
                        {
                            groupShapes.Add(shape);
                        }
                    }

                    // Process each group shape
                    foreach (Shape group in groupShapes)
                    {
                        // Copy child shapes out of the group
                        var childShapes = new List<Shape>(group.Shapes);
                        foreach (Shape child in childShapes)
                        {
                            // Compute absolute position for the child shape
                            double absolutePinX = group.XForm.PinX.Value + child.XForm.PinX.Value - group.XForm.LocPinX.Value;
                            double absolutePinY = group.XForm.PinY.Value + child.XForm.PinY.Value - group.XForm.LocPinY.Value;

                            // Apply the absolute position to the child shape
                            child.XForm.PinX.Value = absolutePinX;
                            child.XForm.PinY.Value = absolutePinY;

                            // Add the child shape to the page's shape collection
                            page.Shapes.Add(child);
                        }

                        // Mark the original group shape as deleted
                        group.Del = BOOL.True;
                    }
                }

                // Save the flattened diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
