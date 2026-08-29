using System.IO;
using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Path to the Visio XSD schema file
            string schemaPath = "VisioSchema.xsd";

            // Prepare the schema set
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, schemaPath);

            // List to collect validation messages
            List<string> validationMessages = new List<string>();

            // Iterate over each SolutionXML in the diagram
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                string xmlContent = solXml.XmlValue;
                if (string.IsNullOrWhiteSpace(xmlContent))
                    continue; // Skip empty entries

                // Configure XML reader settings for schema validation
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    Schemas = schemaSet,
                    ValidationType = ValidationType.Schema,
                    ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
                };

                // Capture validation events
                settings.ValidationEventHandler += (sender, e) =>
                {
                    string msg = $"SolutionXML '{solXml.Name}': {e.Severity} - {e.Message}";
                    validationMessages.Add(msg);
                };

                // Perform validation
                using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlContent), settings))
                {
                    try
                    {
                        while (reader.Read()) { }
                    }
                    catch (XmlException ex)
                    {
                        validationMessages.Add($"SolutionXML '{solXml.Name}': XML parsing error - {ex.Message}");
                    }
                }
            }

            // Output validation results
            if (validationMessages.Count == 0)
            {
                Console.WriteLine("All SolutionXML entries are valid against the Visio schema.");
            }
            else
            {
                Console.WriteLine("Validation errors/warnings:");
                foreach (string msg in validationMessages)
                    Console.WriteLine(msg);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
