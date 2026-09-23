using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static async Task Main(string[] args)
    {
        // Folder containing the diagrams to combine
        string inputFolder = "InputDiagrams";
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all Visio files (adjust the pattern if needed)
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No diagram files found to process.");
            return;
        }

        // Load each diagram concurrently
        List<Task<Diagram>> loadTasks = new List<Task<Diagram>>();
        foreach (string filePath in diagramFiles)
        {
            loadTasks.Add(Task.Run(() => new Diagram(filePath)));
        }

        Diagram[] loadedDiagrams = await Task.WhenAll(loadTasks);

        // Use the first diagram as the base and combine the rest into it
        Diagram combinedDiagram = loadedDiagrams[0];
        for (int i = 1; i < loadedDiagrams.Length; i++)
        {
            combinedDiagram.Combine(loadedDiagrams[i]);
        }

        // Save the combined diagram
        string outputPath = "CombinedDiagram.vsdx";
        combinedDiagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Combined diagram saved to: {outputPath}");
    }
}
