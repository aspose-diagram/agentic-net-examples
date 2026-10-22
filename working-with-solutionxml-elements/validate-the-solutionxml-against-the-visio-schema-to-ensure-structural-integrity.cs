using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: Visio file path and Visio schema (XSD) file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <SchemaFilePath>");
            return;
        }

        string visioPath = args[0];
        string schemaPath = args[1];

        // Guard against missing Visio file
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard against missing schema file
        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"Schema file not found: {schemaPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio diagram: {ex.Message}");
            return;
        }

        // Prepare XML schema set
        XmlSchemaSet schemaSet = new XmlSchemaSet();
        try
        {
            using (FileStream schemaStream = File.OpenRead(schemaPath))
            {
                schemaSet.Add(null, XmlReader.Create(schemaStream));
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading schema: {ex.Message}");
            return;
        }

        bool anyInvalid = false;

        // Iterate over all SolutionXML elements in the diagram
        foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
        {
            string xmlContent = solutionXml.XmlValue;
            if (string.IsNullOrWhiteSpace(xmlContent))
            {
                Console.WriteLine($"SolutionXML \"{solutionXml.Name}\" is empty.");
                continue;
            }

            // Validate the XML content against the schema
            string validationErrors;
            bool isValid = ValidateXmlAgainstSchema(xmlContent, schemaSet, out validationErrors);

            if (isValid)
            {
                Console.WriteLine($"SolutionXML \"{solutionXml.Name}\" is valid.");
            }
            else
            {
                anyInvalid = true;
                Console.Error.WriteLine($"SolutionXML \"{solutionXml.Name}\" is INVALID:");
                Console.Error.WriteLine(validationErrors);
            }
        }

        if (anyInvalid)
        {
            Console.Error.WriteLine("One or more SolutionXML elements failed validation.");
        }
        else
        {
            Console.WriteLine("All SolutionXML elements passed validation.");
        }
    }

    /// <summary>
    /// Validates an XML string against a preloaded XmlSchemaSet.
    /// </summary>
    /// <param name="xmlContent">The XML content to validate.</param>
    /// <param name="schemaSet">The compiled schema set.</param>
    /// <param name="errors">Aggregated validation error messages.</param>
    /// <returns>True if XML is valid; otherwise false.</returns>
    private static bool ValidateXmlAgainstSchema(string xmlContent, XmlSchemaSet schemaSet, out string errors)
    {
        bool isValid = true;
        StringBuilder errorBuilder = new StringBuilder();

        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemaSet,
            DtdProcessing = DtdProcessing.Prohibit
        };
        settings.ValidationEventHandler += (sender, e) =>
        {
            isValid = false;
            errorBuilder.AppendLine($"Line {e.Exception?.LineNumber}, Position {e.Exception?.LinePosition}: {e.Message}");
        };

        try
        {
            using (StringReader stringReader = new StringReader(xmlContent))
            using (XmlReader reader = XmlReader.Create(stringReader, settings))
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }
        }
        catch (Exception ex)
        {
            isValid = false;
            errorBuilder.AppendLine($"Exception during validation: {ex.Message}");
        }

        errors = errorBuilder.ToString();
        return isValid;
    }
}