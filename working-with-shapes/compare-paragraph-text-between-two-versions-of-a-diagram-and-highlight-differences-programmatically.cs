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

        string oldPath = args[0];
        string newPath = args[1];
        string outputPath = args[2];

        // Guard against missing input files
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

        try
        {
            // Load both diagrams inside using blocks (mandatory pattern)
            using (Diagram oldDiagram = new Diagram(oldPath))
            using (Diagram newDiagram = new Diagram(newPath))
            {
                // Build a lookup of shapes from the old diagram by their unique ID
                var oldShapeLookup = new System.Collections.Generic.Dictionary<long, Shape>();

                for (int p = 0; p < oldDiagram.Pages.Count; p++)
                {
                    Page oldPage = oldDiagram.Pages[p];
                    for (int s = 0; s < oldPage.Shapes.Count; s++)
                    {
                        Shape oldShape = oldPage.Shapes[s];
                        // Shape.ID is a unique identifier for the shape
                        oldShapeLookup[oldShape.ID] = oldShape;
                    }
                }

                // Iterate through shapes in the new diagram and compare paragraph text
                for (int p = 0; p < newDiagram.Pages.Count; p++)
                {
                    Page newPage = newDiagram.Pages[p];
                    for (int s = 0; s < newPage.Shapes.Count; s++)
                    {
                        Shape newShape = newPage.Shapes[s];

                        // Skip deleted shapes
                        if (newShape.Del == BOOL.True)
                            continue;

                        if (oldShapeLookup.TryGetValue(newShape.ID, out Shape oldShape))
                        {
                            // Retrieve plain text from the shape (concatenated paragraphs)
                            string oldText = oldShape.Text?.Value?.Text ?? string.Empty;
                            string newText = newShape.Text?.Value?.Text ?? string.Empty;

                            // Compare texts; if different, highlight the shape in the new diagram
                            if (!string.Equals(oldText, newText, StringComparison.Ordinal))
                            {
                                Console.WriteLine($"Difference found in shape ID {newShape.ID} on page {p + 1}");
                                // Highlight by setting the line color to red
                                newShape.Line.LineColor.Value = "#FF0000";
                            }
                        }
                        else
                        {
                            // Shape does not exist in the old diagram; treat as added shape
                            Console.WriteLine($"New shape added with ID {newShape.ID} on page {p + 1}");
                            // Highlight added shapes with green line color
                            newShape.Line.LineColor.Value = "#00FF00";
                        }
                    }
                }

                // Save the modified new diagram with highlights
                newDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Comparison completed. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}