using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define required custom property names and their default values
                var requiredProperties = new (string Name, string Label, string DefaultValue)[]
                {
                    ("Prop1", "Property 1", "Default1"),
                    ("Prop2", "Property 2", "Default2")
                };

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip group shapes without custom properties (optional)
                        // Process each required property
                        foreach (var reqProp in requiredProperties)
                        {
                            bool hasProp = false;
                            // Check if the shape already contains the property
                            foreach (Prop existingProp in shape.Props)
                            {
                                if (existingProp.Name == reqProp.Name)
                                {
                                    hasProp = true;
                                    break;
                                }
                            }

                            // If the property is missing, add it with default values
                            if (!hasProp)
                            {
                                Prop newProp = new Prop();
                                newProp.Name = reqProp.Name;
                                newProp.Label.Value = reqProp.Label;
                                newProp.Value.Val = reqProp.DefaultValue;
                                // Set the property type to string
                                newProp.Type.Value = TypePropValue.String;
                                shape.Props.Add(newProp);
                            }
                        }
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}