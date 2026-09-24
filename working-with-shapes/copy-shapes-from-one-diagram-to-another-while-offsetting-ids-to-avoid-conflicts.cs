using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source diagram containing shapes to copy
            string sourcePath = "source.vsdx";

            // Load the source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Create a new empty diagram that will receive the copied shapes
            Diagram targetDiagram = new Diagram();

            // Ensure the target diagram has at least one page (required before Combine)
            targetDiagram.Pages.Add(new Page());

            // Merge the source diagram into the target diagram.
            // This copies masters, pages, and all shapes, assigning new IDs in the target.
            targetDiagram.Combine(sourceDiagram);

            // Save the resulting diagram
            string outputPath = "combined.vsdx";
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Shapes copied successfully. Output saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
