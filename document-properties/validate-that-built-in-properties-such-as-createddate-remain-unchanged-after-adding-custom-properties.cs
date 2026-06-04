using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source and the output diagram files
            string sourcePath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the original diagram
            Diagram diagram = new Diagram(sourcePath);

            // Capture the built‑in CreatedDate (TimeCreated) before any modifications
            DateTime createdBefore = diagram.DocumentProps.TimeCreated;

            // Create a new custom property
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProperty";
            customProp.PropType = PropType.String;
            customProp.CustomValue.ValueString = "SampleValue";

            // Add the custom property to the document
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the saved diagram to verify built‑in properties
            Diagram reloadedDiagram = new Diagram(outputPath);
            DateTime createdAfter = reloadedDiagram.DocumentProps.TimeCreated;

            // Validate that the built‑in CreatedDate has not changed
            if (createdBefore != createdAfter)
            {
                throw new Exception($"CreatedDate changed from {createdBefore} to {createdAfter}");
            }
            else
            {
                Console.WriteLine("Validation passed: CreatedDate remains unchanged after adding custom properties.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
