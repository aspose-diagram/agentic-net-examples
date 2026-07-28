using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;

class SolutionXmlValidator
{
    // Path to the Visio diagram file
    private const string DiagramPath = "input.vsdx";

    // Path to the Visio XSD schema file
    private const string SchemaPath = "VisioSchema.xsd";

    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule)
            Diagram diagram = new Diagram(DiagramPath);

            // Prepare the XML schema set
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, SchemaPath);

            // Iterate through each SolutionXML in the diagram
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"Validating SolutionXML: {solutionXml.Name}");

                // Configure XML reader settings for schema validation
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    Schemas = schemaSet,
                    ValidationType = ValidationType.Schema,
                    DtdProcessing = DtdProcessing.Prohibit
                };
                settings.ValidationEventHandler += ValidationEventHandler;

                // Perform validation
                using (StringReader stringReader = new StringReader(solutionXml.XmlValue))
                using (XmlReader xmlReader = XmlReader.Create(stringReader, settings))
                {
                    try
                    {
                        while (xmlReader.Read()) { /* reading triggers validation */ }
                        Console.WriteLine("  Validation succeeded.");
                    }
                    catch (XmlException ex)
                    {
                        Console.WriteLine($"  XML parsing error: {ex.Message}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Handles validation errors and warnings
    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        string severity = e.Severity == XmlSeverityType.Error ? "Error" : "Warning";
        Console.WriteLine($"  {severity}: {e.Message}");
    }
}
