using System.IO;
using System;
using Aspose.Diagram;

class MergeOleObjects
{
    static void Main()
    {
        try
        {

            // Paths of the source Visio files containing OLE objects
            string[] sourceFiles = new string[]
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx",
                "Diagram3.vsdx"
            };

            // Create an empty master diagram
            using (Diagram masterDiagram = new Diagram())
            {
                // Load each source diagram and combine it into the master diagram
                foreach (string filePath in sourceFiles)
                {
                    using (Diagram sourceDiagram = new Diagram(filePath))
                    {
                        // Combine preserves the original positions of all shapes, including OLE objects
                        masterDiagram.Combine(sourceDiagram);
                    } // sourceDiagram disposed here
                }

                // Save the merged master diagram to a new file
                masterDiagram.Save("MasterDiagram.vsdx", SaveFileFormat.Vsdx);
            } // masterDiagram disposed here

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
