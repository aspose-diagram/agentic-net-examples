using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramMerger
{
    // Merges two VSDX files into one, fixing duplicate shape IDs.
    public static void Merge(string firstDiagramPath, string secondDiagramPath, string outputPath)
    {
        // Load the two diagrams using the provided constructor (load rule).
        Diagram firstDiagram = new Diagram(firstDiagramPath);
        Diagram secondDiagram = new Diagram(secondDiagramPath);

        // Combine the second diagram into the first one (combine rule).
        firstDiagram.Combine(secondDiagram);

        // ----- Resolve duplicate shape IDs -----
        // First, count occurrences of each shape ID across all pages.
        var idCounts = new Dictionary<long, int>();
        long maxId = 0;

        foreach (Page page in firstDiagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Track the maximum ID to generate new unique IDs later.
                if (shape.ID > maxId)
                    maxId = shape.ID;

                if (idCounts.ContainsKey(shape.ID))
                    idCounts[shape.ID]++;
                else
                    idCounts[shape.ID] = 1;
            }
        }

        // Second, reassign IDs for shapes that appear more than once.
        foreach (Page page in firstDiagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (idCounts[shape.ID] > 1)
                {
                    // Generate a new unique ID.
                    maxId++;
                    shape.ID = maxId;

                    // Update the dictionary so the new ID is considered unique.
                    idCounts[shape.ID] = 1;
                }
            }
        }

        // Save the merged diagram using the provided save method (save rule).
        firstDiagram.Save(outputPath, SaveFileFormat.Vsdx);
    }

    // Example usage.
    static void Main()
    {
        try
        {

            string diagram1 = @"C:\Diagrams\First.vsdx";
            string diagram2 = @"C:\Diagrams\Second.vsdx";
            string mergedOutput = @"C:\Diagrams\Merged.vsdx";

            Merge(diagram1, diagram2, mergedOutput);

            Console.WriteLine("Diagrams merged successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
