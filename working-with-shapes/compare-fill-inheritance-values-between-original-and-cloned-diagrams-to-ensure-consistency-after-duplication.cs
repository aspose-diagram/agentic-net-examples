using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the original Visio file
        string originalPath = "original.vsdx";

        // Guard: ensure the source file exists
        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        try
        {
            // Load the original diagram from file
            Diagram originalDiagram = new Diagram(originalPath);

            // Clone the diagram by saving to a memory stream and re‑loading from it
            Diagram clonedDiagram;
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the original diagram into the stream in VSDX format
                originalDiagram.Save(ms, SaveFileFormat.Vsdx);
                ms.Position = 0; // Reset stream for reading

                // Load a new diagram instance from the same stream
                clonedDiagram = new Diagram(ms);
            }

            // Verify that both diagrams contain the same number of pages
            if (originalDiagram.Pages.Count != clonedDiagram.Pages.Count)
                throw new Exception($"Page count mismatch: original={originalDiagram.Pages.Count}, cloned={clonedDiagram.Pages.Count}");

            // Iterate through each page by index
            for (int pageIndex = 0; pageIndex < originalDiagram.Pages.Count; pageIndex++)
            {
                Page originalPage = originalDiagram.Pages[pageIndex];
                Page clonedPage = clonedDiagram.Pages[pageIndex];

                // Count only non‑deleted shapes on each page
                int originalShapeCount = 0;
                foreach (Shape s in originalPage.Shapes)
                    if (s.Del == BOOL.False) originalShapeCount++;

                int clonedShapeCount = 0;
                foreach (Shape s in clonedPage.Shapes)
                    if (s.Del == BOOL.False) clonedShapeCount++;

                // Compare the non‑deleted shape counts
                if (originalShapeCount != clonedShapeCount)
                    throw new Exception($"Shape count mismatch on page '{originalPage.Name}' (index {pageIndex}): original={originalShapeCount}, cloned={clonedShapeCount}");

                // Iterate through each non‑deleted shape in the original page
                foreach (Shape originalShape in originalPage.Shapes)
                {
                    if (originalShape.Del == BOOL.True) continue; // Skip deleted shapes

                    // Retrieve the matching shape in the cloned page by ID
                    Shape clonedShape = clonedPage.Shapes.GetShape(originalShape.ID);
                    if (clonedShape == null)
                        throw new Exception($"Shape with ID {originalShape.ID} not found in cloned page '{clonedPage.Name}'.");

                    // Compare Fill Foreground color
                    string origForeColor = originalShape.Fill.FillForegnd.Value;
                    string cloneForeColor = clonedShape.Fill.FillForegnd.Value;
                    if (!string.Equals(origForeColor, cloneForeColor, StringComparison.OrdinalIgnoreCase))
                        throw new Exception($"FillForegnd mismatch on shape ID {originalShape.ID} (page '{originalPage.Name}'): original='{origForeColor}', cloned='{cloneForeColor}'.");

                    // Compare Fill Background color
                    string origBackColor = originalShape.Fill.FillBkgnd.Value;
                    string cloneBackColor = clonedShape.Fill.FillBkgnd.Value;
                    if (!string.Equals(origBackColor, cloneBackColor, StringComparison.OrdinalIgnoreCase))
                        throw new Exception($"FillBkgnd mismatch on shape ID {originalShape.ID} (page '{originalPage.Name}'): original='{origBackColor}', cloned='{cloneBackColor}'.");

                    // Compare Fill Pattern
                    int origPattern = originalShape.Fill.FillPattern.Value;
                    int clonePattern = clonedShape.Fill.FillPattern.Value;
                    if (origPattern != clonePattern)
                        throw new Exception($"FillPattern mismatch on shape ID {originalShape.ID} (page '{originalPage.Name}'): original={origPattern}, cloned={clonePattern}.");

                    // Compare inherited Fill Foreground color
                    string origInheritFore = originalShape.InheritFill.FillForegnd.Value;
                    string cloneInheritFore = clonedShape.InheritFill.FillForegnd.Value;
                    if (!string.Equals(origInheritFore, cloneInheritFore, StringComparison.OrdinalIgnoreCase))
                        throw new Exception($"InheritFill.Foregnd mismatch on shape ID {originalShape.ID} (page '{originalPage.Name}'): original='{origInheritFore}', cloned='{cloneInheritFore}'.");

                    // Compare inherited Fill Pattern
                    int origInheritPattern = originalShape.InheritFill.FillPattern.Value;
                    int cloneInheritPattern = clonedShape.InheritFill.FillPattern.Value;
                    if (origInheritPattern != cloneInheritPattern)
                        throw new Exception($"InheritFill.Pattern mismatch on shape ID {originalShape.ID} (page '{originalPage.Name}'): original={origInheritPattern}, cloned={cloneInheritPattern}.");
                }
            }

            Console.WriteLine("All fill inheritance values match between the original and cloned diagrams.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or validation errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}