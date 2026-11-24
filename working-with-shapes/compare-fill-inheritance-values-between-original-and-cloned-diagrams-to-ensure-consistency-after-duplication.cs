using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the source Visio file as the first argument.");
            return;
        }

        string sourcePath = args[0];

        // Guard: ensure the source file exists.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the original diagram inside a using block.
            using (Diagram originalDiagram = new Diagram(sourcePath))
            {
                // Clone the diagram by saving to a memory stream and loading back.
                Diagram clonedDiagram;
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the original diagram to the memory stream in VSDX format.
                    originalDiagram.Save(ms, SaveFileFormat.Vsdx);
                    ms.Position = 0; // Reset stream position for reading.

                    // Load the cloned diagram from the memory stream.
                    clonedDiagram = new Diagram(ms);
                }

                // Ensure the cloned diagram is disposed after use.
                using (clonedDiagram)
                {
                    bool allMatch = true;

                    // Verify both diagrams have the same number of pages.
                    if (originalDiagram.Pages.Count != clonedDiagram.Pages.Count)
                    {
                        Console.Error.WriteLine("Page count mismatch between original and cloned diagrams.");
                        allMatch = false;
                    }

                    int pageCount = Math.Min(originalDiagram.Pages.Count, clonedDiagram.Pages.Count);

                    for (int p = 0; p < pageCount; p++)
                    {
                        Page originalPage = originalDiagram.Pages[p];
                        Page clonedPage = clonedDiagram.Pages[p];

                        // Verify both pages have the same number of shapes.
                        if (originalPage.Shapes.Count != clonedPage.Shapes.Count)
                        {
                            Console.Error.WriteLine($"Shape count mismatch on page {p + 1}.");
                            allMatch = false;
                            continue;
                        }

                        int shapeCount = originalPage.Shapes.Count;

                        for (int s = 0; s < shapeCount; s++)
                        {
                            Shape originalShape = originalPage.Shapes[s];
                            Shape clonedShape = clonedPage.Shapes[s];

                            // Compare each relevant fill inheritance property.
                            if (!CompareCell(
                                    originalShape.InheritFill.FillForegnd.Value ?? string.Empty,
                                    clonedShape.InheritFill.FillForegnd.Value ?? string.Empty,
                                    $"Page {p + 1}, Shape {s + 1} - FillForegnd"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.FillBkgnd.Value ?? string.Empty,
                                    clonedShape.InheritFill.FillBkgnd.Value ?? string.Empty,
                                    $"Page {p + 1}, Shape {s + 1} - FillBkgnd"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.FillPattern.Value.ToString(),
                                    clonedShape.InheritFill.FillPattern.Value.ToString(),
                                    $"Page {p + 1}, Shape {s + 1} - FillPattern"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShdwForegnd.Value ?? string.Empty,
                                    clonedShape.InheritFill.ShdwForegnd.Value ?? string.Empty,
                                    $"Page {p + 1}, Shape {s + 1} - ShdwForegnd"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShdwBkgnd.Value ?? string.Empty,
                                    clonedShape.InheritFill.ShdwBkgnd.Value ?? string.Empty,
                                    $"Page {p + 1}, Shape {s + 1} - ShdwBkgnd"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShdwPattern.Value.ToString(),
                                    clonedShape.InheritFill.ShdwPattern.Value.ToString(),
                                    $"Page {p + 1}, Shape {s + 1} - ShdwPattern"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShapeShdwType.Value.ToString(),
                                    clonedShape.InheritFill.ShapeShdwType.Value.ToString(),
                                    $"Page {p + 1}, Shape {s + 1} - ShapeShdwType"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShapeShdwObliqueAngle.Value.ToString(),
                                    clonedShape.InheritFill.ShapeShdwObliqueAngle.Value.ToString(),
                                    $"Page {p + 1}, Shape {s + 1} - ShapeShdwObliqueAngle"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShapeShdwOffsetX.Value.ToString(),
                                    clonedShape.InheritFill.ShapeShdwOffsetX.Value.ToString(),
                                    $"Page {p + 1}, Shape {s + 1} - ShapeShdwOffsetX"))
                                allMatch = false;

                            if (!CompareCell(
                                    originalShape.InheritFill.ShapeShdwOffsetY.Value.ToString(),
                                    clonedShape.InheritFill.ShapeShdwOffsetY.Value.ToString(),
                                    $"Page {p + 1}, Shape {s + 1} - ShapeShdwOffsetY"))
                                allMatch = false;
                        }
                    }

                    if (allMatch)
                    {
                        Console.WriteLine("All fill inheritance values match between the original and cloned diagrams.");
                    }
                    else
                    {
                        Console.Error.WriteLine("Discrepancies were found in fill inheritance values.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper method to compare two cell values and report mismatches.
    private static bool CompareCell(string originalValue, string clonedValue, string context)
    {
        if (!string.Equals(originalValue, clonedValue, StringComparison.Ordinal))
        {
            Console.Error.WriteLine($"{context}: Original = '{originalValue}', Cloned = '{clonedValue}'");
            return false;
        }
        return true;
    }
}
