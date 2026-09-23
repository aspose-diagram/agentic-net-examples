using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Read the current center header text (null‑safe)
        string headerCenter = diagram.HeaderFooter.HeaderCenter ?? string.Empty;

        // Replace any occurrence of "Draft" with "Final"
        string updatedHeader = headerCenter.Replace("Draft", "Final");

        // Write the updated text back to the diagram
        diagram.HeaderFooter.HeaderCenter = updatedHeader;

        // Save the diagram (using VSDX format as an example)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
