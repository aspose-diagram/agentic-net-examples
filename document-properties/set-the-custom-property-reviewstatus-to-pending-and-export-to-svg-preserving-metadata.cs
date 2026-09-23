using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties; // Required for custom property types

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.svg";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom properties
            var customProps = diagram.DocumentProps.CustomProps;
            CustomProp reviewProp = null;

            // Search for an existing "ReviewStatus" property
            foreach (CustomProp prop in customProps)
            {
                if (prop.Name == "ReviewStatus")
                {
                    reviewProp = prop;
                    break;
                }
            }

            if (reviewProp != null)
            {
                // Update the value of the existing property
                reviewProp.CustomValue.ValueString = "Pending";
            }
            else
            {
                // Create and add a new custom property with the desired value
                CustomProp newProp = new CustomProp
                {
                    Name = "ReviewStatus",
                    PropType = PropType.String,
                    CustomValue = new CustomValue { ValueString = "Pending" }
                };
                customProps.Add(newProp);
            }

            // Configure SVG export options (export first page)
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                PageIndex = 0 // Export only the first page
                // PageCount property is not supported in this version; omitted
            };

            // Save the diagram as SVG, preserving custom metadata
            diagram.Save(outputPath, svgOptions);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}