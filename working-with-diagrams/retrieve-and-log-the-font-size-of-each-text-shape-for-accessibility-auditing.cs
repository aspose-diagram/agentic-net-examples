using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be audited
            string inputPath = "sample.vsdx";

            // Load the diagram (ensure disposal to free resources)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape contains text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}");

                            // Iterate over character formatting runs within the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font size is stored in inches; convert to points (1 inch = 72 points)
                                double fontSizePoints = ch.Size.Value * 72.0;
                                Console.WriteLine($"  Char Index: {ch.IX}, Font: {ch.FontName.Value}, Size (pts): {fontSizePoints}");
                            }
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
