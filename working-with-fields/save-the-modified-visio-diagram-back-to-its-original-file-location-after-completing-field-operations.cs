using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string filePath = "input.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // ----- Begin field operations -----
            // Example: update a custom property named "Status" on all shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "Status")
                        {
                            prop.Value.Val = "Updated";
                        }
                    }
                }
            }
            // ----- End field operations -----

            // Save the modified diagram back to its original location
            diagram.Save(filePath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
