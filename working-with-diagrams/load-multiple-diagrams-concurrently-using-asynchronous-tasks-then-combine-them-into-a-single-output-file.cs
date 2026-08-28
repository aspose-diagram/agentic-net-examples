using System;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Asynchronous entry point
        static async Task Main(string[] args)
        {
            try
            {

                // Input diagram file paths
                string[] inputFiles = { "diagram1.vsdx", "diagram2.vsdx", "diagram3.vsdx" };
                // Output combined diagram file path
                string outputFile = "combined.vsdx";

                // Load all diagrams concurrently using tasks
                Task<Diagram>[] loadTasks = inputFiles
                    .Select(file => Task.Run(() => new Diagram(file)))
                    .ToArray();

                // Await completion of all loading tasks
                Diagram[] diagrams = await Task.WhenAll(loadTasks);

                // Use the first diagram as the base for combination
                Diagram combinedDiagram = diagrams[0];

                // Combine remaining diagrams into the base diagram
                for (int i = 1; i < diagrams.Length; i++)
                {
                    combinedDiagram.Combine(diagrams[i]);
                }

                // Save the combined diagram to the specified output file
                combinedDiagram.Save(outputFile, SaveFileFormat.Vsdx);

                // Dispose all diagram objects to release resources
                foreach (Diagram diagram in diagrams)
                {
                    diagram.Dispose();
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }