using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;

class DiagramToHtmlValidator
{
    // Path to the input Visio diagram
    private const string InputDiagramPath = "input.vsdx";

    // Path where the HTML output will be saved
    private const string OutputHtmlPath = "output.html";

    // Path to the HTML5 XSD schema file
    private const string Html5SchemaPath = "html5.xsd";

    static void Main()
    {
        try
        {

            // Load the Visio diagram using Aspose.Diagram
            Diagram diagram = new Diagram(InputDiagramPath);

            // Save the diagram as HTML
            diagram.Save(OutputHtmlPath, SaveFileFormat.Html);

            // Read the generated HTML content
            string htmlContent = File.ReadAllText(OutputHtmlPath);

            // Validate the HTML against the HTML5 schema
            bool isValid = ValidateHtml(htmlContent, Html5SchemaPath);

            Console.WriteLine(isValid
                ? "HTML validation succeeded."
                : "HTML validation failed. See errors above.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    /// <summary>
    /// Validates an HTML string against an XSD schema.
    /// </summary>
    /// <param name="html">The HTML markup to validate.</param>
    /// <param name="schemaPath">Path to the XSD schema file.</param>
    /// <returns>True if validation succeeds; otherwise false.</returns>
    private static bool ValidateHtml(string html, string schemaPath)
    {
        bool isValid = true;

        // Set up XML reader settings with schema validation
        XmlReaderSettings settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            DtdProcessing = DtdProcessing.Prohibit,
            XmlResolver = null
        };

        // Attach the HTML5 schema
        settings.Schemas.Add(null, schemaPath);

        // Capture validation errors
        settings.ValidationEventHandler += (sender, args) =>
        {
            isValid = false;
            Console.WriteLine($"Validation {args.Severity}: {args.Message}");
        };

        // Use a StringReader to feed the HTML content to the XmlReader
        using (StringReader stringReader = new StringReader(html))
        using (XmlReader reader = XmlReader.Create(stringReader, settings))
        {
            try
            {
                // Parse the entire document; validation occurs during reading
                while (reader.Read()) { }
            }
            catch (XmlException ex)
            {
                // Parsing errors (e.g., not well-formed XML) are treated as validation failures
                isValid = false;
                Console.WriteLine($"XML parsing error: {ex.Message}");
            }
        }

        return isValid;
    }
}
