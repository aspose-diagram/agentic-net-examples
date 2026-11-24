using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, old font name, new font name, output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <oldFontName> <newFontName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string oldFontName = args[1];
        string newFontName = args[2];
        string outputPath = args[3];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all SolutionXML elements and replace the font name in their XML content
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                if (solutionXml.XmlValue != null && solutionXml.XmlValue.Contains(oldFontName))
                {
                    solutionXml.XmlValue = solutionXml.XmlValue.Replace(oldFontName, newFontName);
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Font replacement completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}