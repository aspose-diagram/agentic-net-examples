using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        // Expect three arguments: old diagram path, new diagram path, output diagram path
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: <oldDiagram.vsdx> <newDiagram.vsdx> <outputDiagram.vsdx>");
            return;
        }

        string oldPath = args[0];
        string newPath = args[1];
        string outputPath = args[2];

        // Load the two diagram versions
        Diagram oldDiagram = new Diagram(oldPath);
        Diagram newDiagram = new Diagram(newPath);

        // Assume both diagrams have the same number of pages and matching shape IDs
        for (int pageIndex = 0; pageIndex < oldDiagram.Pages.Count; pageIndex++)
        {
            Page oldPage = oldDiagram.Pages[pageIndex];
            Page newPage = newDiagram.Pages[pageIndex];

            // Iterate through all shapes on the old page
            foreach (Shape oldShape in oldPage.Shapes)
            {
                // Try to find the corresponding shape in the new diagram by ID
                Shape newShape = newPage.Shapes.GetShape(oldShape.ID);
                if (newShape == null)
                    continue; // No matching shape; skip

                // Retrieve plain text from both shapes
                string oldText = oldShape.Text.Value.Text ?? string.Empty;
                string newText = newShape.Text.Value.Text ?? string.Empty;

                // If the texts differ, highlight the shape in the new diagram
                if (!oldText.Equals(newText, StringComparison.Ordinal))
                {
                    // Highlight fill with yellow
                    newShape.Fill.FillForegnd.Value = "#FFFF00";

                    // Ensure there is at least one character formatting entry
                    if (newShape.Chars.Count == 0)
                    {
                        Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                        ch.IX = 0; // first character run
                        newShape.Chars.Add(ch);
                    }

                    // Set text color to red for the first character run
                    newShape.Chars[0].Color.Value = "#FF0000";
                }
            }
        }

        // Save the highlighted diagram
        newDiagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
