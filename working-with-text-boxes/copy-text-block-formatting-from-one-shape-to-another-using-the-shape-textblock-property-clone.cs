using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume source and target shape IDs are known
            long sourceShapeId = 1; // replace with actual source shape ID
            long targetShapeId = 2; // replace with actual target shape ID

            // Retrieve the shapes from the first page
            Page page = diagram.Pages[0];
            Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
            Shape targetShape = page.Shapes.GetShape(targetShapeId);

            if (sourceShape == null)
            {
                throw new Exception($"Source shape with ID {sourceShapeId} not found.");
            }

            if (targetShape == null)
            {
                throw new Exception($"Target shape with ID {targetShapeId} not found.");
            }

            // Copy TextBlock formatting from source to target
            // Margins
            targetShape.TextBlock.LeftMargin.Value = sourceShape.TextBlock.LeftMargin.Value;
            targetShape.TextBlock.RightMargin.Value = sourceShape.TextBlock.RightMargin.Value;
            targetShape.TextBlock.TopMargin.Value = sourceShape.TextBlock.TopMargin.Value;
            targetShape.TextBlock.BottomMargin.Value = sourceShape.TextBlock.BottomMargin.Value;

            // Text direction and vertical alignment
            targetShape.TextBlock.TextDirection.Value = sourceShape.TextBlock.TextDirection.Value;
            targetShape.TextBlock.VerticalAlign.Value = sourceShape.TextBlock.VerticalAlign.Value;

            // Background color and transparency
            targetShape.TextBlock.TextBkgnd.Ufe.F = sourceShape.TextBlock.TextBkgnd.Ufe.F;
            targetShape.TextBlock.TextBkgndTrans.Value = sourceShape.TextBlock.TextBkgndTrans.Value;

            // Default tab stop
            targetShape.TextBlock.DefaultTabStop.Value = sourceShape.TextBlock.DefaultTabStop.Value;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
