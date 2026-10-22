using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty,
                                                                     Path.GetFileNameWithoutExtension(inputPath) + "_updated" + Path.GetExtension(inputPath));

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            bool statusUpdated = false;

            // ----- Update document-level custom property "Status" -----
            foreach (CustomProp docProp in diagram.DocumentProps.CustomProps)
            {
                if (string.Equals(docProp.Name, "Status", StringComparison.OrdinalIgnoreCase))
                {
                    // Ensure the property type is string
                    docProp.PropType = PropType.String;
                    // Set the new value
                    docProp.CustomValue.ValueString = "Completed";
                    statusUpdated = true;
                    Console.WriteLine("Updated document custom property 'Status' to 'Completed'.");
                    break;
                }
            }

            // ----- If not found at document level, search shape-level custom properties -----
            if (!statusUpdated)
            {
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (Prop shapeProp in shape.Props)
                        {
                            if (string.Equals(shapeProp.Name, "Status", StringComparison.OrdinalIgnoreCase))
                            {
                                // Set the property type to string
                                shapeProp.Type.Value = TypePropValue.String;
                                // Assign the new string value
                                shapeProp.Value.Val = "Completed";
                                statusUpdated = true;
                                Console.WriteLine($"Updated shape (ID={shape.ID}) custom property 'Status' to 'Completed'.");
                                // Continue searching in case multiple shapes have the property
                            }
                        }
                    }
                }
            }

            if (!statusUpdated)
            {
                Console.Error.WriteLine("Custom property 'Status' was not found in the document or any shape.");
                // Optionally, you could choose to add the property here instead of exiting.
                return;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}