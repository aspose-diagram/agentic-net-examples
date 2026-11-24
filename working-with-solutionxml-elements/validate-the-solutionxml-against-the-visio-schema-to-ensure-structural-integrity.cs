using System;
using System.IO;
using Aspose.Diagram;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: Visio file path and XML schema file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <SchemaFilePath>");
            return;
        }

        string visioPath = args[0];
        string schemaPath = args[1];

        // Guard: ensure Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard: ensure schema file exists.
        if (!File.Exists(schemaPath))
        {
            Console.Error.WriteLine($"Schema file not found: {schemaPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio file: {ex.Message}");
            return;
        }

        // Prepare XML schema set.
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
            Console.Error.WriteLine($"Error loading XML schema: {ex.Message}");
            return;
        }

        bool anyValidationError = false;

        // Iterate over all SolutionXML elements in the diagram.
        foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
        {
            Console.WriteLine($"Validating SolutionXML: {solutionXml.Name}");

            // Prepare settings for validation.
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemaSet
            };
            settings.ValidationEventHandler += (sender, e) =>
            {
                anyValidationError = true;
                Console.Error.WriteLine($"Validation error in '{solutionXml.Name}': {e.Message}");
            };

            // Validate the XML content.
            try
            {
                using (StringReader stringReader = new StringReader(solutionXml.XmlValue))
                using (XmlReader reader = XmlReader.Create(stringReader, settings))
                {
                    while (reader.Read()) { /* reading triggers validation */ }
                }
            }
            catch (Exception ex)
            {
                anyValidationError = true;
                Console.Error.WriteLine($"Exception during validation of '{solutionXml.Name}': {ex.Message}");
            }
        }

        // Report overall result.
        if (anyValidationError)
        {
            Console.Error.WriteLine("One or more SolutionXML elements failed validation.");
        }
        else
        {
            Console.WriteLine("All SolutionXML elements passed validation against the provided schema.");
        }
    }
}