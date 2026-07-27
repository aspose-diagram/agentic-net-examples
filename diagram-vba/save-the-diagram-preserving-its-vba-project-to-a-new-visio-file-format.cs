using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create save options for a macro‑enabled Visio format (VSDM)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdm);
            // Explicitly set the format (optional, but clarifies intent)
            saveOptions.SaveFormat = SaveFileFormat.Vsdm;

            // Save the diagram to a new file while preserving its VBA project
            diagram.Save("output.vsdm", saveOptions);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
