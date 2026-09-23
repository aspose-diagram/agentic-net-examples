using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source diagram (with custom properties) and the target diagram.
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";
                string outputPath = "target_with_copied_props.vsdx";

                // Load the diagrams.
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // Copy custom properties from source to target.
                CopyCustomProperties(sourceDiagram, targetDiagram);

                // Save the updated target diagram.
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Custom properties copied successfully to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Copies all custom document properties from the source diagram to the target diagram.
        /// Existing properties with the same name are updated; missing ones are added.
        /// </summary>
        /// <param name="source">Diagram containing the original custom properties.</param>
        /// <param name="target">Diagram that will receive the copied properties.</param>
        private static void CopyCustomProperties(Diagram source, Diagram target)
        {
            // Iterate over each custom property in the source diagram.
            foreach (CustomProp srcProp in source.DocumentProps.CustomProps)
            {
                // Try to find a property with the same name in the target diagram.
                CustomProp existingProp = null;
                foreach (CustomProp tgtProp in target.DocumentProps.CustomProps)
                {
                    if (tgtProp.Name == srcProp.Name)
                    {
                        existingProp = tgtProp;
                        break;
                    }
                }

                if (existingProp != null)
                {
                    // Update the value of the existing property.
                    existingProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                    // Preserve the original property type.
                    existingProp.PropType = srcProp.PropType;
                }
                else
                {
                    // Create a new custom property and copy all relevant fields.
                    CustomProp newProp = new CustomProp
                    {
                        Name = srcProp.Name,
                        PropType = srcProp.PropType,
                        CustomValue = { ValueString = srcProp.CustomValue.ValueString }
                    };

                    // Add the new property to the target diagram.
                    target.DocumentProps.CustomProps.Add(newProp);
                }
            }
        }
    }