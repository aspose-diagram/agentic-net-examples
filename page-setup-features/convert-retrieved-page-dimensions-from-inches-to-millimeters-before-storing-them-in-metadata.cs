using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output_with_mm.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (values are in inches)
            double widthInches = page.PageSheet.PageProps.PageWidth.Value;
            double heightInches = page.PageSheet.PageProps.PageHeight.Value;

            // Convert inches to millimeters (1 inch = 25.4 mm)
            const double mmPerInch = 25.4;
            double widthMillimeters = widthInches * mmPerInch;
            double heightMillimeters = heightInches * mmPerInch;

            // -----------------------------------------------------------------
            // Store the converted dimensions in custom document properties
            // -----------------------------------------------------------------

            // Remove any existing custom properties with the same names
            var propsToRemove = new List<CustomProp>();
            foreach (CustomProp existingProp in diagram.DocumentProps.CustomProps)
            {
                if (existingProp.Name == "PageWidthMm" || existingProp.Name == "PageHeightMm")
                {
                    propsToRemove.Add(existingProp);
                }
            }
            foreach (CustomProp prop in propsToRemove)
            {
                diagram.DocumentProps.CustomProps.Remove(prop);
            }

            // Create and add a custom property for page width in millimeters
            CustomProp widthProp = new CustomProp();
            widthProp.Name = "PageWidthMm";
            widthProp.PropType = PropType.String;
            widthProp.CustomValue = new CustomValue(); // Initialize CustomValue object
            widthProp.CustomValue.ValueString = widthMillimeters.ToString("F2");
            diagram.DocumentProps.CustomProps.Add(widthProp);

            // Create and add a custom property for page height in millimeters
            CustomProp heightProp = new CustomProp();
            heightProp.Name = "PageHeightMm";
            heightProp.PropType = PropType.String;
            heightProp.CustomValue = new CustomValue(); // Initialize CustomValue object
            heightProp.CustomValue.ValueString = heightMillimeters.ToString("F2");
            diagram.DocumentProps.CustomProps.Add(heightProp);

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with millimeter dimensions metadata to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
