using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the Visio files to be merged
            string firstVisioPath = "Diagram1.vsdx";
            string secondVisioPath = "Diagram2.vsdx";

            // Path for the merged output file
            string outputPath = "MergedDiagram.vsdx";

            // Load the first diagram (target diagram)
            Diagram targetDiagram = new Diagram(firstVisioPath);

            // Load the second diagram (source diagram)
            Diagram sourceDiagram = new Diagram(secondVisioPath);

            // Merge the source diagram into the target diagram.
            // This copies all pages, masters, layers, and connections.
            targetDiagram.Combine(sourceDiagram);

            // Save the merged diagram.
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
