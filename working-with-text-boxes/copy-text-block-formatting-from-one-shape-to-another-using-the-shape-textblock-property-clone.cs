using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the first page (avoid ActivePage)
            Page page = diagram.Pages[0];

            // Locate source and target shapes by name
            Shape sourceShape = null;
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Name == "SourceShape")
                    sourceShape = shape;
                else if (shape.Name == "TargetShape")
                    targetShape = shape;
            }

            // Validate shape existence
            if (sourceShape == null)
                throw new Exception("Source shape not found.");
            if (targetShape == null)
                throw new Exception("Target shape not found.");

            // Copy TextBlock formatting property‑by‑property (TextBlock is read‑only)
            var srcBlock = sourceShape.TextBlock;
            var tgtBlock = targetShape.TextBlock;

            // Clone margin values
            tgtBlock.LeftMargin = (DoubleValue)srcBlock.LeftMargin.Clone();
            tgtBlock.RightMargin = (DoubleValue)srcBlock.RightMargin.Clone();
            tgtBlock.TopMargin = (DoubleValue)srcBlock.TopMargin.Clone();
            tgtBlock.BottomMargin = (DoubleValue)srcBlock.BottomMargin.Clone();

            // Copy text direction and vertical alignment enums
            tgtBlock.TextDirection.Value = srcBlock.TextDirection.Value;
            tgtBlock.VerticalAlign.Value = srcBlock.VerticalAlign.Value;

            // Clone background color formula (Ufe.F) and transparency
            tgtBlock.TextBkgnd.Ufe.F = srcBlock.TextBkgnd.Ufe.F;
            tgtBlock.TextBkgndTrans = (DoubleValue)srcBlock.TextBkgndTrans.Clone();

            // Clone default tab stop
            tgtBlock.DefaultTabStop = (DoubleValue)srcBlock.DefaultTabStop.Clone();

            // Save the modified diagram using a valid overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}