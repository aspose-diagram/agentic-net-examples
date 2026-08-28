using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page, convert dimensions from inches to millimeters,
            // and store the results as custom document properties.
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page width and height in inches
                double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Convert to millimeters (1 inch = 25.4 mm)
                double widthMm = widthInches * 25.4;
                double heightMm = heightInches * 25.4;

                // Create and add a custom property for the page width in mm
                CustomProp widthProp = new CustomProp();
                widthProp.Name = $"Page_{pageIndex}_WidthMm";
                widthProp.PropType = PropType.String;
                widthProp.CustomValue.ValueString = widthMm.ToString("F2");
                diagram.DocumentProps.CustomProps.Add(widthProp);

                // Create and add a custom property for the page height in mm
                CustomProp heightProp = new CustomProp();
                heightProp.Name = $"Page_{pageIndex}_HeightMm";
                heightProp.PropType = PropType.String;
                heightProp.CustomValue.ValueString = heightMm.ToString("F2");
                diagram.DocumentProps.CustomProps.Add(heightProp);

                pageIndex++;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            diagram.Dispose();

            Console.WriteLine("Page dimensions converted to millimeters and stored as custom properties.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
