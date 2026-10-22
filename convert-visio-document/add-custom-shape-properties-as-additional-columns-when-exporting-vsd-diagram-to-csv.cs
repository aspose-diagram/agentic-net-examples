using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define the custom property name we want to add to every shape.
            const string customPropName = "MyCustomProp";
            const string customPropLabel = "My Custom Property";
            const string customPropValue = "DefaultValue";

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape already contains the custom property.
                    bool hasProp = false;
                    foreach (Prop existingProp in shape.Props)
                    {
                        if (existingProp.Name == customPropName)
                        {
                            hasProp = true;
                            break;
                        }
                    }

                    // Add the custom property if it does not exist.
                    if (!hasProp)
                    {
                        Prop prop = new Prop();
                        prop.Name = customPropName;                     // Property internal name.
                        prop.Label.Value = customPropLabel;             // Column header shown in CSV.
                        prop.Value.Val = customPropValue;               // Default value for the shape.
                        prop.Type.Value = TypePropValue.String;        // Property type.
                        shape.Props.Add(prop);
                    }
                }
            }

            // Export the diagram to CSV. Custom properties become additional columns.
            diagram.Save(outputPath, SaveFileFormat.Csv);
            Console.WriteLine($"Diagram exported successfully to CSV: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}