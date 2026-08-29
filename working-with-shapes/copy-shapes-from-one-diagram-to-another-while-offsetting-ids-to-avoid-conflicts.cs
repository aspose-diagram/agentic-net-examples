using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram, target diagram, output diagram
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <exe> <source.vsdx> <target.vsdx> <output.vsdx>");
            return;
        }

        string sourcePath = args[0];
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        string targetPath = args[1];
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"File not found: {targetPath}");
            return;
        }

        string outputPath = args[2];
        // No need to check output existence – it will be created/overwritten.

        try
        {
            // Load source and target diagrams
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // Ensure target has at least as many pages as source
            while (targetDiagram.Pages.Count < sourceDiagram.Pages.Count)
            {
                // Add blank pages to target when needed
                targetDiagram.Pages.Add(new Page());
            }

            // Iterate through each page in the source diagram
            for (int pageIndex = 0; pageIndex < sourceDiagram.Pages.Count; pageIndex++)
            {
                Page srcPage = sourceDiagram.Pages[pageIndex];
                Page tgtPage = targetDiagram.Pages[pageIndex];

                // Copy each non‑deleted shape from source page to target page
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    // Skip shapes marked for deletion
                    if (srcShape.Del == BOOL.True)
                        continue;

                    // Retrieve master name; if missing, cannot recreate shape
                    string masterName = srcShape.Master?.Name;
                    if (string.IsNullOrEmpty(masterName))
                        continue;

                    // Add a new shape to the target page using the same master and position
                    long newShapeId = tgtPage.AddShape(
                        srcShape.XForm.PinX.Value,   // X coordinate
                        srcShape.XForm.PinY.Value,   // Y coordinate
                        masterName,                  // Master name
                        false);                      // isCalculate flag

                    // Retrieve the newly added shape for property copying
                    Shape tgtShape = tgtPage.Shapes.GetShape(newShapeId);

                    // Copy size and rotation
                    tgtShape.XForm.Width.Value = srcShape.XForm.Width.Value;
                    tgtShape.XForm.Height.Value = srcShape.XForm.Height.Value;
                    tgtShape.XForm.Angle.Value = srcShape.XForm.Angle.Value;

                    // Copy fill properties (foreground color, background color, pattern)
                    tgtShape.Fill.FillForegnd.Value = srcShape.Fill.FillForegnd.Value;
                    tgtShape.Fill.FillBkgnd.Value = srcShape.Fill.FillBkgnd.Value;
                    tgtShape.Fill.FillPattern.Value = srcShape.Fill.FillPattern.Value;

                    // Copy line properties (color, weight, pattern)
                    tgtShape.Line.LineColor.Value = srcShape.Line.LineColor.Value;
                    tgtShape.Line.LineWeight.Value = srcShape.Line.LineWeight.Value;
                    tgtShape.Line.LinePattern.Value = srcShape.Line.LinePattern.Value;

                    // Copy text content (clear existing text then add runs)
                    tgtShape.Text.Value.Clear();
                    foreach (var txtItem in srcShape.Text.Value)
                    {
                        if (txtItem is Txt txtRun)
                        {
                            // Preserve the exact text of each run
                            tgtShape.Text.Value.Add(new Txt(txtRun.Text));
                        }
                    }

                    // Copy hyperlink collection if any
                    if (srcShape.Hyperlinks != null && srcShape.Hyperlinks.Count > 0)
                    {
                        foreach (Hyperlink srcLink in srcShape.Hyperlinks)
                        {
                            Hyperlink newLink = new Hyperlink();
                            newLink.Name = srcLink.Name;
                            newLink.Address.Value = srcLink.Address.Value;
                            newLink.SubAddress.Value = srcLink.SubAddress.Value;
                            newLink.Description.Value = srcLink.Description.Value;
                            tgtShape.Hyperlinks.Add(newLink);
                        }
                    }
                }
            }

            // Save the merged diagram to the specified output path
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shapes copied successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}