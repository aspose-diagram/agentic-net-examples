using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram from the specified file.
        Diagram diagram = new Diagram(inputPath);

        // Retrieve the current text of the center header.
        string headerCenter = diagram.HeaderFooter.HeaderCenter;

        // Replace any occurrence of "Draft" with "Final".
        if (!string.IsNullOrEmpty(headerCenter) && headerCenter.Contains("Draft"))
        {
            string updatedHeader = headerCenter.Replace("Draft", "Final");
            diagram.HeaderFooter.HeaderCenter = updatedHeader;
        }

        // Save the modified diagram. Adjust the format as needed (Vsdx used here).
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
