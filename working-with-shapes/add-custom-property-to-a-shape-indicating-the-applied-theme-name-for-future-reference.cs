using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Get the first shape on the first page
            Shape shape = page.Shapes[0];

            // Define the theme name to apply (example value)
            string themeName = "MyCustomTheme";

            // Apply a preset theme to the shape (write‑only property)
            // Note: PresetTheme is write‑only; we cannot read it back.
            // The actual enum/value depends on the Visio theme you want.
            // Here we simply demonstrate setting a theme identifier.
            // shape.PresetTheme = PresetThemeValue.Theme1; // Uncomment and set appropriate enum if needed

            // Add a custom property to store the applied theme name
            Prop customProp = new Prop();
            customProp.Name = "AppliedTheme";               // Property name
            customProp.Value.Val = themeName;               // Property value (the theme name)
            customProp.Label.Value = "Applied Theme";       // Human‑readable label
            customProp.Type.Value = TypePropValue.String;  // Data type of the property

            // Add the custom property to the shape's property collection
            shape.Props.Add(customProp);

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