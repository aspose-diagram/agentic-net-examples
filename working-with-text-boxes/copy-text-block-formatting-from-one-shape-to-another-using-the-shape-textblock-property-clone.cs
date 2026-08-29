using System.IO;
using System;
using Aspose.Diagram;

class CopyTextBlockFormatting
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify source and target shapes (by their IDs)
            // Adjust the page index and shape IDs as needed
            int pageIndex = 0;
            long sourceShapeId = 1;   // ID of the shape to copy formatting from
            long targetShapeId = 2;   // ID of the shape to apply formatting to

            // Retrieve the shapes
            Shape sourceShape = diagram.Pages[pageIndex].Shapes.GetShape(sourceShapeId);
            Shape targetShape = diagram.Pages[pageIndex].Shapes.GetShape(targetShapeId);

            // Clone the source shape's TextBlock
            TextBlock clonedTextBlock = (TextBlock)sourceShape.TextBlock.Clone();

            // Copy each formatting property to the target shape's TextBlock
            // (Only the properties that define the block's appearance are copied)
            targetShape.TextBlock.BottomMargin = clonedTextBlock.BottomMargin;
            targetShape.TextBlock.DefaultTabStop = clonedTextBlock.DefaultTabStop;
            targetShape.TextBlock.Del = clonedTextBlock.Del;
            targetShape.TextBlock.LeftMargin = clonedTextBlock.LeftMargin;
            targetShape.TextBlock.RightMargin = clonedTextBlock.RightMargin;
            targetShape.TextBlock.TextBkgnd = clonedTextBlock.TextBkgnd;
            targetShape.TextBlock.TextBkgndTrans = clonedTextBlock.TextBkgndTrans;
            targetShape.TextBlock.TextDirection = clonedTextBlock.TextDirection;
            targetShape.TextBlock.TopMargin = clonedTextBlock.TopMargin;
            targetShape.TextBlock.VerticalAlign = clonedTextBlock.VerticalAlign;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
