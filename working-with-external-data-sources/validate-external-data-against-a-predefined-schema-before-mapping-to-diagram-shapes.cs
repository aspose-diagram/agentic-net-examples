using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;

class Program
{
    // Path to the Visio diagram to be processed
    private const string DiagramPath = @"C:\Diagrams\InputDiagram.vsdx";

    // Path to the external XML data file
    private const string XmlDataPath = @"C:\Data\ExternalData.xml";

    // Path to the XSD schema that defines the expected structure of the XML data
    private const string SchemaPath = @"C:\Data\ExternalDataSchema.xsd";

    // Path where the updated diagram will be saved
    private const string OutputDiagramPath = @"C:\Diagrams\OutputDiagram.vsdx";

    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram(DiagramPath);

            // Load and validate the external XML data against the XSD schema
            string xmlContent = File.ReadAllText(XmlDataPath);
            bool isValid = ValidateXml(xmlContent, SchemaPath, out string validationErrors);

            if (!isValid)
            {
                Console.WriteLine("XML validation failed:");
                Console.WriteLine(validationErrors);
                // Abort further processing because data does not conform to the schema
                return;
            }

            // Create a DataRecordSet and assign the validated XML as its ADOData
            DataRecordSet dataRecordSet = new DataRecordSet
            {
                Name = "ExternalDataSet",
                ID = 1,
                ADOData = xmlContent
            };

            // Add the DataRecordSet to the diagram (lifecycle rule: create)
            diagram.DataRecordSets.Add(dataRecordSet);

            // OPTIONAL: Refresh the DataRecordSet to ensure any linked shapes are updated
            // (if the diagram contains shapes linked to this DataRecordSet)
            dataRecordSet.Refresh();

            // Save the updated diagram (lifecycle rule: save)
            diagram.Save(OutputDiagramPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved successfully to: " + OutputDiagramPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Validates an XML string against an XSD schema.
    /// </summary>
    /// <param name="xml">The XML content to validate.</param>
    /// <param name="xsdPath">Path to the XSD schema file.</param>
    /// <param name="errors">Aggregated validation error messages.</param>
    /// <returns>True if XML is valid; otherwise false.</returns>
    private static bool ValidateXml(string xml, string xsdPath, out string errors)
    {
        bool isValid = true;
        StringWriter errorWriter = new StringWriter();

        // Set up the XML schema set
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdPath);

        // Configure XML reader settings for validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas
        };
        settings.ValidationEventHandler += (sender, e) =>
        {
            isValid = false;
            errorWriter.WriteLine($"{e.Severity}: {e.Message}");
        };

        // Perform validation using an XmlReader
        using (StringReader stringReader = new StringReader(xml))
        using (XmlReader reader = XmlReader.Create(stringReader, settings))
        {
            try
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
            catch (XmlException ex)
            {
                isValid = false;
                errorWriter.WriteLine($"XML Exception: {ex.Message}");
            }
        }

        errors = errorWriter.ToString();
        return isValid;
    }
}
