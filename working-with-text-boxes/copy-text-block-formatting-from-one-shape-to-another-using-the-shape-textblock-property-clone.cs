using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            Shape? sourceShape = null;
            Shape? targetShape = null;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "SourceShape")
                    sourceShape = shape;
                else if (shape.NameU == "TargetShape")
                    targetShape = shape;

                if (sourceShape != null && targetShape != null)
                    break;
            }

            if (sourceShape == null)
                throw new Exception("Source shape with NameU 'SourceShape' not found.");
            if (targetShape == null)
                throw new Exception("Target shape with NameU 'TargetShape' not found.");

            // Copy TextBlock formatting properties
            TextBlock srcTB = sourceShape.TextBlock;
            TextBlock tgtTB = targetShape.TextBlock;

            tgtTB.LeftMargin = srcTB.LeftMargin;
            tgtTB.RightMargin = srcTB.RightMargin;
            tgtTB.TopMargin = srcTB.TopMargin;
            tgtTB.BottomMargin = srcTB.BottomMargin;
            tgtTB.TextDirection = srcTB.TextDirection;
            tgtTB.VerticalAlign = srcTB.VerticalAlign;
            tgtTB.TextBkgnd = srcTB.TextBkgnd;
            tgtTB.TextBkgndTrans = srcTB.TextBkgndTrans;
            tgtTB.DefaultTabStop = srcTB.DefaultTabStop;

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}