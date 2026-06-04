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

            // Load an existing Visio diagram (replace with your actual file path)
            var diagram = new Diagram("input.vsdx");

            // Prepare save options (optional, can be omitted if not needed)
            var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);

            // Record start time before saving
            var stopwatch = Stopwatch.StartNew();

            // Save the diagram to a new file
            diagram.Save("output.vdx", saveOptions);

            // Stop the timer after saving completes
            stopwatch.Stop();

            // Log the duration of the save operation
            Console.WriteLine($"Diagram saved in {stopwatch.ElapsedMilliseconds} ms.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
