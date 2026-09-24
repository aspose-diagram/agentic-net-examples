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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to locate group shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify a group shape
                    if (shape.Type == TypeValue.Group)
                    {
                        // Locate the desired sub‑shape within the same page.
                        // Replace "SubShapeName" with the actual NameU of the sub‑shape.
                        foreach (Shape subShape in page.Shapes)
                        {
                            if (subShape.NameU == "SubShapeName")
                            {
                                // Set the width of the sub‑shape to 5.0 inches using double precision
                                subShape.SetWidth(5.0);
                                Console.WriteLine($"Width of sub‑shape '{subShape.NameU}' set to 5.0 inches.");
                            }
                        }
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
