using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify triangle shapes by master name
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Set fill foreground transparency to 75% (0.75 = 75% transparent)
                        shape.Fill.FillForegndTrans.Value = 0.75;
                        // Optionally, also set background transparency if needed
                        shape.Fill.FillBkgndTrans.Value = 0.75;
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
