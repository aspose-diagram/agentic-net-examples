using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Capture the built‑in creation date before any modifications
            DateTime createdBefore = diagram.DocumentProps.TimeCreated;

            // Create a new custom property
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProp";
            customProp.PropType = PropType.String;
            customProp.CustomValue.ValueString = "TestValue";

            // Add the custom property to the document
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Optionally save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Capture the creation date after adding the custom property
            DateTime createdAfter = diagram.DocumentProps.TimeCreated;

            // Validate that the built‑in creation date has not changed
            if (createdBefore != createdAfter)
            {
                throw new Exception("The document's creation date changed after adding a custom property.");
            }
            else
            {
                Console.WriteLine("Validation successful: CreatedDate remains unchanged.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
