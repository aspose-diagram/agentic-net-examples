using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input arguments: diagram file, HTML output file, HTML5 XSD schema file
            string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
            string htmlPath = args.Length > 1 ? args[1] : "output.html";
            string schemaPath = args.Length > 2 ? args[2] : "html5.xsd";

            // Verify that the diagram file exists
            if (!File.Exists(diagramPath))
            {
                Console.WriteLine($"Diagram file not found: {diagramPath}");
                return;
            }

            // Verify that the schema file exists
            if (!File.Exists(schemaPath))
            {
                Console.WriteLine($"HTML5 schema file not found: {schemaPath}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Configure HTML save options (default PNG images)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Save diagram as HTML
            diagram.Save(htmlPath, htmlOptions);
            Console.WriteLine($"Diagram saved as HTML to: {htmlPath}");

            // Validate the generated HTML against the provided HTML5 XSD schema
            ValidateHtmlAgainstSchema(htmlPath, schemaPath);
        }

        private static void ValidateHtmlAgainstSchema(string htmlFilePath, string xsdFilePath)
        {
            // Prepare the schema set
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdFilePath);

            // Set up XML reader settings for validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas,
                DtdProcessing = DtdProcessing.Prohibit
            };
            settings.ValidationEventHandler += ValidationEventHandler;

            // Perform validation
            using (FileStream fs = new FileStream(htmlFilePath, FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(fs, settings))
            {
                try
                {
                    while (reader.Read()) { /* reading triggers validation */ }
                    Console.WriteLine("HTML validation completed successfully. No errors found.");
                }
                catch (XmlException ex)
                {
                    Console.WriteLine($"XML parsing error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error during validation: {ex.Message}");
                }
            }
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            // Treat warnings as errors for strict validation
            string severity = e.Severity == XmlSeverityType.Error ? "Error" : "Warning";
            Console.WriteLine($"{severity}: {e.Message}");
            if (e.Severity == XmlSeverityType.Error)
            {
                // Throw to stop further processing if a validation error occurs
                throw new Exception($"HTML validation failed: {e.Message}");
            }
        }
    }