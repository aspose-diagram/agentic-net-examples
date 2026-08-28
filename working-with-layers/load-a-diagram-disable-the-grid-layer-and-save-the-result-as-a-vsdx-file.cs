using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the existing Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages and disable the layer named "Grid"
        foreach (Page page in diagram.Pages)
        {
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Grid")
                {
                    layer.Visible.Value = BOOL.False;
                }
            }
        }

        // Save the modified diagram as VSDX
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
