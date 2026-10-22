using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <oldFontName> <newFontName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string oldFontName = args[1];
        string newFontName = args[2];
        string outputPath = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all SolutionXML elements and replace font names.
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (solXml.XmlValue != null && solXml.XmlValue.Contains(oldFontName))
                {
                    // Replace all occurrences of the old font name with the new one.
                    solXml.XmlValue = solXml.XmlValue.Replace(oldFontName, newFontName);
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}