using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing the source Visio files.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Get all .vsdx files in the folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the specified folder.");
            return;
        }

        // Load the first diagram as the target diagram.
        Diagram targetDiagram = new Diagram(diagramFiles[0]);

        // Iterate over the remaining diagrams and merge their pages into the target.
        for (int i = 1; i < diagramFiles.Length; i++)
        {
            Diagram sourceDiagram = new Diagram(diagramFiles[i]);
            targetDiagram.Combine(sourceDiagram);
        }

        // Save the merged diagram to a new file.
        string outputPath = Path.Combine(folderPath, "MergedDiagram.vsdx");
        targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Merged diagram saved to: {outputPath}");
    }
}
