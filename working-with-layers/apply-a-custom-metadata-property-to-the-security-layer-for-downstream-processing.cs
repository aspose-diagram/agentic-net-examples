using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Flag to indicate whether the 'Security' layer was found
            bool securityLayerFound = false;

            // Iterate through all pages and their layers to locate the 'Security' layer
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Security")
                    {
                        securityLayerFound = true;
                        // The layer exists – you can perform additional layer-specific actions here
                        // (e.g., change visibility, print info, etc.)
                    }
                }
            }

            if (!securityLayerFound)
            {
                throw new Exception("Layer named 'Security' was not found in the diagram.");
            }

            // Add a custom metadata property to the diagram that references the Security layer
            var customProp = new CustomProp
            {
                Name = "SecurityLayerMetadata",
                PropType = PropType.String,
                CustomValue = { ValueString = "Processed" }
            };
            diagram.DocumentProps.CustomProps.Add(customProp);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
