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
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is a target page (second page). Create one if it does not exist.
            Page targetPage;
            if (diagram.Pages.Count >= 2)
            {
                targetPage = diagram.Pages[1];
            }
            else
            {
                // Determine a new unique page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                targetPage = new Page(maxId + 1);
                diagram.Pages.Add(targetPage);
            }

            // Get a shape to clone from the first page (source page)
            Page sourcePage = diagram.Pages[0];
            Shape sourceShape = null;
            foreach (Shape s in sourcePage.Shapes)
            {
                sourceShape = s;
                break; // take the first shape found
            }

            if (sourceShape == null)
            {
                Console.WriteLine("No shape found on the source page to clone.");
                return;
            }

            // Retrieve the master name of the source shape
            string masterName = sourceShape.Master != null ? sourceShape.Master.Name : string.Empty;
            if (string.IsNullOrEmpty(masterName))
            {
                Console.WriteLine("Source shape does not have an associated master.");
                return;
            }

            // Add a new shape on the target page using the same master
            long newShapeId = targetPage.AddShape(
                sourceShape.XForm.PinX.Value,
                sourceShape.XForm.PinY.Value,
                masterName,
                false);

            // Retrieve the newly added shape instance
            Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

            // Copy size (width & height)
            clonedShape.XForm.Width.Value = sourceShape.XForm.Width.Value;
            clonedShape.XForm.Height.Value = sourceShape.XForm.Height.Value;

            // Copy text content
            clonedShape.Text.Value.Clear();
            foreach (var item in sourceShape.Text.Value)
            {
                if (item is Txt txt)
                {
                    // Preserve the text run
                    clonedShape.Text.Value.Add(new Txt(txt.Text));
                }
            }

            // Apply a different preset theme to the cloned shape
            clonedShape.PresetTheme = PresetThemeValue.Bubble;
            clonedShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
            // Optionally set a quick style
            clonedShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Shape cloned, theme applied, and placed on another page successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
