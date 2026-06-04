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

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new custom property
            CustomProp projectIdProp = new CustomProp();
            projectIdProp.Name = "ProjectId";
            // Set the property type to numeric
            projectIdProp.PropType = PropType.Number;
            // Assign the numeric value as a string
            projectIdProp.CustomValue.ValueString = "12345";

            // Add the custom property to the diagram's custom properties collection
            diagram.DocumentProps.CustomProps.Add(projectIdProp);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
