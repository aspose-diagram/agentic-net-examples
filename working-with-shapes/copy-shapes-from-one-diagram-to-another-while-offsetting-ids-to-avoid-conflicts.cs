using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to source and destination Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(sourcePath);

                // Create a new empty diagram
                Diagram targetDiagram = new Diagram();

                // Ensure the target diagram has at least one page
                if (targetDiagram.Pages.Count == 0)
                {
                    targetDiagram.Pages.Add(new Page());
                }

                // Use the first page of each diagram for simplicity
                Page sourcePage = sourceDiagram.Pages[0];
                Page targetPage = targetDiagram.Pages[0];

                // ------------------------------------------------------------
                // 1. Copy masters from source to target to avoid missing master errors
                // ------------------------------------------------------------
                foreach (Master srcMaster in sourceDiagram.Masters)
                {
                    // Add master by name; if already exists, Aspose.Diagram ignores duplicate addition
                    targetDiagram.AddMaster(sourceDiagram, srcMaster.Name);
                }

                // ------------------------------------------------------------
                // 2. Copy each shape from source page to target page
                // ------------------------------------------------------------
                foreach (Shape srcShape in sourcePage.Shapes)
                {
                    // Skip deleted shapes
                    if (srcShape.Del == BOOL.True)
                        continue;

                    // Determine master name; if the shape has no master, skip it
                    string masterName = srcShape.Master?.Name;
                    if (string.IsNullOrEmpty(masterName))
                        continue;

                    // Retrieve geometry information
                    double pinX = srcShape.XForm.PinX.Value;
                    double pinY = srcShape.XForm.PinY.Value;
                    double width = srcShape.XForm.Width.Value;
                    double height = srcShape.XForm.Height.Value;

                    // Add a new shape to the target page using the same master and geometry
                    long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName);

                    // Retrieve the newly added shape object
                    Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                    // --------------------------------------------------------
                    // 3. Copy basic visual properties (text, fill, line)
                    // --------------------------------------------------------
                    // Copy text
                    string plainText = srcShape.Text.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(plainText))
                    {
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(plainText));
                    }

                    // Copy fill foreground color
                    newShape.Fill.FillForegnd.Value = srcShape.Fill.FillForegnd.Value;

                    // Copy line color and weight
                    newShape.Line.LineColor.Value = srcShape.Line.LineColor.Value;
                    newShape.Line.LineWeight.Value = srcShape.Line.LineWeight.Value;

                    // Copy rotation angle
                    newShape.XForm.Angle.Value = srcShape.XForm.Angle.Value;
                }

                // ------------------------------------------------------------
                // 4. Save the target diagram
                // ------------------------------------------------------------
                targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }