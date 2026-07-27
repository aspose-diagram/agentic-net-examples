using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source diagram, the target diagram, and the output file.
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";
                string outputPath = "merged.vsdx";

                // Load the source and target diagrams.
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // Copy custom properties from source to target.
                foreach (CustomProp srcProp in sourceDiagram.DocumentProps.CustomProps)
                {
                    // Check if a property with the same name already exists in the target.
                    CustomProp existingProp = null;
                    foreach (CustomProp tgtProp in targetDiagram.DocumentProps.CustomProps)
                    {
                        if (tgtProp.Name == srcProp.Name)
                        {
                            existingProp = tgtProp;
                            break;
                        }
                    }

                    // If it exists, remove it to avoid duplicates.
                    if (existingProp != null)
                    {
                        targetDiagram.DocumentProps.CustomProps.Remove(existingProp);
                    }

                    // Create a new custom property and copy its details.
                    CustomProp newProp = new CustomProp
                    {
                        Name = srcProp.Name,
                        PropType = srcProp.PropType,
                        // Preserve the original value string.
                        CustomValue = { ValueString = srcProp.CustomValue.ValueString }
                    };

                    // Add the new property to the target diagram.
                    targetDiagram.DocumentProps.CustomProps.Add(newProp);
                }

                // Save the updated target diagram.
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }