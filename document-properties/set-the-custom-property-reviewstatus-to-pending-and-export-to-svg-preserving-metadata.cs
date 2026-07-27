using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the exported SVG file
                string outputPath = "output.svg";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create a custom property named "ReviewStatus" with value "Pending"
                CustomProp reviewProp = new CustomProp();
                reviewProp.Name = "ReviewStatus";
                reviewProp.PropType = PropType.String;
                reviewProp.CustomValue.ValueString = "Pending";

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(reviewProp);

                // Prepare SVG save options (default options preserve metadata)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the diagram to SVG
                diagram.Save(outputPath, svgOptions);

                Console.WriteLine("Diagram exported to SVG with ReviewStatus set to Pending.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }