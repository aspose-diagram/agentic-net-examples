using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // -------------------------------------------------
            // Your processing steps would go here.
            // -------------------------------------------------

            // Update the custom property "Status" to "Completed"
            bool statusFound = false;
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "Status")
                {
                    prop.CustomValue.ValueString = "Completed";
                    statusFound = true;
                    break;
                }
            }

            // If the property does not exist, add it
            if (!statusFound)
            {
                CustomProp statusProp = new CustomProp();
                statusProp.Name = "Status";
                statusProp.PropType = PropType.String;
                statusProp.CustomValue = new CustomValue();
                statusProp.CustomValue.ValueString = "Completed";

                diagram.DocumentProps.CustomProps.Add(statusProp);
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
