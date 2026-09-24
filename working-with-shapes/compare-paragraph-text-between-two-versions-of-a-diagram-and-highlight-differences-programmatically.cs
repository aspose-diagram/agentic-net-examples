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

            // Paths to the original and updated Visio files
            string oldDiagramPath = "oldDiagram.vsdx";
            string newDiagramPath = "newDiagram.vsdx";
            string outputDiagramPath = "newDiagram_HighlightedDifferences.vsdx";

            // Load both diagrams
            Diagram oldDiagram = new Diagram(oldDiagramPath);
            Diagram newDiagram = new Diagram(newDiagramPath);

            // Iterate through pages (assumes same page order and count)
            for (int pageIndex = 0; pageIndex < oldDiagram.Pages.Count; pageIndex++)
            {
                Page oldPage = oldDiagram.Pages[pageIndex];
                Page newPage = newDiagram.Pages[pageIndex];

                // Iterate through shapes on the old page
                foreach (Shape oldShape in oldPage.Shapes)
                {
                    // Skip deleted shapes
                    if (oldShape.Del == BOOL.True)
                        continue;

                    // Find the corresponding shape in the new diagram by ID
                    Shape newShape = newPage.Shapes.GetShape(oldShape.ID);
                    if (newShape == null || newShape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text from both shapes
                    string oldText = oldShape.Text.Value.Text ?? string.Empty;
                    string newText = newShape.Text.Value.Text ?? string.Empty;

                    // Compare texts; if different, highlight the shape in the new diagram
                    if (!string.Equals(oldText, newText, StringComparison.Ordinal))
                    {
                        Console.WriteLine($"Difference found in Shape ID {oldShape.ID} on Page {pageIndex + 1}");
                        Console.WriteLine($"Old Text: \"{oldText}\"");
                        Console.WriteLine($"New Text: \"{newText}\"");

                        // Highlight by setting the fill foreground color to red
                        newShape.Fill.FillForegnd.Value = "#FF0000";

                        // Optionally, also change the line color to red for better visibility
                        newShape.Line.LineColor.Value = "#FF0000";
                    }
                }
            }

            // Save the highlighted diagram
            newDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Highlighted diagram saved to: {outputDiagramPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
