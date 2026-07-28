using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static async Task Main()
    {
        try
        {

            // Paths of the diagrams to be loaded
            string[] inputFiles = { "diagram1.vsdx", "diagram2.vsdx", "diagram3.vsdx" };

            // Load each diagram on a separate thread concurrently
            Task<Diagram>[] loadTasks = inputFiles
                .Select(path => Task.Run(() => new Diagram(path)))
                .ToArray();

            // Await all loading tasks
            Diagram[] diagrams = await Task.WhenAll(loadTasks);

            // Combine all diagrams into the first one
            Diagram combinedDiagram = diagrams[0];
            for (int i = 1; i < diagrams.Length; i++)
            {
                combinedDiagram.Combine(diagrams[i]);
            }

            // Save the combined diagram to a new file
            combinedDiagram.Save("combined_output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
