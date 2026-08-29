using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to enforce fill inheritance
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Copy current fill properties to the InheritFill object
                    // This forces the shape to inherit its fill from the parent style/master
                    shape.InheritFill.FillForegnd = shape.Fill.FillForegnd;
                    shape.InheritFill.FillForegndTrans = shape.Fill.FillForegndTrans;
                    shape.InheritFill.FillBkgnd = shape.Fill.FillBkgnd;
                    shape.InheritFill.FillBkgndTrans = shape.Fill.FillBkgndTrans;
                    shape.InheritFill.FillPattern = shape.Fill.FillPattern;
                }
            }

            // Save the modified diagram (lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
