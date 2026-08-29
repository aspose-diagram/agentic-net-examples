using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class MemoryMeasurement
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram that contains many windows.
            // Replace with the actual file path when running the code.
            string diagramPath = @"C:\Diagrams\LargeWindowDiagram.vsdx";

            // Force a full garbage collection before measuring baseline memory.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Record memory usage before loading the diagram.
            long memoryBefore = GC.GetTotalMemory(true);

            // Load the diagram using the provided constructor.
            Diagram diagram = new Diagram(diagramPath);

            // Record memory usage after loading the diagram.
            long memoryAfter = GC.GetTotalMemory(true);

            // Calculate the memory delta.
            long memoryDelta = memoryAfter - memoryBefore;

            // Output the results.
            Console.WriteLine($"Memory before loading: {memoryBefore:N0} bytes");
            Console.WriteLine($"Memory after loading : {memoryAfter:N0} bytes");
            Console.WriteLine($"Memory increase      : {memoryDelta:N0} bytes");

            // Report the number of windows present in the loaded diagram.
            Console.WriteLine($"Number of windows    : {diagram.Windows.Count}");

            // Optionally, iterate through windows to display some properties.
            for (int i = 0; i < diagram.Windows.Count; i++)
            {
                Window win = diagram.Windows[i];
                Console.WriteLine($"Window {i + 1}: Type={win.WindowType}, Width={win.WindowWidth}, Height={win.WindowHeight}");
            }

            // Clean up resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
