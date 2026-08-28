using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Initialize a new empty diagram
            Diagram diagram = new Diagram();

            // Add a custom document property to the original diagram
            CustomProp originalProp = new CustomProp();
            originalProp.Name = "MyCustomProp";
            originalProp.PropType = PropType.String;
            originalProp.CustomValue = new CustomValue();
            originalProp.CustomValue.ValueString = "OriginalValue";
            diagram.DocumentProps.CustomProps.Add(originalProp);

            // Clone the diagram by saving to a memory stream and loading a new instance
            Diagram clonedDiagram;
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the original diagram into the stream in VSDX format
                diagram.Save(ms, SaveFileFormat.Vsdx);
                ms.Position = 0; // Reset stream position for reading

                // Load a new diagram from the stream (deep copy)
                clonedDiagram = new Diagram(ms);
            }

            // Update the custom property in the cloned diagram
            if (clonedDiagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Cloned diagram does not contain the custom property.");

            CustomProp clonedProp = clonedDiagram.DocumentProps.CustomProps[0];
            clonedProp.CustomValue.ValueString = "UpdatedValue";

            // Verify the original diagram's custom property remains unchanged
            if (diagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Original diagram does not contain the custom property.");

            CustomProp checkOriginalProp = diagram.DocumentProps.CustomProps[0];
            if (checkOriginalProp.CustomValue.ValueString != "OriginalValue")
                throw new Exception("Original diagram's custom property was altered after cloning.");

            // Verify the cloned diagram reflects the update
            if (clonedProp.CustomValue.ValueString != "UpdatedValue")
                throw new Exception("Cloned diagram's custom property was not updated correctly.");

            Console.WriteLine("Validation successful: original custom property unchanged, clone updated.");

            // Save both diagrams to files for persistence (optional)
            diagram.Save("original.vsdx", SaveFileFormat.Vsdx);
            clonedDiagram.Save("clone.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}