using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Page dimensions are stored in inches
                    double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Convert inches to millimeters (1 inch = 25.4 mm)
                    double widthMillimeters = widthInches * 25.4;
                    double heightMillimeters = heightInches * 25.4;

                    // Create custom property for page width in mm
                    CustomProp widthProp = new CustomProp();
                    widthProp.Name = "PageWidthMm";
                    widthProp.PropType = PropType.String;
                    widthProp.CustomValue.ValueString = widthMillimeters.ToString("F2");

                    // Create custom property for page height in mm
                    CustomProp heightProp = new CustomProp();
                    heightProp.Name = "PageHeightMm";
                    heightProp.PropType = PropType.String;
                    heightProp.CustomValue.ValueString = heightMillimeters.ToString("F2");

                    // Add the custom properties to the document's metadata
                    diagram.DocumentProps.CustomProps.Add(widthProp);
                    diagram.DocumentProps.CustomProps.Add(heightProp);

                    // Save the diagram back to a Visio file
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page dimensions converted to millimeters and stored in metadata.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }