using System.IO;
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
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions in inches
                    double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Convert inches to millimeters (1 inch = 25.4 mm)
                    double widthMillimeters = widthInches * 25.4;
                    double heightMillimeters = heightInches * 25.4;

                    // Store width in custom document properties
                    CustomProp widthProp = new CustomProp();
                    widthProp.Name = $"Page{pageIndex}_WidthMm";
                    widthProp.PropType = PropType.String;
                    widthProp.CustomValue.ValueString = widthMillimeters.ToString("F2");
                    diagram.DocumentProps.CustomProps.Add(widthProp);

                    // Store height in custom document properties
                    CustomProp heightProp = new CustomProp();
                    heightProp.Name = $"Page{pageIndex}_HeightMm";
                    heightProp.PropType = PropType.String;
                    heightProp.CustomValue.ValueString = heightMillimeters.ToString("F2");
                    diagram.DocumentProps.CustomProps.Add(heightProp);

                    pageIndex++;
                }

                // Save the updated diagram back to Visio format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
