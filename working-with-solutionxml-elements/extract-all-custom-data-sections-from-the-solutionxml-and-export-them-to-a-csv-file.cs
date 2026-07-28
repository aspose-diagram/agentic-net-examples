using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses Aspose.Diagram's load functionality)
            Diagram diagram = new Diagram("input.vsdx");

            // Path for the CSV output
            string csvFilePath = "SolutionXmlData.csv";

            // Create a StreamWriter for the CSV file
            using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("Name,XmlValue");

                // Iterate through all SolutionXML entries in the diagram
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    // Ensure the XML value is not null and escape double quotes
                    string xmlValue = solXml.XmlValue?.Replace("\"", "\"\"") ?? string.Empty;

                    // Enclose fields in double quotes to handle commas, newlines, etc.
                    writer.WriteLine($"\"{solXml.Name}\",\"{xmlValue}\"");
                }
            }

            // (Optional) Save the diagram if any modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
