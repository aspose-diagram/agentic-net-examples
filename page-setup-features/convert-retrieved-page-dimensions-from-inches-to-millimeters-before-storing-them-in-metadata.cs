using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first page.
                Page page = diagram.Pages[0];

                // Get page dimensions in inches.
                double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Convert inches to millimeters (1 inch = 25.4 mm).
                double widthMillimeters = widthInches * 25.4;
                double heightMillimeters = heightInches * 25.4;

                // Create a custom property for page width in millimeters.
                CustomProp widthProp = new CustomProp
                {
                    Name = "PageWidthMm",
                    PropType = PropType.Number,
                    CustomValue = { ValueString = widthMillimeters.ToString() }
                };

                // Create a custom property for page height in millimeters.
                CustomProp heightProp = new CustomProp
                {
                    Name = "PageHeightMm",
                    PropType = PropType.Number,
                    CustomValue = { ValueString = heightMillimeters.ToString() }
                };

                // Access the custom property collection.
                var customProps = diagram.DocumentProps.CustomProps;

                // Remove existing properties with the same names, if any.
                for (int i = customProps.Count - 1; i >= 0; i--)
                {
                    if (customProps[i].Name == widthProp.Name || customProps[i].Name == heightProp.Name)
                    {
                        customProps.Remove(customProps[i]);
                    }
                }

                // Add the new custom properties to the diagram.
                customProps.Add(widthProp);
                customProps.Add(heightProp);

                // Save the updated diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
            return;
        }
    }
}