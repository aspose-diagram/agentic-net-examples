using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output path (second argument)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Set the footer text to display the page number dynamically.
        // '&p' is the Visio field code for the current page number.
        diagram.HeaderFooter.FooterRight = "Page: &p";

        // Save the modified diagram (preserving the original format)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Footer added and diagram saved to '{outputPath}'.");
    }
}
