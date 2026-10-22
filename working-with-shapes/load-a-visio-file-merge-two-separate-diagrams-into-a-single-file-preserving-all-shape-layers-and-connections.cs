using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first diagram path, second diagram path, output path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <FirstDiagramPath> <SecondDiagramPath> <OutputPath>");
            return;
        }

        string firstDiagramPath = args[0];
        string secondDiagramPath = args[1];
        string outputPath = args[2];

        // Guard: ensure the first input file exists
        if (!File.Exists(firstDiagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {firstDiagramPath}");
            return;
        }

        // Guard: ensure the second input file exists
        if (!File.Exists(secondDiagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {secondDiagramPath}");
            return;
        }

        try
        {
            // Load the first diagram inside a using block as required
            using (Diagram targetDiagram = new Diagram(firstDiagramPath))
            {
                // Load the second diagram inside a nested using block
                using (Diagram sourceDiagram = new Diagram(secondDiagramPath))
                {
                    // Merge the source diagram into the target diagram, preserving layers and connections
                    targetDiagram.Combine(sourceDiagram);
                }

                // Save the merged diagram to the specified output path in VSDX format
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagrams merged successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}