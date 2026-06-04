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
            // Create an empty diagram (original)
            Diagram originalDiagram = new Diagram();

            // Add a custom property to the original diagram
            CustomProp originalProp = new CustomProp();
            originalProp.Name = "MyCustomProp";
            originalProp.PropType = PropType.String;
            originalProp.CustomValue.ValueString = "OriginalValue";
            originalDiagram.DocumentProps.CustomProps.Add(originalProp);

            // Save original diagram to a temporary file
            string tempPath = Path.GetTempFileName();
            originalDiagram.Save(tempPath, SaveFileFormat.Vsdx);
            if (!File.Exists(tempPath))
            {
                Console.Error.WriteLine($"Failed to create temporary file: {tempPath}");
                return;
            }

            // Load the temporary file as a cloned diagram
            Diagram clonedDiagram = new Diagram(tempPath);

            // Update the custom property value in the cloned diagram
            if (clonedDiagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Cloned diagram does not contain the expected custom property.");

            CustomProp clonedProp = clonedDiagram.DocumentProps.CustomProps[0];
            clonedProp.CustomValue.ValueString = "UpdatedValue";

            // Validate that the original diagram's custom property remains unchanged
            if (originalDiagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Original diagram lost its custom property after cloning.");

            string originalValue = originalDiagram.DocumentProps.CustomProps[0].CustomValue.ValueString;
            string clonedValue = clonedDiagram.DocumentProps.CustomProps[0].CustomValue.ValueString;

            if (originalValue != "OriginalValue")
                throw new Exception($"Original custom property was modified. Expected 'OriginalValue', got '{originalValue}'.");

            if (clonedValue != "UpdatedValue")
                throw new Exception($"Cloned custom property was not updated correctly. Expected 'UpdatedValue', got '{clonedValue}'.");

            Console.WriteLine("Validation succeeded: original custom property unchanged, cloned property updated.");

            // Clean up temporary file
            try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}