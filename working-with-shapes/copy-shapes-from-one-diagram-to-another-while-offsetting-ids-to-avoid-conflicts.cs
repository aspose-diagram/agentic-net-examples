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

            // Load the source diagram from a file.
            string sourcePath = "source.vsdx";
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Create a new empty diagram.
                using (Diagram targetDiagram = new Diagram())
                {
                    // Ensure the target diagram has at least one page.
                    if (targetDiagram.Pages.Count == 0)
                    {
                        targetDiagram.Pages.Add(new Page());
                    }

                    // Copy all masters from the source to the target to preserve shape types.
                    foreach (Master srcMaster in sourceDiagram.Masters)
                    {
                        // AddMaster adds a master by its universal name.
                        targetDiagram.AddMaster(sourceDiagram, srcMaster.NameU);
                    }

                    // For each page in the source diagram, create a corresponding page in the target.
                    foreach (Page srcPage in sourceDiagram.Pages)
                    {
                        // Create a new page in the target diagram.
                        Page tgtPage = new Page();
                        targetDiagram.Pages.Add(tgtPage);

                        // Copy each shape from the source page to the target page.
                        foreach (Shape srcShape in srcPage.Shapes)
                        {
                            // Retrieve the master name; if the shape has no master, skip it.
                            string masterName = srcShape.Master?.Name;
                            if (string.IsNullOrEmpty(masterName))
                                continue;

                            // Get geometry values.
                            double pinX = srcShape.XForm.PinX.Value;
                            double pinY = srcShape.XForm.PinY.Value;
                            double width = srcShape.XForm.Width.Value;
                            double height = srcShape.XForm.Height.Value;

                            // Add the shape to the target page. The returned ID is unique within the target diagram.
                            long newShapeId = tgtPage.AddShape(pinX, pinY, width, height, masterName);

                            // Retrieve the newly added shape to copy additional properties.
                            Shape tgtShape = tgtPage.Shapes.GetShape(newShapeId);

                            // Copy plain text (if any) from the source shape.
                            string plainText = srcShape.Text.Value.Text;
                            if (!string.IsNullOrWhiteSpace(plainText))
                            {
                                tgtShape.Text.Value.Clear();
                                tgtShape.Text.Value.Add(new Txt(plainText));
                            }

                            // Example of copying fill foreground color (optional).
                            tgtShape.Fill.FillForegnd.Value = srcShape.Fill.FillForegnd.Value;

                            // Example of copying line color (optional).
                            tgtShape.Line.LineColor.Value = srcShape.Line.LineColor.Value;
                        }
                    }

                    // Save the target diagram to a new file.
                    string targetPath = "target.vsdx";
                    targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram copied successfully to '{targetPath}'.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
