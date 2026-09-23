using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Record the start timestamp
            DateTime startTime = DateTime.UtcNow;

            // Perform the conversion (save to PDF in this example)
            string outputPath = "output.pdf";
            diagram.Save(outputPath, SaveFileFormat.Pdf);

            // Record the end timestamp
            DateTime endTime = DateTime.UtcNow;

            // Calculate and log the total conversion time
            TimeSpan duration = endTime - startTime;
            Console.WriteLine($"Conversion completed in {duration.TotalMilliseconds} ms.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
