using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: old diagram path and new diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <oldDiagramPath> <newDiagramPath>");
            return;
        }

        string oldPath = args[0];
        string newPath = args[1];

        // Guard against missing files
        if (!File.Exists(oldPath))
        {
            Console.Error.WriteLine($"File not found: {oldPath}");
            return;
        }
        if (!File.Exists(newPath))
        {
            Console.Error.WriteLine($"File not found: {newPath}");
            return;
        }

        Diagram oldDiagram = null;
        Diagram newDiagram = null;

        try
        {
            // Load both diagram versions
            oldDiagram = new Diagram(oldPath);
            newDiagram = new Diagram(newPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagrams: {ex.Message}");
            return;
        }

        try
        {
            // Determine the number of pages to compare (use the smaller count)
            int pageCount = Math.Min(oldDiagram.Pages.Count, newDiagram.Pages.Count);

            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                Page oldPage = oldDiagram.Pages[pageIndex];
                Page newPage = newDiagram.Pages[pageIndex];

                // Iterate over shapes in the old page
                foreach (Shape oldShape in oldPage.Shapes)
                {
                    // Attempt to locate the corresponding shape in the new diagram by ID
                    Shape newShape = null;
                    try
                    {
                        // Shape IDs are unique within a page; use GetShape(id) to retrieve
                        newShape = newPage.Shapes.GetShape(oldShape.ID);
                    }
                    catch
                    {
                        // If GetShape throws, the shape does not exist in the new version
                        newShape = null;
                    }

                    if (newShape == null)
                    {
                        Console.WriteLine($"Shape ID {oldShape.ID} missing in new diagram (Page {pageIndex + 1}).");
                        continue;
                    }

                    // Compare each supported event cell
                    CompareEventCell("EventDrop", oldShape.Event.EventDrop?.Ufe?.F, newShape.Event.EventDrop?.Ufe?.F, oldShape.ID, pageIndex);
                    CompareEventCell("EventDblClick", oldShape.Event.EventDblClick?.Ufe?.F, newShape.Event.EventDblClick?.Ufe?.F, oldShape.ID, pageIndex);
                    CompareEventCell("EventXFMod", oldShape.Event.EventXFMod?.Ufe?.F, newShape.Event.EventXFMod?.Ufe?.F, oldShape.ID, pageIndex);
                    CompareEventCell("EventMultiDrop", oldShape.Event.EventMultiDrop?.Ufe?.F, newShape.Event.EventMultiDrop?.Ufe?.F, oldShape.ID, pageIndex);
                    CompareEventCell("TheText", oldShape.Event.TheText?.Ufe?.F, newShape.Event.TheText?.Ufe?.F, oldShape.ID, pageIndex);
                    CompareEventCell("TheData", oldShape.Event.TheData?.Ufe?.F, newShape.Event.TheData?.Ufe?.F, oldShape.ID, pageIndex);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released
            oldDiagram?.Dispose();
            newDiagram?.Dispose();
        }
    }

    /// <summary>
    /// Compares two event cell formulas and writes a message if they differ.
    /// </summary>
    /// <param name="cellName">Name of the event cell (e.g., EventDrop).</param>
    /// <param name="oldFormula">Formula from the old diagram (may be null).</param>
    /// <param name="newFormula">Formula from the new diagram (may be null).</param>
    /// <param name="shapeId">Identifier of the shape being compared.</param>
    /// <param name="pageIndex">Zero‑based page index.</param>
    private static void CompareEventCell(string cellName, string oldFormula, string newFormula, long shapeId, int pageIndex)
    {
        // Normalize nulls to empty strings for comparison
        string oldVal = oldFormula ?? string.Empty;
        string newVal = newFormula ?? string.Empty;

        if (!oldVal.Equals(newVal, StringComparison.Ordinal))
        {
            Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shapeId}, Cell {cellName} changed:");
            Console.WriteLine($"  Old: {oldVal}");
            Console.WriteLine($"  New: {newVal}");
        }
    }
}