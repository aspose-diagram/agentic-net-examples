using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (can be passed as first argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the original diagram inside a using block (mandatory pattern)
            using (Diagram originalDiagram = new Diagram(inputPath))
            {
                // Ensure there is at least one custom property to work with
                var originalCustomProps = originalDiagram.DocumentProps.CustomProps;
                if (originalCustomProps.Count == 0)
                {
                    // Create a new custom property
                    CustomProp newProp = new CustomProp();
                    newProp.Name = "TestProp";
                    newProp.PropType = PropType.String;
                    newProp.CustomValue.ValueString = "OriginalValue";
                    originalCustomProps.Add(newProp);
                }

                // Capture the original value of the first custom property
                CustomProp originalProp = originalCustomProps[0];
                string originalValueBeforeClone = originalProp.CustomValue.ValueString;

                // ----- Clone the diagram -----
                // Create an empty diagram and combine the original into it.
                using (Diagram clonedDiagram = new Diagram())
                {
                    // Combine copies pages and masters from the original diagram
                    clonedDiagram.Combine(originalDiagram);

                    // After combine, ensure the cloned diagram has the same custom property
                    var clonedCustomProps = clonedDiagram.DocumentProps.CustomProps;
                    CustomProp clonedProp = null;
                    for (int i = 0; i < clonedCustomProps.Count; i++)
                    {
                        if (clonedCustomProps[i].Name == originalProp.Name)
                        {
                            clonedProp = clonedCustomProps[i];
                            break;
                        }
                    }

                    // If the property does not exist in the clone, add it with the original value
                    if (clonedProp == null)
                    {
                        CustomProp copyProp = new CustomProp();
                        copyProp.Name = originalProp.Name;
                        copyProp.PropType = originalProp.PropType;
                        copyProp.CustomValue.ValueString = originalProp.CustomValue.ValueString;
                        clonedCustomProps.Add(copyProp);
                        clonedProp = copyProp;
                    }

                    // Update the custom property in the cloned diagram
                    clonedProp.CustomValue.ValueString = "UpdatedValue";

                    // ----- Validation -----
                    // Verify that the original diagram's property remains unchanged
                    string originalValueAfterClone = originalDiagram.DocumentProps.CustomProps[0].CustomValue.ValueString;
                    if (originalValueAfterClone != originalValueBeforeClone)
                    {
                        throw new Exception("Validation failed: Original diagram's custom property was modified after cloning.");
                    }

                    // Verify that the cloned diagram reflects the updated value
                    string clonedValue = clonedDiagram.DocumentProps.CustomProps[0].CustomValue.ValueString;
                    if (clonedValue != "UpdatedValue")
                    {
                        throw new Exception("Validation failed: Cloned diagram's custom property does not reflect the updated value.");
                    }

                    // ----- Save both diagrams for manual inspection -----
                    string originalOutput = "original_output.vsdx";
                    string clonedOutput = "cloned_output.vsdx";

                    originalDiagram.Save(originalOutput, SaveFileFormat.Vsdx);
                    clonedDiagram.Save(clonedOutput, SaveFileFormat.Vsdx);

                    Console.WriteLine("Validation succeeded: Original diagram unchanged, clone updated.");
                    Console.WriteLine($"Original diagram saved to: {originalOutput}");
                    Console.WriteLine($"Cloned diagram saved to: {clonedOutput}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}