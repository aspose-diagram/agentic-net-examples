using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provided via command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Determine whether the shape has the custom property "IsTarget" set to "True"
                    bool hasTargetProperty = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "IsTarget" &&
                            string.Equals(prop.Value.Val, "True", StringComparison.OrdinalIgnoreCase))
                        {
                            hasTargetProperty = true;
                            break;
                        }
                    }

                    // If the condition is met, add a new text field to the shape
                    if (hasTargetProperty)
                    {
                        // Create a new Field object
                        Field field = new Field();

                        // Set the field's displayed value (static text "Special")
                        field.Value.Val = "Special";

                        // Add the field to the shape's Fields collection
                        shape.Fields.Add(field);
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}