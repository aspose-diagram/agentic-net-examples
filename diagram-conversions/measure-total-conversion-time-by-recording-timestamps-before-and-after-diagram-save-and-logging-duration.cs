using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source diagram file
            string inputPath = "input.vsdx";

            // Path where the converted diagram will be saved
            string outputPath = "output.vdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Start measuring time before the save operation
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Save the diagram in VDX format
            diagram.Save(outputPath, SaveFileFormat.Vdx);

            // Stop the timer after saving completes
            stopwatch.Stop();

            // Log the elapsed time in milliseconds
            Console.WriteLine($"Total conversion time: {stopwatch.Elapsed.TotalMilliseconds} ms");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
