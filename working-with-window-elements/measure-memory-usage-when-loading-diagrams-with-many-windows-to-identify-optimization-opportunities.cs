using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide a folder path containing diagram files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get diagram files with common extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".mmd", ".html" };
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        var diagramFiles = Array.FindAll(files, f => Array.Exists(extensions, e => f.EndsWith(e, StringComparison.OrdinalIgnoreCase)));

        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No diagram files found in the specified folder.");
            return;
        }

        // Process each diagram file.
        foreach (string filePath in diagramFiles)
        {
            // Guard: ensure the file exists (redundant after GetFiles, but required by rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File does not exist: {filePath}");
                continue;
            }

            try
            {
                // Capture memory usage before loading.
                long memoryBefore = Process.GetCurrentProcess().PrivateMemorySize64;

                // Load the diagram.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Capture memory usage after loading.
                    long memoryAfter = Process.GetCurrentProcess().PrivateMemorySize64;

                    // Calculate memory delta.
                    long memoryDelta = memoryAfter - memoryBefore;

                    // Count windows in the diagram.
                    int windowCount = diagram.Windows.Count;

                    // Output results.
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Windows: {windowCount}");
                    Console.WriteLine($"  Memory before load: {memoryBefore:N0} bytes");
                    Console.WriteLine($"  Memory after load : {memoryAfter:N0} bytes");
                    Console.WriteLine($"  Memory increase   : {memoryDelta:N0} bytes");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during loading or processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}