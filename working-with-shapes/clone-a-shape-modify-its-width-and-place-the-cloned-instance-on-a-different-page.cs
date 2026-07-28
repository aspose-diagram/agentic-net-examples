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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are at least two pages (source + target)
            if (diagram.Pages.Count < 2)
            {
                // Add a blank page as the target page
                diagram.Pages.Add(new Page());
            }

            // Source page (first page)
            Page sourcePage = diagram.Pages[0];

            // Retrieve the first shape on the source page to clone
            Shape sourceShape = null;
            foreach (Shape s in sourcePage.Shapes)
            {
                sourceShape = s;
                break;
            }

            if (sourceShape == null)
            {
                throw new Exception("No shape found on the source page to clone.");
            }

            // Determine the master name of the source shape
            string masterName = sourceShape.Master != null ? sourceShape.Master.Name : "Rectangle";

            // Target page (second page)
            Page targetPage = diagram.Pages[1];

            // Add a new shape on the target page using the same master
            long newShapeId = targetPage.AddShape(
                sourceShape.XForm.PinX.Value,   // PinX (position X)
                sourceShape.XForm.PinY.Value,   // PinY (position Y)
                masterName);                    // Master name

            // Retrieve the newly added shape instance
            Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

            // Copy all properties from the source shape to the cloned shape
            sourceShape.Copy(clonedShape);

            // Modify the width of the cloned shape (e.g., set to 2.0 inches)
            clonedShape.XForm.Width.Value = 2.0;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Shape cloned, width modified, and placed on a different page successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
