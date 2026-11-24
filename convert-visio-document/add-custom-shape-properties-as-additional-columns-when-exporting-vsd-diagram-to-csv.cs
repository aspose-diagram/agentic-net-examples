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
            Console.Error.WriteLine("Usage: Program <input.vsd> <output.csv>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a new custom property (Prop) to be exported as an extra CSV column.
                        Prop customProp = new Prop();

                        // Set the internal name of the property.
                        customProp.Name = "ExportColumn";

                        // Set the label that will appear as the CSV column header.
                        customProp.Label.Value = "Export Column";

                        // Set the value for this shape. Here we use the shape ID to generate a unique value.
                        customProp.Value.Val = $"Value_{shape.ID}";

                        // Define the property type as a string.
                        customProp.Type.Value = TypePropValue.String;

                        // Add the custom property to the shape's property collection.
                        shape.Props.Add(customProp);
                    }
                }

                // Export the diagram to CSV. The custom properties become additional columns.
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Diagram exported successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}