using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Force a full garbage collection to get a clean baseline memory measurement.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Record memory usage before loading the diagram.
            long memoryBefore = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory before loading: {memoryBefore / 1024.0 / 1024.0:F2} MB");

            // Optional: measure load time.
            Stopwatch sw = Stopwatch.StartNew();

            // Load the diagram from the specified file.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                sw.Stop();
                Console.WriteLine($"Diagram loaded in {sw.ElapsedMilliseconds} ms");

                // Output information about each window in the diagram.
                Console.WriteLine($"Total windows: {diagram.Windows.Count}");
                foreach (Window window in diagram.Windows)
                {
                    Console.WriteLine($"Window ID: {window.ID}, Type: {window.WindowType}, Width: {window.WindowWidth}, Height: {window.WindowHeight}, State: {window.WindowState}");
                }
            }

            // Record memory usage after the diagram has been loaded and processed.
            long memoryAfter = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory after loading: {memoryAfter / 1024.0 / 1024.0:F2} MB");

            // Calculate and display the memory delta.
            long memoryDelta = memoryAfter - memoryBefore;
            Console.WriteLine($"Memory increase due to loading: {memoryDelta / 1024.0 / 1024.0:F2} MB");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or general exceptions and report them.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}