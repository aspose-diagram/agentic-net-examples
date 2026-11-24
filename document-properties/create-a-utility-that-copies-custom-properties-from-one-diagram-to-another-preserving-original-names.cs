using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: source diagram, target diagram, optional output path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath> [outputDiagramPath]");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args.Length >= 3 ? args[2] : targetPath;

        // Guard against missing files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // ----- Remove existing custom properties from target -----
                    List<CustomProp> toRemove = new List<CustomProp>();
                    foreach (CustomProp prop in targetDiagram.DocumentProps.CustomProps)
                    {
                        toRemove.Add(prop);
                    }
                    foreach (CustomProp prop in toRemove)
                    {
                        targetDiagram.DocumentProps.CustomProps.Remove(prop);
                    }

                    // ----- Copy custom properties from source to target -----
                    foreach (CustomProp srcProp in sourceDiagram.DocumentProps.CustomProps)
                    {
                        // Create a new custom property
                        CustomProp destProp = new CustomProp();

                        // Preserve the original name
                        destProp.Name = srcProp.Name;

                        // Preserve the property type
                        destProp.PropType = srcProp.PropType;

                        // Preserve the property value (string representation)
                        if (srcProp.CustomValue != null && srcProp.CustomValue.ValueString != null)
                        {
                            destProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                        }

                        // Add the new property to the target diagram
                        targetDiagram.DocumentProps.CustomProps.Add(destProp);
                    }

                    // ----- Save the modified target diagram -----
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Custom properties copied successfully. Output saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}