using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source diagrams
                string firstDiagramPath = "FirstDiagram.vsd";   // VSD file (source order)
                string secondDiagramPath = "SecondDiagram.vsdx"; // VSDX file to be appended

                // Load the first diagram (target) – its page order will be preserved
                Diagram targetDiagram = new Diagram(firstDiagramPath);

                // Load the second diagram (source) – its pages will be added after the target's pages
                Diagram sourceDiagram = new Diagram(secondDiagramPath);

                // Combine the source diagram into the target diagram
                // This merges all pages, masters, and other resources from sourceDiagram into targetDiagram
                targetDiagram.Combine(sourceDiagram);

                // Save the combined diagram. The result is saved as VSDX to retain modern format.
                string outputPath = "CombinedDiagram.vsdx";
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagrams combined successfully. Output saved to: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }