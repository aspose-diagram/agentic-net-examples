using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: old diagram path, new diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <oldDiagramPath> <newDiagramPath> <outputDiagramPath>");
            return;
        }

        string oldDiagramPath = args[0];
        string newDiagramPath = args[1];
        string outputDiagramPath = args[2];

        // Guard against missing input files
        if (!File.Exists(oldDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {oldDiagramPath}");
            return;
        }
        if (!File.Exists(newDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {newDiagramPath}");
            return;
        }

        try
        {
            // Load the old (baseline) diagram
            Diagram oldDiagram = new Diagram(oldDiagramPath);
            // Load the new diagram that will be highlighted
            Diagram newDiagram = new Diagram(newDiagramPath);

            // Determine the number of pages to compare (use the smaller count for safety)
            int pageCount = Math.Min(oldDiagram.Pages.Count, newDiagram.Pages.Count);

            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Access corresponding pages
                Page oldPage = oldDiagram.Pages[pageIndex];
                Page newPage = newDiagram.Pages[pageIndex];

                // Determine the number of shapes to compare on this page
                int shapeCount = Math.Min(oldPage.Shapes.Count, newPage.Shapes.Count);

                for (int shapeIdx = 0; shapeIdx < shapeCount; shapeIdx++)
                {
                    // Retrieve shapes by index
                    Shape oldShape = oldPage.Shapes[shapeIdx];
                    Shape newShape = newPage.Shapes[shapeIdx];

                    // Skip shapes that are marked as deleted
                    if (oldShape.Del == BOOL.True || newShape.Del == BOOL.True)
                        continue;

                    // Extract plain text from each shape
                    string oldText = oldShape.Text?.Value?.Text ?? string.Empty;
                    string newText = newShape.Text?.Value?.Text ?? string.Empty;

                    // Compare the text content
                    if (!string.Equals(oldText, newText, StringComparison.Ordinal))
                    {
                        // Highlight the shape in the new diagram to indicate a difference.
                        // Set line color to red.
                        newShape.Line.LineColor.Value = "#FF0000";

                        // Set fill foreground color to a light red.
                        newShape.Fill.FillForegnd.Value = "#FFCCCC";

                        // Log the difference.
                        Console.WriteLine($"Difference found on Page {pageIndex + 1}, Shape {shapeIdx + 1}.");
                        Console.WriteLine($"Old Text: \"{oldText}\"");
                        Console.WriteLine($"New Text: \"{newText}\"");
                    }
                }
            }

            // Save the highlighted new diagram.
            newDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Highlighted diagram saved to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}