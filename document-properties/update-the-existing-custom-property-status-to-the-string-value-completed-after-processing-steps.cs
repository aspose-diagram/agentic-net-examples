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

            // Load the existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Find the custom property named "Status"
            bool found = false;
            foreach (CustomProp prop in diagram.DocumentProps.CustomProps)
            {
                if (prop.Name == "Status")
                {
                    // Update its value to "Completed"
                    prop.CustomValue.ValueString = "Completed";
                    found = true;
                    break;
                }
            }

            // If the property does not exist, optionally add it
            if (!found)
            {
                var newProp = new CustomProp
                {
                    Name = "Status",
                    PropType = PropType.String,
                    CustomValue = { ValueString = "Completed" }
                };
                diagram.DocumentProps.CustomProps.Add(newProp);
            }

            // Save the diagram with the updated property
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
