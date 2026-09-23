using System.IO;
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

            // Load the source diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // 1. Clone an existing master (e.g., a master named "Rectangle")
            // ------------------------------------------------------------
            Master originalMaster = diagram.Masters.GetMasterByName("Rectangle");
            if (originalMaster == null)
            {
                Console.WriteLine("Master named 'Rectangle' was not found in the diagram.");
                return;
            }

            // Clone and give the new master a distinct name
            Master clonedMaster = (Master)originalMaster.Clone();
            clonedMaster.Name = "RectangleClone";
            diagram.Masters.Add(clonedMaster);

            // ------------------------------------------------------------
            // 2. Modify the text block of shapes inside the cloned master
            // ------------------------------------------------------------
            foreach (Shape masterShape in clonedMaster.Shapes)
            {
                // Replace any existing text with new text
                masterShape.Text.Value.Clear();
                masterShape.Text.Value.Add(new Txt("Cloned Master Text"));

                // Example: set a background color for the text block
                // (uses the RGB() string format as required by the API)
                masterShape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,255,0)"; // yellow background
            }

            // ------------------------------------------------------------
            // 3. Apply the cloned master to selected shapes
            //    (here we replace all shapes that currently use the "Rectangle" master)
            // ------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that need to be replaced
                List<long> shapesToReplace = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Determine the page index for the AddShape overload
                int pageIndex = diagram.Pages.IndexOf(page);

                // Replace each identified shape
                foreach (long oldShapeId in shapesToReplace)
                {
                    Shape oldShape = page.Shapes.GetShape(oldShapeId);

                    // Preserve geometry of the original shape
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Hide the old shape (setting Del to BOOL.True marks it as deleted)
                    oldShape.Del = BOOL.True;

                    // Add a new shape based on the cloned master at the same location/size
                    long newShapeId = diagram.AddShape(pinX, pinY, width, height, clonedMaster.Name, pageIndex);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Optional: apply additional formatting to the newly added shape
                    newShape.Fill.FillForegnd.Value = "#00FF00"; // green fill
                }
            }

            // ------------------------------------------------------------
            // 4. Save the modified diagram
            // ------------------------------------------------------------
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
