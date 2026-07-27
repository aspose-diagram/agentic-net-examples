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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a new custom property
            CustomProp projectIdProp = new CustomProp();
            projectIdProp.Name = "ProjectId";
            projectIdProp.PropType = PropType.Number;
            projectIdProp.CustomValue.ValueString = "12345";

            // Add the custom property to the diagram's custom properties collection
            diagram.DocumentProps.CustomProps.Add(projectIdProp);

            // Save the diagram with the new custom property
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
