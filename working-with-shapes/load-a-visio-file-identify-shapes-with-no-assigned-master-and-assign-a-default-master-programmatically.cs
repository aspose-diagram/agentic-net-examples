using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio document (replace with your actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Choose a default master.
                // Here we take the first master in the document.
                // You can also load a master from a stencil/template if needed.
                Master defaultMaster = diagram.Masters[0];

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shapes without an assigned master have Master == null.
                        if (shape.Master == null)
                        {
                            // Assign the default master to the shape.
                            shape.Master = defaultMaster;
                        }
                    }
                }

                // Save the modified diagram (replace with your desired output path).
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
