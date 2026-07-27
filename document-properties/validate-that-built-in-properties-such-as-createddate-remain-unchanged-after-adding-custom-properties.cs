using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Capture the built‑in creation date (TimeCreated) before any changes
            DateTime createdBefore = diagram.DocumentProps.TimeCreated;

            // Create and add a custom document property
            CustomProp customProp = new CustomProp();
            customProp.Name = "MyCustomProp";
            customProp.PropType = PropType.String;
            customProp.CustomValue.ValueString = "CustomValue";
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Reload the saved diagram to verify persisted values
            Diagram reloadedDiagram = new Diagram(outputPath);
            DateTime createdAfter = reloadedDiagram.DocumentProps.TimeCreated;

            // Validate that the built‑in creation date has not changed
            if (createdBefore != createdAfter)
            {
                throw new Exception($"Created date changed from {createdBefore} to {createdAfter}");
            }
            else
            {
                Console.WriteLine("Built‑in CreatedDate remains unchanged after adding custom properties.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
