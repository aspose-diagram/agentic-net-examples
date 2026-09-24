using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: SolutionXmlExport <inputVisioFile> <outputCsvFile>");
                return;
            }

            string visioPath = args[0];
            string csvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Create or overwrite the CSV file
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                // Write CSV header
                writer.WriteLine("\"Name\",\"XmlValue\"");

                // Iterate through all SolutionXML elements
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Escape double quotes in values by doubling them
                    string nameEscaped = solutionXml.Name?.Replace("\"", "\"\"") ?? string.Empty;
                    string xmlEscaped = solutionXml.XmlValue?.Replace("\"", "\"\"") ?? string.Empty;

                    // Write a CSV line with quoted fields
                    writer.WriteLine($"\"{nameEscaped}\",\"{xmlEscaped}\"");
                }
            }

            Console.WriteLine($"Export completed. CSV saved to: {csvPath}");
        }
    }