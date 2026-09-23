using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the original and the saved diagram
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Capture page and shape information before modifications
            List<PageSnapshot> beforeSnapshots = CaptureDiagramState(diagram);

            // Modify print options for each page
            foreach (Page page in diagram.Pages)
            {
                // Set orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                // Set scaling to 75%
                page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                page.PageSheet.PrintProps.ScaleY.Value = 0.75;
            }

            // Save the diagram after changes
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Load the saved diagram for verification
            Diagram savedDiagram = new Diagram(outputPath);
            List<PageSnapshot> afterSnapshots = CaptureDiagramState(savedDiagram);

            // Verify that page content (shapes and their text) is unchanged
            if (beforeSnapshots.Count != afterSnapshots.Count)
                throw new Exception("Page count mismatch after saving.");

            for (int i = 0; i < beforeSnapshots.Count; i++)
            {
                PageSnapshot before = beforeSnapshots[i];
                PageSnapshot after = afterSnapshots[i];

                if (before.ShapeInfos.Count != after.ShapeInfos.Count)
                    throw new Exception($"Shape count mismatch on page ID {before.PageId}.");

                for (int j = 0; j < before.ShapeInfos.Count; j++)
                {
                    ShapeInfo sBefore = before.ShapeInfos[j];
                    ShapeInfo sAfter = after.ShapeInfos[j];

                    if (sBefore.ShapeId != sAfter.ShapeId)
                        throw new Exception($"Shape ID mismatch on page ID {before.PageId}.");

                    if (sBefore.Text != sAfter.Text)
                        throw new Exception($"Shape text mismatch on shape ID {sBefore.ShapeId} (page ID {before.PageId}).");
                }
            }

            Console.WriteLine("Verification passed: page content unchanged after saving.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Captures a lightweight snapshot of each page's shapes and their plain text
    private static List<PageSnapshot> CaptureDiagramState(Diagram diagram)
    {
        var snapshots = new List<PageSnapshot>();

        foreach (Page page in diagram.Pages)
        {
            var pageSnap = new PageSnapshot
            {
                PageId = page.ID,
                ShapeInfos = new List<ShapeInfo>()
            };

            foreach (Shape shape in page.Shapes)
            {
                // Retrieve plain text of the shape
                string plainText = shape.Text.Value.Text;

                pageSnap.ShapeInfos.Add(new ShapeInfo
                {
                    ShapeId = shape.ID,
                    Text = plainText
                });
            }

            snapshots.Add(pageSnap);
        }

        return snapshots;
    }

    // Simple DTO for page snapshot
    private class PageSnapshot
    {
        public int PageId { get; set; }
        public List<ShapeInfo> ShapeInfos { get; set; }
    }

    // Simple DTO for shape information
    private class ShapeInfo
    {
        public long ShapeId { get; set; }
        public string Text { get; set; }
    }
}