using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found -> {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Check if any SolutionXML elements exist.
            if (diagram.SolutionXMLs.Count == 0)
            {
                Console.WriteLine("No SolutionXML elements found in the diagram.");
                return;
            }

            // Iterate through all SolutionXML elements and output their data.
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"Name: {solutionXml.Name}");
                Console.WriteLine("XML Value:");
                Console.WriteLine(solutionXml.XmlValue);
                Console.WriteLine(new string('-', 40));
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}