using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class ExportSolutionXmlToJson
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputVisioPath = "input.vsd";

            // Path where the formatted JSON will be saved
            string outputJsonPath = "solutionxml.json";

            // Load the Visio diagram (uses Aspose.Diagram's constructor)
            Diagram diagram = new Diagram(inputVisioPath);

            // Collect each SolutionXML entry (Name and XmlValue) into a list
            var solutionXmlItems = new List<object>();
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                solutionXmlItems.Add(new
                {
                    Name = solXml.Name,
                    XmlValue = solXml.XmlValue
                });
            }

            // Serialize the list to formatted (indented) JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonContent = JsonSerializer.Serialize(solutionXmlItems, jsonOptions);

            // Write the JSON string to the output file
            File.WriteAllText(outputJsonPath, jsonContent);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
