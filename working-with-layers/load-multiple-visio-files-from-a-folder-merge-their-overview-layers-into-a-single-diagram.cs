using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Folder containing the Visio files to be merged
            string sourceFolder = @"C:\VisioFiles";

            // Output file for the merged diagram
            string outputFile = @"C:\Merged\OverviewMerged.vsdx";

            // Create an empty diagram that will hold the merged content
            Diagram masterDiagram = new Diagram();

            // Iterate through all Visio files in the folder (adjust the pattern if needed)
            foreach (string filePath in Directory.GetFiles(sourceFolder, "*.vsdx"))
            {
                // Load the current Visio file
                Diagram sourceDiagram = new Diagram(filePath);

                // Combine the source diagram into the master diagram
                masterDiagram.Combine(sourceDiagram);
            }

            // Save the merged diagram to the specified file in VSDX format
            masterDiagram.Save(outputFile, SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
