using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Find windows where rulers are hidden
            var windowsToEnable = diagram.Windows
                .Where(w => w.ShowRulers == BOOL.False)
                .ToList();

            // Enable rulers for those windows
            foreach (var window in windowsToEnable)
            {
                window.ShowRulers = BOOL.True;
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
