using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;

class MemoryUsageDemo
{
    static void Main()
    {
        try
        {

            // Path to the Visio file that contains many windows
            string filePath = @"C:\Diagrams\LargeWindowsDiagram.vsdx";

            // Force garbage collection and get baseline memory usage (in bytes)
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            long beforeLoad = GC.GetTotalMemory(forceFullCollection: true);

            // Load the diagram using the provided constructor (Diagram(string))
            Diagram diagram = new Diagram(filePath);

            // Measure memory after loading
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            long afterLoad = GC.GetTotalMemory(forceFullCollection: true);

            // Calculate memory delta
            long memoryUsed = afterLoad - beforeLoad;

            // Retrieve the number of Window elements in the document
            int windowCount = diagram.Windows.Count;

            // Output the results
            Console.WriteLine($"Memory used to load diagram: {memoryUsed:N0} bytes");
            Console.WriteLine($"Number of windows in the diagram: {windowCount}");

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
