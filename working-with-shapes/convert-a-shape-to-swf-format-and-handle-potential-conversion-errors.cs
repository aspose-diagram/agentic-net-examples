using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        string outputPath = "shape.swf";

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Locate the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No suitable shape found in the diagram.");
                return;
            }

            // Create a new diagram that will contain only the selected shape
            Diagram singleShapeDiagram = new Diagram();

            // Ensure the master of the target shape exists in the new diagram
            if (targetShape.Master != null)
            {
                singleShapeDiagram.AddMaster(diagram, targetShape.Master.Name);
            }

            // Add the shape to the new diagram's first page
            Page newPage = singleShapeDiagram.Pages[0];
            long newShapeId = newPage.AddShape(
                targetShape.XForm.PinX.Value,
                targetShape.XForm.PinY.Value,
                targetShape.Master?.Name ?? "Rectangle",
                false);

            // Retrieve the newly added shape instance
            Shape newShape = newPage.Shapes.GetShape(newShapeId);

            // Copy the text content from the original shape to the new shape
            newShape.Text.Value.Clear();
            foreach (var item in targetShape.Text.Value)
            {
                if (item is Txt txt)
                {
                    newShape.Text.Value.Add(new Txt(txt.Text));
                }
            }

            // Configure SWF save options
            SWFSaveOptions swfOptions = new SWFSaveOptions
            {
                DefaultFont = "Arial",
                ViewerIncluded = true
            };

            // Save the new diagram (containing only the selected shape) as SWF
            singleShapeDiagram.Save(outputPath, swfOptions);

            Console.WriteLine($"Shape successfully exported to SWF at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
