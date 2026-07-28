using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file that contains many windows.
            string filePath = args.Length > 0 ? args[0] : "LargeDiagram.vsdx";

            // Force a full garbage collection and get the memory usage before loading.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            long memoryBefore = GC.GetTotalMemory(true);

            // Load the diagram using the provided constructor (load rule).
            Diagram diagram = new Diagram(filePath);

            // Force a full garbage collection and get the memory usage after loading.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            long memoryAfter = GC.GetTotalMemory(true);

            // Calculate the memory consumed by loading the diagram.
            long memoryUsed = memoryAfter - memoryBefore;

            // Retrieve the number of windows in the loaded diagram.
            int windowCount = diagram.Windows?.Count ?? 0;

            // Output the results.
            Console.WriteLine($"File: {filePath}");
            Console.WriteLine($"Memory before load: {memoryBefore:N0} bytes");
            Console.WriteLine($"Memory after load : {memoryAfter:N0} bytes");
            Console.WriteLine($"Memory used by load: {memoryUsed:N0} bytes");
            Console.WriteLine($"Number of windows  : {windowCount}");

            // Clean up.
            diagram.Dispose();

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
