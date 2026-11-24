using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the name of the custom property to check
            const string targetPropName = "MyFlag";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    bool hasTargetPropTrue = false;

                    // Check each custom property (Prop) of the shape
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == targetPropName &&
                            prop.Value != null &&
                            prop.Value.Val != null &&
                            prop.Value.Val.Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            hasTargetPropTrue = true;
                            break;
                        }
                    }

                    // If the shape has the target property set to true, add a field
                    if (hasTargetPropTrue)
                    {
                        // Create a new field (e.g., a custom text field)
                        Field field = new Field();

                        // Set the field type to Undefined (safe default)
                        field.Type.Value = TypeFieldValue.Undefined;

                        // Assign a value to the field (can be any string)
                        field.Value.Val = "AddedField";

                        // Add the field to the shape's Fields collection
                        shape.Fields.Add(field);
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}