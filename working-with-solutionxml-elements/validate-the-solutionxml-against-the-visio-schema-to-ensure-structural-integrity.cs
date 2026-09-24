using System.IO;
using System;
using System.Xml;
using System.Xml.Schema;

public class VisioXmlValidator
{
    // Validates a Visio SolutionXML file against the Visio XSD schema.
    // Returns true if the XML conforms to the schema; otherwise false.
    public static bool Validate(string xmlFilePath, string xsdFilePath)
    {
        bool isValid = true;

        // Set up the XML schema set and add the Visio schema.
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add(null, xsdFilePath); // No target namespace specified for Visio schema.

        // Configure the XML reader settings for validation.
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = schemas,
            // Stop on the first validation error if desired.
            // ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings
        };

        // Attach a validation event handler to capture any schema violations.
        settings.ValidationEventHandler += (sender, args) =>
        {
            // Mark the document as invalid and output the error details.
            isValid = false;
            Console.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        // Create an XML reader that validates while reading the document.
        using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
        {
            try
            {
                // Parse the entire XML document. Validation occurs automatically.
                while (reader.Read()) { }
            }
            catch (XmlException ex)
            {
                // XML is not well-formed.
                isValid = false;
                Console.WriteLine($"XML parsing error: {ex.Message}");
            }
        }

        return isValid;
    }

    // Example usage.
    public static void Main()
    {
        try
        {

            string xmlPath = "Solution.xml";   // Path to the Visio SolutionXML file.
            string xsdPath = "VisioSchema.xsd"; // Path to the Visio XSD schema file.

            bool result = Validate(xmlPath, xsdPath);
            Console.WriteLine(result
                ? "SolutionXML is valid against the Visio schema."
                : "SolutionXML failed validation against the Visio schema.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
