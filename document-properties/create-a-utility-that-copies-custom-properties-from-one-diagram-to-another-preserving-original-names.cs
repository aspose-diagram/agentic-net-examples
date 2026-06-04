using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: source diagram path, target diagram path, output diagram path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCustomPropertyCopy <source.vsdx> <target.vsdx> [output.vsdx]");
                return;
            }

            string sourcePath = args[0];
            string targetPath = args[1];
            string outputPath = args.Length >= 3 ? args[2] : targetPath;

            try
            {
                // Load source and target diagrams
                Diagram sourceDiagram = new Diagram(sourcePath);
                Diagram targetDiagram = new Diagram(targetPath);

                // Access custom property collections
                var sourceCustomProps = sourceDiagram.DocumentProps.CustomProps;
                var targetCustomProps = targetDiagram.DocumentProps.CustomProps;

                // Iterate over each custom property in the source diagram
                for (int i = 0; i < sourceCustomProps.Count; i++)
                {
                    var srcProp = sourceCustomProps[i];

                    // Remove any existing property in the target with the same name to avoid duplicates
                    CustomProp existing = null;
                    for (int j = 0; j < targetCustomProps.Count; j++)
                    {
                        if (targetCustomProps[j].Name == srcProp.Name)
                        {
                            existing = targetCustomProps[j];
                            break;
                        }
                    }
                    if (existing != null)
                    {
                        targetCustomProps.Remove(existing);
                    }

                    // Create a new custom property and copy values
                    CustomProp newProp = new CustomProp
                    {
                        Name = srcProp.Name,
                        // Preserve the original property type
                        PropType = srcProp.PropType,
                        // Copy the stored value (as string)
                        CustomValue = { ValueString = srcProp.CustomValue.ValueString }
                    };

                    // Add the new property to the target diagram
                    targetCustomProps.Add(newProp);
                }

                // Save the modified target diagram
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Custom properties copied successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }