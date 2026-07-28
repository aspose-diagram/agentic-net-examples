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
            Console.WriteLine("Usage: <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through every page and every shape on each page.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Rotate the shape's text by 180 degrees.
                // Text rotation is specified in radians; 180° = π radians.
                shape.TextXForm.TxtAngle.Value = Math.PI;
            }
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
