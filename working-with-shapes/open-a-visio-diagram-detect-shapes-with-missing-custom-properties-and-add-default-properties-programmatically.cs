using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define required custom properties and their default values.
        var requiredProperties = new Dictionary<string, string>
        {
            { "Author", "Unknown" },
            { "ReviewDate", DateTime.Today.ToShortDateString() },
            { "Status", "Draft" }
        };

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip group shapes (they may not have a Props collection).
                        if (shape.Type == TypeValue.Group)
                            continue;

                        // Ensure the shape is not marked for deletion.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check each required property.
                        foreach (KeyValuePair<string, string> kvp in requiredProperties)
                        {
                            string propName = kvp.Key;
                            string defaultValue = kvp.Value;

                            bool hasProperty = false;
                            foreach (Prop existingProp in shape.Props)
                            {
                                if (existingProp.Name == propName)
                                {
                                    hasProperty = true;
                                    break;
                                }
                            }

                            // Add missing property with default values.
                            if (!hasProperty)
                            {
                                Prop newProp = new Prop();
                                newProp.Name = propName;
                                newProp.Label.Value = propName;               // Use the property name as label.
                                newProp.Value.Val = defaultValue;             // Set the default value.
                                newProp.Type.Value = TypePropValue.String;    // Assume string type.
                                shape.Props.Add(newProp);
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}