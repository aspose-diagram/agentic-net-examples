using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input file path, output file path, desired uniform width (in inches)
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <input.vsdx> <output.vsdx> <widthInInches>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double uniformWidth = double.Parse(args[2]);

        // Load the Visio document
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Loop through every page in the document
            foreach (Page page in diagram.Pages)
            {
                // Loop through every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Set the shape's width to the uniform value
                    shape.SetWidth(uniformWidth);
                }
            }

            // Save the modified diagram back to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }
}
