using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Create a custom property named "ReviewStatus" with value "Pending"
                CustomProp reviewProp = new CustomProp();
                reviewProp.Name = "ReviewStatus";
                reviewProp.PropType = PropType.String;
                reviewProp.CustomValue.ValueString = "Pending";

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(reviewProp);

                // Export the diagram to SVG while preserving all metadata
                string outputPath = "output.svg";
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine("Diagram exported to SVG with custom property 'ReviewStatus' set to 'Pending'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }