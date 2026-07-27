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

            // Paths for the source diagram and the output file
            string inputPath = "input.vsdx";
            string outputPath = "output.vdx";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(inputPath);

            // Optional: configure save options (e.g., specify format)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            // saveOptions.AutoFitPageToDrawingContent = true; // example of additional option

            // Record start time
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Save the diagram using the configured options
            diagram.Save(outputPath, saveOptions);

            // Record end time
            stopwatch.Stop();

            // Log the elapsed time
            Console.WriteLine($"Diagram saved in {stopwatch.Elapsed.TotalMilliseconds} ms.");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
