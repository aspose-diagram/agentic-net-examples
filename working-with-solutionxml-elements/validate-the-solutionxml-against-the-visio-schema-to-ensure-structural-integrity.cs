using System.IO;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;

class SolutionXmlValidator
{
    // Path to the Visio XSD schema file
    private const string VisioSchemaPath = @"C:\Schemas\Visio.xsd";

    // Path to the Visio document to be validated
    private const string DiagramPath = @"C:\Diagrams\sample.vsdx";

    static void Main()
    {
        try
        {

            // Load the Visio diagram (using Aspose.Diagram load rule)
            Diagram diagram = new Diagram(DiagramPath);

            // Prepare the XML schema set
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, VisioSchemaPath);

            // List to collect validation errors
            List<string> validationErrors = new List<string>();

            // Iterate over all SolutionXML entries in the diagram
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Load the XML value into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(solXml.XmlValue);
                }
                catch (XmlException ex)
                {
                    validationErrors.Add($"SolutionXML '{solXml.Name}' is not well‑formed XML: {ex.Message}");
                    continue;
                }

                // Set up validation settings
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema,
                    Schemas = schemaSet,
                    ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
                };
                settings.ValidationEventHandler += (sender, args) =>
                {
                    string msg = $"SolutionXML '{solXml.Name}': {args.Severity} - {args.Message}";
                    validationErrors.Add(msg);
                };

                // Perform validation using an XmlReader
                using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlDoc.OuterXml), settings))
                {
                    try
                    {
                        while (reader.Read()) { } // Trigger validation
                    }
                    catch (XmlException ex)
                    {
                        validationErrors.Add($"SolutionXML '{solXml.Name}' validation error: {ex.Message}");
                    }
                }
            }

            // Output validation results
            if (validationErrors.Count == 0)
            {
                Console.WriteLine("All SolutionXML entries are valid against the Visio schema.");
            }
            else
            {
                Console.WriteLine("Validation errors found:");
                foreach (string err in validationErrors)
                {
                    Console.WriteLine(err);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
