using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {
            // Create a new empty diagram
            Diagram originalDiagram = new Diagram();

            // Add a custom property to the original diagram
            CustomProp originalProp = new CustomProp();
            originalProp.Name = "SampleProp";
            originalProp.PropType = PropType.String;
            originalProp.CustomValue.ValueString = "OriginalValue";
            originalDiagram.DocumentProps.CustomProps.Add(originalProp);

            // Clone the diagram by saving to a memory stream and loading back
            Diagram clonedDiagram;
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the original diagram into the stream in VSDX format
                originalDiagram.Save(ms, SaveFileFormat.Vsdx);
                ms.Position = 0; // Reset stream position for reading
                // Load a new diagram instance from the stream (acts as a clone)
                clonedDiagram = new Diagram(ms);
            }

            // Update the custom property in the cloned diagram
            CustomProp clonedProp = null;
            foreach (CustomProp cp in clonedDiagram.DocumentProps.CustomProps)
            {
                if (cp.Name == "SampleProp")
                {
                    clonedProp = cp;
                    break;
                }
            }

            if (clonedProp == null)
                throw new Exception("Cloned diagram does not contain the expected custom property.");

            // Change the value only in the cloned diagram
            clonedProp.CustomValue.ValueString = "UpdatedValue";

            // Validate that the original diagram's custom property remains unchanged
            string originalValue = originalDiagram.DocumentProps.CustomProps[0].CustomValue.ValueString;
            if (originalValue != "OriginalValue")
                throw new Exception($"Original diagram property was altered. Expected 'OriginalValue', got '{originalValue}'.");

            // Validate that the cloned diagram reflects the update
            string clonedValue = clonedDiagram.DocumentProps.CustomProps[0].CustomValue.ValueString;
            if (clonedValue != "UpdatedValue")
                throw new Exception($"Cloned diagram property was not updated. Expected 'UpdatedValue', got '{clonedValue}'.");

            Console.WriteLine("Validation successful: original property unchanged, cloned property updated.");

            // Optional: save both diagrams to verify manually
            originalDiagram.Save("OriginalDiagram.vsdx", SaveFileFormat.Vsdx);
            clonedDiagram.Save("ClonedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}