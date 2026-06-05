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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify diamond shapes by their master name
                    if (shape.Master != null && shape.Master.Name == "Diamond")
                    {
                        // Center text horizontally (use first paragraph)
                        if (shape.Paras.Count > 0)
                        {
                            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                        }

                        // Center text vertically
                        shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
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
