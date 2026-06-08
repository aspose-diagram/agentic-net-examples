using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {

            // Input Visio files to be combined
            string[] inputFiles = { "Diagram1.vsdx", "Diagram2.vsdx", "Diagram3.vsdx" };

            // Load all diagrams concurrently
            Diagram[] diagrams = await LoadDiagramsAsync(inputFiles);

            // Use the first diagram as the base and combine the rest into it
            Diagram combinedDiagram = diagrams[0];
            for (int i = 1; i < diagrams.Length; i++)
            {
                combinedDiagram.Combine(diagrams[i]);
            }

            // Save the combined diagram to a new file
            combinedDiagram.Save("CombinedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Asynchronously loads each diagram using the Diagram(string) constructor
    static Task<Diagram[]> LoadDiagramsAsync(string[] filePaths)
    {
        var loadTasks = new Task<Diagram>[filePaths.Length];
        for (int i = 0; i < filePaths.Length; i++)
        {
            string path = filePaths[i];
            loadTasks[i] = Task.Run(() => new Diagram(path));
        }
        return Task.WhenAll(loadTasks);
    }
}
