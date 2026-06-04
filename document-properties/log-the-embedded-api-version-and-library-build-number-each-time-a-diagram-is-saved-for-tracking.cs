using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramProcessor
{
    // Loads a Visio diagram, logs its version and build number, then saves it.
    public void ProcessDiagram(string inputPath, string outputPath)
    {
        // Load the diagram using the provided constructor (lifecycle rule)
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Log the embedded API version and build number for tracking
            Console.WriteLine($"Visio Version: {diagram.Version}");
            Console.WriteLine($"Visio Build Number: {diagram.Buildnum}");

            // Choose a save format (e.g., VDX) and save using the provided Save method (lifecycle rule)
            diagram.Save(outputPath, SaveFileFormat.Vdx);
        }
    }

    // Example usage
    static void Main()
    {
        try
        {

            string inputFile = "sample.vsdx";
            string outputFile = "sample_saved.vdx";

            DiagramProcessor processor = new DiagramProcessor();
            processor.ProcessDiagram(inputFile, outputFile);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
