using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // High quality rendering (value range 0-100)
                Quality = 100
            };

            string outputPath = "output.svg";
            diagram.Save(outputPath, svgOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}