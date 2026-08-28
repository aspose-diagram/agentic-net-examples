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

                // Input Visio file (replace with actual path)
                string inputPath = "input.vsdx";

                // Output SVG file
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

                // Save the diagram as SVG with the custom property included
                diagram.Save(outputPath, svgOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }