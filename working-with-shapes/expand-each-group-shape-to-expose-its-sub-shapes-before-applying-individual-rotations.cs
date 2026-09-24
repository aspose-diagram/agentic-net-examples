using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect all group shapes on the current page
                List<Shape> groupShapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Identify group shapes by their Type
                    if (shape.Type == TypeValue.Group)
                        groupShapes.Add(shape);
                }

                // Process each group shape
                foreach (Shape groupShape in groupShapes)
                {
                    // Capture IDs of sub‑shapes before ungrouping
                    List<long> subShapeIds = new List<long>();
                    foreach (Shape sub in groupShape.Shapes)
                        subShapeIds.Add(sub.ID);

                    // Ungroup the shape; this removes the group and promotes its children to the page level
                    groupShape.Ungroup();

                    // Rotate each former sub‑shape individually (example: 45 degrees)
                    foreach (long id in subShapeIds)
                    {
                        // Retrieve the shape now residing directly on the page
                        Shape subShape = page.Shapes.GetShape(id);
                        // Set rotation angle in degrees
                        subShape.XForm.Angle.Value = 45.0;
                    }
                }
            }

            // Save the modified diagram using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}