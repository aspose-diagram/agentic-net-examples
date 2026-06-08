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

            // Path to the source Visio diagram (replace with actual file path)
            string diagramPath = "input.vsdx";

            // Path where the CSV export will be saved
            string csvPath = "output.csv";

            // Load the Visio diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Export shape data to CSV using the built‑in CSV format
            diagram.Save(csvPath, SaveFileFormat.Csv);

            // Inform the user that the export has completed
            Console.WriteLine("Diagram shape data exported to CSV at: " + csvPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
