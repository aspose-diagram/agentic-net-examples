using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output CSV file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "solutionxml_export.csv";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare to write CSV output
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("Name,XmlValue");

                    // Iterate over all SolutionXML elements in the diagram
                    foreach (SolutionXML solXml in diagram.SolutionXMLs)
                    {
                        // Retrieve the name and XML content
                        string name = solXml.Name ?? string.Empty;
                        string xml = solXml.XmlValue ?? string.Empty;

                        // Escape double quotes by doubling them
                        string escapedName = name.Replace("\"", "\"\"");
                        string escapedXml = xml.Replace("\"", "\"\"");

                        // Enclose fields in double quotes to handle commas and newlines
                        writer.WriteLine($"\"{escapedName}\",\"{escapedXml}\"");
                    }
                }

                Console.WriteLine($"Successfully exported SolutionXML data to CSV: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}