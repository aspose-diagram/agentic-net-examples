using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Set HTML save options (optional customizations)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Save the whole diagram as a single HTML file
                SaveAsSingleFile = true,
                // Include toolbar in the generated HTML (default is true)
                SaveToolBar = true
            };

            // Convert the diagram to HTML and save it to disk
            diagram.Save("output.html", htmlOptions);

            // Path to the HTML5 schema (XSD) used for validation
            string htmlSchemaPath = "html5.xsd";

            // Configure XML reader settings for schema validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema
            };
            settings.Schemas.Add(null, htmlSchemaPath);
            settings.ValidationEventHandler += ValidationEventHandler;

            // Validate the generated HTML (assumed to be well‑formed XHTML)
            using (XmlReader reader = XmlReader.Create("output.html", settings))
            {
                while (reader.Read()) { /* reading triggers validation */ }
            }

            Console.WriteLine("HTML validation completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Handles validation warnings and errors
    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        Console.WriteLine($"{e.Severity}: {e.Message}");
    }
}
