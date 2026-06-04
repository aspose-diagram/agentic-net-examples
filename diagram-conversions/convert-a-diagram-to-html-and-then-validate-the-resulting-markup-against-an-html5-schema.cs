using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Configure HTML save options to generate a single HTML file
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true,
                Title = "Converted Diagram"
            };

            // Destination HTML file
            string htmlPath = "output.html";

            // Convert each shape to HTML using the ToHTML method (single file mode)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.ToHTML(htmlPath, htmlOptions);
                }
            }

            // Validate the generated HTML against an HTML5 XSD schema
            string schemaPath = "html5.xsd"; // Path to the HTML5 schema file
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(string.Empty, schemaPath);

            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas
            };
            settings.ValidationEventHandler += ValidationCallback;

            using (FileStream fs = new FileStream(htmlPath, FileMode.Open, FileAccess.Read))
            using (XmlReader reader = XmlReader.Create(fs, settings))
            {
                while (reader.Read()) { }
            }

            Console.WriteLine("Diagram converted to HTML and validation completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Handles validation events
    static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        Console.WriteLine($"Validation {e.Severity}: {e.Message}");
    }
}
