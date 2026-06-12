using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to locate the watermark shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify the watermark shape by its name (adjust as needed)
                    if (shape.NameU == "Watermark")
                    {
                        // If the shape contains an image, set its transparency (0 = opaque, 1 = fully transparent)
                        if (shape.Image != null)
                        {
                            shape.Image.Transparency.Value = 0.5; // 50% transparency
                        }

                        // Additionally, set the fill background transparency to ensure consistent appearance
                        if (shape.Fill != null)
                        {
                            shape.Fill.FillBkgndTrans.Value = 0.5; // 50% transparency
                        }
                    }
                }
            }

            // Save the modified diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
