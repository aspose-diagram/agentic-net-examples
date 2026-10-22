using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Check if any SolutionXML elements are present
            if (diagram.SolutionXMLs == null || diagram.SolutionXMLs.Count == 0)
            {
                Console.WriteLine("No SolutionXML elements found in the diagram.");
                return;
            }

            // Iterate through all SolutionXML elements and output their details
            Console.WriteLine($"Found {diagram.SolutionXMLs.Count} SolutionXML element(s):");
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                // Output the name identifier of the SolutionXML element
                Console.WriteLine($"Name: {solutionXml.Name}");

                // Output the stored XML content
                Console.WriteLine("XML Value:");
                Console.WriteLine(solutionXml.XmlValue);
                Console.WriteLine(new string('-', 40));
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}