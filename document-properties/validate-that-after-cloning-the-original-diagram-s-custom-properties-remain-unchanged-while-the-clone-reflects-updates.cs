using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram originalDiagram = new Diagram();

            // Add a custom property to the original diagram
            CustomProp originalProp = new CustomProp();
            originalProp.Name = "MyProp";
            originalProp.PropType = PropType.String;
            originalProp.CustomValue.ValueString = "OriginalValue";
            originalDiagram.DocumentProps.CustomProps.Add(originalProp);

            // Clone the diagram by saving to a temporary file and loading it back
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");
            originalDiagram.Save(tempPath, SaveFileFormat.Vsdx);
            if (!File.Exists(tempPath))
            {
                Console.Error.WriteLine($"Failed to create temporary file for cloning: {tempPath}");
                return;
            }
            Diagram clonedDiagram = new Diagram(tempPath);

            // Update the custom property in the cloned diagram
            if (clonedDiagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Cloned diagram does not contain the custom property.");

            CustomProp clonedProp = clonedDiagram.DocumentProps.CustomProps[0];
            clonedProp.CustomValue.ValueString = "UpdatedValue";

            // Validate that the original diagram's custom property remains unchanged
            if (originalDiagram.DocumentProps.CustomProps.Count == 0)
                throw new Exception("Original diagram lost the custom property after cloning.");

            CustomProp checkOriginalProp = originalDiagram.DocumentProps.CustomProps[0];
            if (checkOriginalProp.CustomValue.ValueString != "OriginalValue")
                throw new Exception("Original diagram's custom property was modified after cloning.");

            // Validate that the cloned diagram reflects the update
            if (clonedProp.CustomValue.ValueString != "UpdatedValue")
                throw new Exception("Cloned diagram's custom property does not reflect the updated value.");

            // Output success messages
            Console.WriteLine("Validation successful:");
            Console.WriteLine($"Original property value: {checkOriginalProp.CustomValue.ValueString}");
            Console.WriteLine($"Cloned property value: {clonedProp.CustomValue.ValueString}");

            // Save both diagrams to files for visual inspection (optional)
            originalDiagram.Save("original.vsdx", SaveFileFormat.Vsdx);
            clonedDiagram.Save("clone.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}