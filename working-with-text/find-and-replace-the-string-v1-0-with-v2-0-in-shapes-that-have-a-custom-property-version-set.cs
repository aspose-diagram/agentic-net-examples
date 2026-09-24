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
            Console.WriteLine("Usage: <program> <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages and shapes.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a Props collection.
                if (shape.Props == null)
                    continue;

                // Look for a custom property named "Version".
                foreach (Prop prop in shape.Props)
                {
                    if (prop.Name == "Version")
                    {
                        string currentValue = prop.Value.Val ?? string.Empty;

                        // Replace "v1.0" with "v2.0" if present.
                        if (currentValue.Contains("v1.0"))
                        {
                            prop.Value.Val = currentValue.Replace("v1.0", "v2.0");
                            Console.WriteLine($"Updated shape ID {shape.ID} on page {page.ID}.");
                        }
                    }
                }
            }
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved to " + outputPath);
    }
}
