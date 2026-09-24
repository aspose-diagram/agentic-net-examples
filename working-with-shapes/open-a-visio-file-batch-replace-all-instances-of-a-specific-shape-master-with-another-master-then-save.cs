using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input parameters (replace with actual paths and master names as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";
            string oldMasterName = "OldMaster";
            string newMasterName = "NewMaster";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs to replace (to avoid modifying collection while iterating)
                var shapesToReplace = new System.Collections.Generic.List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == oldMasterName)
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Replace each identified shape
                foreach (long shapeId in shapesToReplace)
                {
                    Shape oldShape = page.Shapes.GetShape(shapeId);

                    // Preserve geometry
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Preserve text (if any)
                    string oldText = oldShape.Text.Value.Text;

                    // Mark the old shape for deletion
                    oldShape.Del = BOOL.True;

                    // Add a new shape using the new master
                    long newShapeId = page.AddShape(pinX, pinY, width, height, newMasterName, false);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Transfer text to the new shape
                    if (!string.IsNullOrWhiteSpace(oldText))
                    {
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(oldText));
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
