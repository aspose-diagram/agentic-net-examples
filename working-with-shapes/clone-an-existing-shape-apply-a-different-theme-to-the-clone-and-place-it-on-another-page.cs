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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there are at least two pages (source and target)
                if (diagram.Pages.Count < 2)
                {
                    // Add a new blank page as the target page
                    diagram.Pages.Add(new Page());
                }

                // Source page (first page) and target page (second page)
                Page sourcePage = diagram.Pages[0];
                Page targetPage = diagram.Pages[1];

                // Find the first shape on the source page to clone
                Shape originalShape = null;
                foreach (Shape s in sourcePage.Shapes)
                {
                    originalShape = s;
                    break;
                }

                if (originalShape == null)
                    throw new Exception("No shape found on the source page to clone.");

                // Retrieve the master name of the original shape
                if (originalShape.Master == null)
                    throw new Exception("The original shape does not have an associated master.");

                string masterName = originalShape.Master.Name;

                // Get original shape position
                double origPinX = originalShape.XForm.PinX.Value;
                double origPinY = originalShape.XForm.PinY.Value;

                // Add a new shape on the target page using the same master.
                // Offset the position slightly so the clone does not overlap the original.
                long newShapeId = targetPage.AddShape(origPinX + 2.0, origPinY + 2.0, masterName);

                // Retrieve the newly added shape instance
                Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                // Copy all formatting and properties from the original shape to the clone
                originalShape.Copy(clonedShape);

                // Apply a different preset theme to the cloned shape
                clonedShape.PresetTheme = PresetThemeValue.Bubble;
                clonedShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Shape cloned, themed, and placed on another page successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
