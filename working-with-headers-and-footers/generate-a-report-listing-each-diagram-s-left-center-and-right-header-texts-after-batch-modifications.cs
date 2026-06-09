using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        // If a path is supplied as a command‑line argument it is used,
        // otherwise the current working directory is processed.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Get all Visio files (VSDX) in the folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

        foreach (string filePath in diagramFiles)
        {
            // Load the diagram from file.
            Diagram diagram = new Diagram(filePath);

            // ----- Batch modifications (example) -----
            // Update the global header texts.
            diagram.HeaderFooter.HeaderLeft = "Left Header";
            diagram.HeaderFooter.HeaderCenter = "Center Header";
            diagram.HeaderFooter.HeaderRight = "Right Header";

            // Save the modified diagram (optional, can be omitted if only reporting).
            string outputPath = Path.Combine(
                folderPath,
                Path.GetFileNameWithoutExtension(filePath) + "_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // ----- Reporting -----
            Console.WriteLine($"Diagram: {Path.GetFileName(filePath)}");
            Console.WriteLine($"  Header Left:   {diagram.HeaderFooter.HeaderLeft}");
            Console.WriteLine($"  Header Center: {diagram.HeaderFooter.HeaderCenter}");
            Console.WriteLine($"  Header Right:  {diagram.HeaderFooter.HeaderRight}");
        }
    }
}
