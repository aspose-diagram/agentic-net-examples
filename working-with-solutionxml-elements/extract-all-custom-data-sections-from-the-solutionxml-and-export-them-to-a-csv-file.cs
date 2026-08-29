using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";

            // Path to the output CSV file
            string csvPath = "solutionxml.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Create a CSV file and write the header
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("Name,XmlValue");

                // Iterate through each SolutionXML entry in the diagram
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // Escape fields to handle commas, quotes, and newlines
                    string name = EscapeCsv(solXml.Name);
                    string xml = EscapeCsv(solXml.XmlValue);

                    // Write a CSV line for the current SolutionXML
                    writer.WriteLine($"{name},{xml}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    static string EscapeCsv(string field)
    {
        if (field == null) return string.Empty;

        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");
        if (mustQuote)
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
