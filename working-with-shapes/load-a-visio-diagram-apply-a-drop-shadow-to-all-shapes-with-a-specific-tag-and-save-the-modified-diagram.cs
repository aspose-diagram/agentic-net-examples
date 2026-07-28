using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // The name of the custom property (tag) to look for
            string targetTagName = "MyTag";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a custom property with the target tag name
                    bool hasTag = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == targetTagName)
                        {
                            hasTag = true;
                            break;
                        }
                    }

                    if (hasTag)
                    {
                        // Apply a simple drop shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable shadow
                        shape.Fill.ShdwForegnd.Value = "#000000";                    // Shadow color (black)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
