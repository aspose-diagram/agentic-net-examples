using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class MemoryUsageMeasurement
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram that contains many windows.
        // Adjust the file name as needed.
        string diagramPath = "LargeDiagram.vsdx";

        // Ensure the file exists.
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Get the current process to read memory statistics.
        Process currentProcess = Process.GetCurrentProcess();

        // Force a full garbage collection before measurement to get a clean baseline.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Record memory usage before loading the diagram.
        long memoryBeforeGC = GC.GetTotalMemory(forceFullCollection: true);
        long privateBytesBefore = currentProcess.PrivateMemorySize64;

        Console.WriteLine("Memory usage BEFORE loading diagram:");
        Console.WriteLine($"  GC.GetTotalMemory: {memoryBeforeGC:N0} bytes");
        Console.WriteLine($"  Process.PrivateMemorySize64: {privateBytesBefore:N0} bytes");

        // Load the diagram.
        Diagram diagram = new Diagram(diagramPath);

        // Force a garbage collection again to stabilize memory after loading.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Record memory usage after loading the diagram.
        long memoryAfterGC = GC.GetTotalMemory(forceFullCollection: true);
        long privateBytesAfter = currentProcess.PrivateMemorySize64;

        Console.WriteLine("\nMemory usage AFTER loading diagram:");
        Console.WriteLine($"  GC.GetTotalMemory: {memoryAfterGC:N0} bytes");
        Console.WriteLine($"  Process.PrivateMemorySize64: {privateBytesAfter:N0} bytes");

        // Calculate differences.
        long gcMemoryDiff = memoryAfterGC - memoryBeforeGC;
        long privateMemoryDiff = privateBytesAfter - privateBytesBefore;

        Console.WriteLine("\nMemory increase due to loading:");
        Console.WriteLine($"  GC.GetTotalMemory increase: {gcMemoryDiff:N0} bytes");
        Console.WriteLine($"  Process.PrivateMemorySize64 increase: {privateMemoryDiff:N0} bytes");

        // Optional: iterate through windows to ensure they are loaded.
        Console.WriteLine($"\nNumber of windows in the diagram: {diagram.Windows.Count}");

        // Cleanup.
        diagram = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
