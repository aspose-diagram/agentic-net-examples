using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the custom properties collection
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Try to find an existing custom property named "ReviewStatus"
            CustomProp reviewProp = null;
            foreach (CustomProp cp in customProps)
            {
                if (cp.Name == "ReviewStatus")
                {
                    reviewProp = cp;
                    break;
                }
            }

            if (reviewProp != null)
            {
                // Update the existing property's value to "Pending"
                reviewProp.CustomValue.ValueString = "Pending";
            }
            else
            {
                // Create a new custom property with the required settings
                CustomProp newProp = new CustomProp();
                newProp.Name = "ReviewStatus";
                newProp.PropType = PropType.String;
                newProp.CustomValue.ValueString = "Pending";

                // Add the new property to the document's custom properties collection
                customProps.Add(newProp);
            }

            // Prepare SVG save options (default options preserve metadata where possible)
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // No additional option is required for metadata preservation; defaults are used.

            // Save the diagram as SVG
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram saved to SVG with ReviewStatus set to Pending: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}