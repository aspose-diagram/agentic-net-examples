using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

        // Guard against missing input file
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
                    // Collect group shapes to avoid modifying the collection while iterating
                    List<Shape> groups = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify group shapes by TypeValue.Group
                        if (shape.Type == TypeValue.Group)
                        {
                            groups.Add(shape);
                        }
                    }

                    // Process each group shape
                    foreach (Shape groupShape in groups)
                    {
                        // Compute group's local pin offsets (used for absolute positioning)
                        double groupPinX = groupShape.XForm.PinX.Value;
                        double groupPinY = groupShape.XForm.PinY.Value;
                        double groupLocPinX = groupShape.XForm.LocPinX.Value;
                        double groupLocPinY = groupShape.XForm.LocPinY.Value;

                        // Iterate over child shapes within the group
                        foreach (Shape child in groupShape.Shapes)
                        {
                            // Ensure the child has a master (required for AddShape)
                            if (child.Master == null)
                                continue;

                            // Calculate absolute position of the child shape
                            double absolutePinX = groupPinX + child.XForm.PinX.Value - groupLocPinX;
                            double absolutePinY = groupPinY + child.XForm.PinY.Value - groupLocPinY;

                            // Add a new independent shape to the page using the child's master name
                            long newShapeId = page.AddShape(absolutePinX, absolutePinY, child.Master.Name, false);
                            Shape newShape = page.Shapes.GetShape(newShapeId);

                            // Copy size
                            newShape.XForm.Width.Value = child.XForm.Width.Value;
                            newShape.XForm.Height.Value = child.XForm.Height.Value;

                            // Copy fill properties (foreground color and pattern)
                            newShape.Fill.FillForegnd.Value = child.Fill.FillForegnd.Value;
                            newShape.Fill.FillPattern.Value = child.Fill.FillPattern.Value;

                            // Copy line properties (color and weight)
                            newShape.Line.LineColor.Value = child.Line.LineColor.Value;
                            newShape.Line.LineWeight.Value = child.Line.LineWeight.Value;

                            // Copy text content
                            newShape.Text.Value.Clear();
                            foreach (var item in child.Text.Value)
                            {
                                if (item is Txt txt)
                                {
                                    newShape.Text.Value.Add(new Txt(txt.Text));
                                }
                            }
                        }

                        // Mark the original group shape as deleted
                        groupShape.Del = BOOL.True;
                    }
                }

                // Save the flattened diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}