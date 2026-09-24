using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for input Visio file path
        Console.WriteLine("Enter the path to the Visio file to process:");
        string inputPath = Console.ReadLine();

        // Prompt for the font name to replace
        Console.WriteLine("Enter the font name to replace (exact match):");
        string oldFontName = Console.ReadLine();

        // Prompt for the new font name
        Console.WriteLine("Enter the new font name:");
        string newFontName = Console.ReadLine();

        // Prompt for the output file path
        Console.WriteLine("Enter the path for the output Visio file:");
        string outputPath = Console.ReadLine();

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate over all SolutionXML elements and replace the font name in the XML content
        foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
        {
            if (!string.IsNullOrEmpty(solutionXml.XmlValue))
            {
                solutionXml.XmlValue = solutionXml.XmlValue.Replace(oldFontName, newFontName);
            }
        }

        // Save the modified diagram (using VSDX format as an example)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine("Font replacement completed and diagram saved to: " + outputPath);
    }
}
