using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: old diagram path, new diagram path, output path
            if (args == null || args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramComparison <oldDiagramPath> <newDiagramPath> <outputPath>");
                return;
            }

            string oldDiagramPath = args[0];
            string newDiagramPath = args[1];
            string outputPath = args[2];

            // Load the two diagram versions
            Diagram oldDiagram = new Diagram(oldDiagramPath);
            Diagram newDiagram = new Diagram(newDiagramPath);

            // Iterate through pages of the old diagram
            foreach (Page oldPage in oldDiagram.Pages)
            {
                // Find the corresponding page in the new diagram by name
                Page newPage = newDiagram.Pages.GetPage(oldPage.Name);
                if (newPage == null)
                {
                    // No matching page; skip to next
                    continue;
                }

                // Iterate through shapes on the old page
                foreach (Shape oldShape in oldPage.Shapes)
                {
                    // Retrieve the shape with the same ID from the new page
                    Shape newShape = newPage.Shapes.GetShape(oldShape.ID);
                    if (newShape == null)
                    {
                        // Shape not present in new diagram; skip
                        continue;
                    }

                    // Get plain text from both shapes
                    string oldText = oldShape.Text.Value.Text;
                    string newText = newShape.Text.Value.Text;

                    // Compare texts; if different, highlight the shape in the new diagram
                    if (!string.Equals(oldText, newText, StringComparison.Ordinal))
                    {
                        // Highlight by setting fill foreground color to yellow
                        newShape.Fill.FillForegnd.Value = "#FFFF00";
                    }
                }
            }

            // Save the highlighted new diagram
            newDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Comparison completed. Highlighted diagram saved to: {outputPath}");
        }
    }