using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";   // TODO: replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Ensure there are at least two pages; add a new page if necessary
                Page sourcePage = diagram.Pages[0];
                Page targetPage;
                if (diagram.Pages.Count > 1)
                {
                    targetPage = diagram.Pages[1];
                }
                else
                {
                    // Add a blank page and use it as the target
                    targetPage = new Page();
                    diagram.Pages.Add(targetPage);
                }

                // Retrieve the first shape on the source page to duplicate
                Shape originalShape = null;
                foreach (Shape shp in sourcePage.Shapes)
                {
                    originalShape = shp;
                    break;
                }

                if (originalShape == null)
                {
                    Console.WriteLine("No shape found on the source page to duplicate.");
                    return;
                }

                // Get master name (shape type) – required for creating a new shape of the same type
                string masterName = originalShape.Master != null ? originalShape.Master.Name : string.Empty;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.WriteLine("The original shape does not have an associated master. Duplication aborted.");
                    return;
                }

                // Get original position (PinX, PinY)
                double origPinX = originalShape.XForm.PinX.Value;
                double origPinY = originalShape.XForm.PinY.Value;

                // Add a new shape on the target page using the same master.
                // Offset the position slightly so the copy does not overlap the original.
                double newPinX = origPinX + 2.0; // 2 inches to the right
                double newPinY = origPinY + 2.0; // 2 inches up

                long newShapeId = targetPage.AddShape(newPinX, newPinY, masterName);
                Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                // Copy the text content from the original shape to the new shape
                newShape.Text.Value.Clear();
                foreach (var item in originalShape.Text.Value)
                {
                    if (item is Txt txt)
                    {
                        newShape.Text.Value.Add(new Txt(txt.Text));
                    }
                }

                // Optionally, copy other visual properties (fill, line, etc.) as needed.
                // Example: copy fill foreground color
                newShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                // Example: copy line color
                newShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;

                // Save the modified diagram
                string outputPath = "output.vsdx"; // TODO: replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine($"Shape duplicated. New shape ID: {newShapeId}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }