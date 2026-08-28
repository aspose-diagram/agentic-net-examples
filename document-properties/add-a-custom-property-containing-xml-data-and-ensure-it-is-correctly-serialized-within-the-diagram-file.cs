using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram
            Diagram diagram = new Diagram();

            // Create a custom property to hold XML data
            CustomProp xmlProp = new CustomProp
            {
                Name = "MyXmlData",               // Property name
                PropType = PropType.String,       // Data type of the property
                // Store the XML string in the custom value field
                CustomValue = { ValueString = "<root><item>Value</item></root>" }
            };

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(xmlProp);

            // Save the diagram to a VSDX file; the custom property will be serialized with the file
            string outputPath = "DiagramWithXmlProp.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}' with custom XML property.");
        }
    }