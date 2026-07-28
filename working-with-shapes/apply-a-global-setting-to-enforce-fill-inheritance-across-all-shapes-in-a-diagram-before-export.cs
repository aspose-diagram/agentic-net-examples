using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Fill element before copying values
                    if (shape.Fill != null && shape.InheritFill != null)
                    {
                        // Copy fill foreground color
                        shape.InheritFill.FillForegnd = shape.Fill.FillForegnd;
                        // Copy fill background color
                        shape.InheritFill.FillBkgnd = shape.Fill.FillBkgnd;
                        // Copy fill pattern
                        shape.InheritFill.FillPattern = shape.Fill.FillPattern;
                        // Copy foreground transparency
                        shape.InheritFill.FillForegndTrans = shape.Fill.FillForegndTrans;
                        // Copy background transparency
                        shape.InheritFill.FillBkgndTrans = shape.Fill.FillBkgndTrans;
                    }
                }
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
