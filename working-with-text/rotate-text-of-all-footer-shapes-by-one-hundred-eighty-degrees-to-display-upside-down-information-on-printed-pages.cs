using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // 180 degrees in radians.
        double angleRadians = Math.PI;

        // Iterate through all pages and their shapes.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Identify footer shapes by name (case‑insensitive contains "Footer").
                string nameU = shape.NameU ?? string.Empty;
                if (nameU.IndexOf("Footer", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Rotate the text block of the shape by 180°.
                    shape.TextXForm.TxtAngle.Value = angleRadians;
                }
            }
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
