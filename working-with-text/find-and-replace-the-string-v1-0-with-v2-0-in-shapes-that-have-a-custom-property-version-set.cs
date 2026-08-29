using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a custom property named "Version"
                    bool hasVersionProp = shape.Props != null &&
                                          shape.Props.Any(p => p.Name != null && p.Name.Equals("Version", StringComparison.OrdinalIgnoreCase));

                    if (hasVersionProp)
                    {
                        // Replace occurrences of "v1.0" with "v2.0" in the shape's text
                        shape.ReplaceText("v1.0", "v2.0");
                        // Refresh shape data to update its layout after text change
                        shape.RefreshData();
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
