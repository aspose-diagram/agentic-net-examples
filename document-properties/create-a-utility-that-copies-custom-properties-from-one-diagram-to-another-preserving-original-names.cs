using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: source diagram path, target diagram path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramCustomPropertyCopy <source.vsdx> <target.vsdx> <output.vsdx>");
                return;
            }

            string sourcePath = args[0];
            string targetPath = args[1];
            string outputPath = args[2];

            // Load source diagram
            Diagram sourceDiagram = new Diagram(sourcePath);
            // Load target diagram
            Diagram targetDiagram = new Diagram(targetPath);

            // Access custom properties collections
            CustomPropCollection sourceProps = sourceDiagram.DocumentProps.CustomProps;
            CustomPropCollection targetProps = targetDiagram.DocumentProps.CustomProps;

            // Iterate over each custom property in the source diagram
            foreach (CustomProp srcProp in sourceProps)
            {
                // Try to find a property with the same name in the target diagram
                CustomProp existingProp = null;
                foreach (CustomProp tp in targetProps)
                {
                    if (tp.Name == srcProp.Name)
                    {
                        existingProp = tp;
                        break;
                    }
                }

                if (existingProp != null)
                {
                    // Property exists – update its value while preserving the name and type
                    existingProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                }
                else
                {
                    // Property does not exist – create a new one with the same name, type, and value
                    CustomProp newProp = new CustomProp();
                    newProp.Name = srcProp.Name;
                    newProp.PropType = srcProp.PropType; // PropType enum (String, Number, Date, etc.)
                    newProp.CustomValue.ValueString = srcProp.CustomValue.ValueString;
                    targetProps.Add(newProp);
                }
            }

            // Save the modified target diagram to the specified output path
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Custom properties copied from '{sourcePath}' to '{outputPath}'.");
        }
    }