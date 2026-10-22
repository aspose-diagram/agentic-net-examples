using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input file paths
        string sourcePath = "source.vsdx";
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }
        string targetPath = "target.vsdx";
        string outputPath = "merged_output.vsdx";

        // Guard clauses to ensure files exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source and target diagrams
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // Iterate through pages by index (assumes both diagrams have the same number of pages)
            int pageCount = Math.Min(sourceDiagram.Pages.Count, targetDiagram.Pages.Count);
            for (int i = 0; i < pageCount; i++)
            {
                Page srcPage = sourceDiagram.Pages[i];
                Page tgtPage = targetDiagram.Pages[i];

                // Iterate all shapes on the source page
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    // Skip deleted shapes
                    if (srcShape.Del == BOOL.True)
                        continue;

                    // Find matching shape in target page by universal name (NameU)
                    Shape tgtShape = tgtPage.Shapes.GetShapeIncludingChild(srcShape.NameU);
                    if (tgtShape == null)
                        continue; // No matching shape; skip

                    // Copy EventDrop formula
                    tgtShape.Event.EventDrop.Ufe.F = srcShape.Event.EventDrop.Ufe.F;

                    // Copy EventDblClick formula
                    tgtShape.Event.EventDblClick.Ufe.F = srcShape.Event.EventDblClick.Ufe.F;

                    // Copy EventXFMod formula
                    tgtShape.Event.EventXFMod.Ufe.F = srcShape.Event.EventXFMod.Ufe.F;

                    // Copy EventMultiDrop formula
                    tgtShape.Event.EventMultiDrop.Ufe.F = srcShape.Event.EventMultiDrop.Ufe.F;

                    // Copy TheText formula (if needed)
                    tgtShape.Event.TheText.Ufe.F = srcShape.Event.TheText.Ufe.F;

                    // Copy TheData formula (if needed)
                    tgtShape.Event.TheData.Ufe.F = srcShape.Event.TheData.Ufe.F;
                }
            }

            // Save the merged diagram to the output path in VSDX format
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagrams merged successfully. Output saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine("An error occurred during diagram processing:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}