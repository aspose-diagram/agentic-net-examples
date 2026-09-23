using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Paths to source Visio files (replace with actual file locations)
        string[] sourceFiles = { "source1.vsdx", "source2.vsdx", "source3.vsdx" };
        // Path for the merged master diagram
        string masterOutputPath = "merged_master.vsdx";

        // Guard: ensure the first source file exists before loading the master diagram
        if (!File.Exists(sourceFiles[0]))
        {
            Console.Error.WriteLine($"File not found: {sourceFiles[0]}");
            return;
        }

        Diagram masterDiagram;
        try
        {
            // Load the first diagram as the initial master diagram
            masterDiagram = new Diagram(sourceFiles[0]);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load master diagram: {ex.Message}");
            return;
        }

        // Process remaining source diagrams and merge their OLE objects
        for (int i = 1; i < sourceFiles.Length; i++)
        {
            // Guard: ensure each source file exists before loading
            if (!File.Exists(sourceFiles[i]))
            {
                Console.Error.WriteLine($"File not found: {sourceFiles[i]}");
                continue; // Skip missing files but continue processing others
            }

            Diagram srcDiagram;
            try
            {
                srcDiagram = new Diagram(sourceFiles[i]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load source diagram '{sourceFiles[i]}': {ex.Message}");
                continue;
            }

            // Iterate through each page of the source diagram
            foreach (Page srcPage in srcDiagram.Pages)
            {
                // Try to find a page with the same name in the master diagram
                Page targetPage = masterDiagram.Pages.GetPage(srcPage.Name);
                if (targetPage == null)
                {
                    // Create a new page in the master diagram when no matching page exists
                    targetPage = new Page
                    {
                        Name = srcPage.Name
                    };

                    // Assign a unique page ID by finding the current maximum ID
                    int maxId = 0;
                    foreach (Page p in masterDiagram.Pages)
                    {
                        if (p.ID > maxId) maxId = p.ID;
                    }
                    targetPage.ID = maxId + 1;

                    masterDiagram.Pages.Add(targetPage);
                }

                // Iterate through shapes on the source page
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    // Identify OLE (foreign) shapes
                    if (srcShape.Type == TypeValue.Foreign &&
                        srcShape.ForeignData != null &&
                        srcShape.ForeignData.ObjectData != null &&
                        srcShape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Clone the foreign data (OLE binary) to avoid modifying the source
                        ForeignData clonedForeign = (ForeignData)srcShape.ForeignData.Clone();

                        // Preserve original geometry
                        double pinX = srcShape.XForm.PinX.Value;
                        double pinY = srcShape.XForm.PinY.Value;
                        double width = srcShape.XForm.Width.Value;
                        double height = srcShape.XForm.Height.Value;

                        // Create a new shape on the target page with the same size/position
                        long newShapeId = targetPage.DrawRectangle(pinX, pinY, width, height);
                        Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                        // Convert the rectangle into an OLE foreign shape
                        newShape.Type = TypeValue.Foreign;
                        // ForeignData is read‑only; copy the binary data instead of assigning a new instance
                        newShape.ForeignData.ObjectData = clonedForeign.ObjectData;

                        // Copy any text content from the source shape
                        if (srcShape.Text != null && srcShape.Text.Value != null && srcShape.Text.Value.Count > 0)
                        {
                            newShape.Text.Value.Clear();
                            foreach (var item in srcShape.Text.Value)
                            {
                                if (item is Txt txt)
                                {
                                    newShape.Text.Value.Add(new Txt(txt.Text));
                                }
                            }
                        }
                    }
                }
            }
        }

        try
        {
            // Save the merged diagram preserving all OLE objects and their positions
            masterDiagram.Save(masterOutputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {masterOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save merged diagram: {ex.Message}");
        }
    }
}