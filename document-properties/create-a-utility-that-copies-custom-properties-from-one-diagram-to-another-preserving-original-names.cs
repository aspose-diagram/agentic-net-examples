using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: CopyCustomProps <source.vsdx> <target.vsdx> <output.vsdx>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

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
            // Load source and target diagrams
            Diagram sourceDiagram = new Diagram(sourcePath);
            Diagram targetDiagram = new Diagram(targetPath);

            // Iterate over each custom property in the source diagram
            foreach (CustomProp srcProp in sourceDiagram.DocumentProps.CustomProps)
            {
                // Search for a property with the same name in the target diagram
                CustomProp targetProp = null;
                foreach (CustomProp tp in targetDiagram.DocumentProps.CustomProps)
                {
                    if (tp.Name == srcProp.Name)
                    {
                        targetProp = tp;
                        break;
                    }
                }

                if (targetProp != null)
                {
                    // Property exists – update its type and value
                    targetProp.PropType = srcProp.PropType;
                    targetProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                }
                else
                {
                    // Property does not exist – create a new one preserving name, type, and value
                    CustomProp newProp = new CustomProp();
                    newProp.Name = srcProp.Name;
                    newProp.PropType = srcProp.PropType;
                    newProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;

                    // Add the new custom property to the target diagram
                    targetDiagram.DocumentProps.CustomProps.Add(newProp);
                }
            }

            // Save the modified target diagram to the specified output path
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Custom properties copied successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}