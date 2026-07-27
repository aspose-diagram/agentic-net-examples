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

            // Paths to the source and target Visio files
            string sourcePath = "source.vsdx";
            string targetPath = "target.vsdx";
            string outputPath = "merged.vsdx";

            // Load the source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);
            // Load the target diagram
            Diagram targetDiagram = new Diagram(targetPath);

            // Assume we work with the first page of each diagram
            Page sourcePage = sourceDiagram.Pages[0];
            Page targetPage = targetDiagram.Pages[0];

            // Iterate through each shape in the source page
            foreach (Shape srcShape in sourcePage.Shapes)
            {
                // Find a shape in the target page with the same universal name (NameU)
                Shape matchingTarget = null;
                foreach (Shape tgtShape in targetPage.Shapes)
                {
                    if (srcShape.NameU == tgtShape.NameU)
                    {
                        matchingTarget = tgtShape;
                        break;
                    }
                }

                // If a matching shape is found, copy its EventSection cells
                if (matchingTarget != null)
                {
                    // Copy double‑click event
                    matchingTarget.Event.EventDblClick.Ufe.F = srcShape.Event.EventDblClick.Ufe.F;
                    // Copy drop event
                    matchingTarget.Event.EventDrop.Ufe.F = srcShape.Event.EventDrop.Ufe.F;
                    // Copy multi‑drop event
                    matchingTarget.Event.EventMultiDrop.Ufe.F = srcShape.Event.EventMultiDrop.Ufe.F;
                    // Copy text change event
                    matchingTarget.Event.TheText.Ufe.F = srcShape.Event.TheText.Ufe.F;
                    // Copy data change event
                    matchingTarget.Event.TheData.Ufe.F = srcShape.Event.TheData.Ufe.F;
                    // Copy XFMod event (if needed)
                    matchingTarget.Event.EventXFMod.Ufe.F = srcShape.Event.EventXFMod.Ufe.F;
                }
            }

            // Save the modified target diagram
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Merge completed. Output saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
