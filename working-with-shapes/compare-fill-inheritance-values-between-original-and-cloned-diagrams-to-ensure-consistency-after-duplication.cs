using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the source Visio file as the first argument.");
            return;
        }

        string sourcePath = args[0];

        // Guard: ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Temporary file used to create a clone of the diagram
        string tempClonePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");

        try
        {
            // Load the original diagram and save a temporary clone
            using (Diagram originalDiagram = new Diagram(sourcePath))
            {
                originalDiagram.Save(tempClonePath, SaveFileFormat.Vsdx);
            }

            // Load the cloned diagram
            using (Diagram clonedDiagram = new Diagram(tempClonePath))
            {
                // Load the original diagram again for comparison
                using (Diagram originalDiagram = new Diagram(sourcePath))
                {
                    // Verify page count consistency
                    if (originalDiagram.Pages.Count != clonedDiagram.Pages.Count)
                    {
                        Console.Error.WriteLine("Page count mismatch between original and cloned diagrams.");
                        return;
                    }

                    // Helper local function to compare two values (converted to string)
                    bool CellsEqual(string cellName, object originalValue, object clonedValue, int pageIdx, int shapeIdx)
                    {
                        string origStr = originalValue?.ToString();
                        string cloneStr = clonedValue?.ToString();

                        if (origStr != cloneStr)
                        {
                            Console.Error.WriteLine($"Mismatch in {cellName} on page {pageIdx + 1}, shape index {shapeIdx + 1}.");
                            Console.Error.WriteLine($"Original: {origStr ?? "null"}, Cloned: {cloneStr ?? "null"}");
                            return false;
                        }
                        return true;
                    }

                    // Iterate through each page by index
                    for (int pageIndex = 0; pageIndex < originalDiagram.Pages.Count; pageIndex++)
                    {
                        Page originalPage = originalDiagram.Pages[pageIndex];
                        Page clonedPage = clonedDiagram.Pages[pageIndex];

                        // Verify shape count consistency on the current page
                        if (originalPage.Shapes.Count != clonedPage.Shapes.Count)
                        {
                            Console.Error.WriteLine($"Shape count mismatch on page {pageIndex + 1}.");
                            return;
                        }

                        // Compare each shape's inherited fill values
                        for (int shapeIdx = 0; shapeIdx < originalPage.Shapes.Count; shapeIdx++)
                        {
                            Shape originalShape = originalPage.Shapes[shapeIdx];
                            Shape clonedShape = clonedPage.Shapes[shapeIdx];

                            bool allMatch = true;
                            allMatch &= CellsEqual("FillForegnd",
                                originalShape.InheritFill.FillForegnd.Value,
                                clonedShape.InheritFill.FillForegnd.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("FillBkgnd",
                                originalShape.InheritFill.FillBkgnd.Value,
                                clonedShape.InheritFill.FillBkgnd.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("FillPattern",
                                originalShape.InheritFill.FillPattern.Value,
                                clonedShape.InheritFill.FillPattern.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShdwForegnd",
                                originalShape.InheritFill.ShdwForegnd.Value,
                                clonedShape.InheritFill.ShdwForegnd.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShdwBkgnd",
                                originalShape.InheritFill.ShdwBkgnd.Value,
                                clonedShape.InheritFill.ShdwBkgnd.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShdwPattern",
                                originalShape.InheritFill.ShdwPattern.Value,
                                clonedShape.InheritFill.ShdwPattern.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShapeShdwType",
                                originalShape.InheritFill.ShapeShdwType.Value,
                                clonedShape.InheritFill.ShapeShdwType.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShapeShdwObliqueAngle",
                                originalShape.InheritFill.ShapeShdwObliqueAngle.Value,
                                clonedShape.InheritFill.ShapeShdwObliqueAngle.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShapeShdwOffsetX",
                                originalShape.InheritFill.ShapeShdwOffsetX.Value,
                                clonedShape.InheritFill.ShapeShdwOffsetX.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShapeShdwOffsetY",
                                originalShape.InheritFill.ShapeShdwOffsetY.Value,
                                clonedShape.InheritFill.ShapeShdwOffsetY.Value,
                                pageIndex, shapeIdx);
                            allMatch &= CellsEqual("ShapeShdwScaleFactor",
                                originalShape.InheritFill.ShapeShdwScaleFactor.Value,
                                clonedShape.InheritFill.ShapeShdwScaleFactor.Value,
                                pageIndex, shapeIdx);

                            if (!allMatch)
                            {
                                Console.Error.WriteLine("Fill inheritance values differ between original and cloned diagrams.");
                                return;
                            }
                        }
                    }

                    // If execution reaches here, all fill inheritance values match
                    Console.WriteLine("All fill inheritance values are consistent between the original and cloned diagrams.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary clone file if it exists
            if (File.Exists(tempClonePath))
            {
                try
                {
                    File.Delete(tempClonePath);
                }
                catch
                {
                    // Suppress any cleanup errors
                }
            }
        }
    }
}
