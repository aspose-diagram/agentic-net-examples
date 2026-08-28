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

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Capture the built‑in CreatedDate (TimeCreated) before adding custom properties
            DateTime createdBefore = diagram.DocumentProps.TimeCreated;

            // Create a custom property
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProp";
            customProp.PropType = PropType.String;
            customProp.CustomValue.ValueString = "CustomValue";

            // Add the custom property to the document
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the saved diagram to verify the built‑in property
            Diagram reloadedDiagram = new Diagram(outputPath);
            DateTime createdAfter = reloadedDiagram.DocumentProps.TimeCreated;

            // Validate that the built‑in CreatedDate has not changed
            if (createdBefore != createdAfter)
            {
                throw new Exception($"CreatedDate changed from {createdBefore} to {createdAfter}");
            }
            else
            {
                Console.WriteLine("Built-in CreatedDate unchanged after adding custom property.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
