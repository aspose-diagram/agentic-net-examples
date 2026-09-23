using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Paths to the source Visio files (VSDX format)
            string firstDiagramPath = "Diagram1.vsdx";
            string secondDiagramPath = "Diagram2.vsdx";

            // Load the two diagrams
            Diagram firstDiagram = new Diagram(firstDiagramPath, LoadFileFormat.Vsdx);
            Diagram secondDiagram = new Diagram(secondDiagramPath, LoadFileFormat.Vsdx);

            // Merge the second diagram into the first.
            // The Combine method merges pages, masters, and resolves duplicate shape IDs internally.
            firstDiagram.Combine(secondDiagram);

            // Save the merged diagram
            string outputPath = "MergedDiagram.vsdx";
            firstDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Merged diagram saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
