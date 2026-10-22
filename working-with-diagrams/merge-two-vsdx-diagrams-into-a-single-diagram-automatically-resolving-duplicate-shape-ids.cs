using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: first diagram path, second diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: MergeDiagrams <first.vsdx> <second.vsdx> <output.vsdx>");
            return;
        }

        string firstPath = args[0];
        string secondPath = args[1];
        string outputPath = args[2];

        // Guard clauses for input files
        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        try
        {
            // Load the first diagram (target diagram)
            Diagram targetDiagram = new Diagram(firstPath);

            // Load the second diagram (source diagram to be merged)
            Diagram sourceDiagram = new Diagram(secondPath);

            // Combine the source diagram into the target diagram.
            // Aspose.Diagram.Combine automatically merges pages, masters and resolves duplicate shape IDs.
            targetDiagram.Combine(sourceDiagram);

            // Save the merged diagram to the specified output path in VSDX format.
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagrams merged successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}