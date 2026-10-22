using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Program <diagramPath1> [<diagramPath2> ...]");
            return;
        }

        // List to hold processing tasks for each diagram file
        List<Task> processingTasks = new List<Task>();

        foreach (string diagramPath in args)
        {
            // Guard: ensure the file exists before processing
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            // Create a task that loads the diagram and adjusts its page size in parallel
            Task processingTask = Task.Run(() =>
            {
                Diagram diagram = null;

                // Asynchronously load the diagram (simulated via Task.Run)
                try
                {
                    diagram = new Diagram(diagramPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error loading diagram \"{diagramPath}\": {ex.Message}");
                    return;
                }

                // Prepare new page size values in a separate parallel task
                Task<(double width, double height)> sizeTask = Task.Run(() =>
                {
                    // Simulate some computation or I/O delay
                    Thread.Sleep(100);
                    // Example target size: 8.5 x 11 inches
                    return (8.5, 11.0);
                });

                // Retrieve the computed size (wait for the size task to complete)
                (double newWidth, double newHeight) = sizeTask.Result;

                // Adjust each page's size using the computed dimensions
                try
                {
                    foreach (Page page in diagram.Pages)
                    {
                        // Set page width and height (values are in inches)
                        page.PageSheet.PageProps.PageWidth.Value = newWidth;
                        page.PageSheet.PageProps.PageHeight.Value = newHeight;
                    }

                    // Define output file name with a suffix to avoid overwriting the original
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(diagramPath) ?? string.Empty,
                        Path.GetFileNameWithoutExtension(diagramPath) + "_Adjusted.vsdx");

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing diagram \"{diagramPath}\": {ex.Message}");
                }
            });

            processingTasks.Add(processingTask);
        }

        // Wait for all diagram processing tasks to complete before exiting
        Task.WaitAll(processingTasks.ToArray());
    }
}