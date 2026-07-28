using System.IO;
using System;
using Aspose.Diagram;

class MergeOleObjects
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio files containing OLE objects
            string[] sourceFiles = new string[]
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx",
                "Diagram3.vsdx"
            };

            // Create an empty master diagram
            Diagram masterDiagram = new Diagram();

            // Iterate through each source diagram, load it, and combine into the master diagram
            foreach (string filePath in sourceFiles)
            {
                // Load the source diagram from file
                Diagram sourceDiagram = new Diagram(filePath);

                // Combine the source diagram into the master diagram.
                // This preserves the original positions of all shapes, including OLE objects.
                masterDiagram.Combine(sourceDiagram);

                // Dispose the source diagram to free resources
                sourceDiagram.Dispose();
            }

            // Save the merged master diagram to a new file
            masterDiagram.Save("MergedMasterDiagram.vsdx", SaveFileFormat.Vsdx);

            // Clean up
            masterDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
