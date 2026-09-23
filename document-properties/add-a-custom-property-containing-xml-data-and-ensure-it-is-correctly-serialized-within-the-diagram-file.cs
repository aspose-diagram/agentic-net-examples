using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (must exist)
            string inputPath = "input.vsdx";
            // Output file with the custom XML property
            string outputPath = "output_with_customprop.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // XML data to store in the custom property
            string xmlData = "<root><item id=\"1\">Value</item></root>";

            // Create a new custom property
            CustomProp customProp = new CustomProp();
            customProp.Name = "XmlData";
            customProp.PropType = PropType.String;
            customProp.CustomValue.ValueString = xmlData;

            // Add the custom property to the document's custom properties collection
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the diagram, ensuring the custom property is serialized
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
