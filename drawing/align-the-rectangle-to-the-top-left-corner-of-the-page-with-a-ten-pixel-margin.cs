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
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Convert 10 pixels to inches (assuming 96 DPI)
                double marginInches = 10.0 / 96.0;

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Locate the first rectangle shape on the page
                Shape rectangle = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        rectangle = shape;
                        break;
                    }
                }

                if (rectangle == null)
                {
                    throw new Exception("Rectangle shape not found on the first page.");
                }

                // Align the rectangle to the top‑left corner with a 10‑pixel margin
                rectangle.XForm.PinX.Value = marginInches; // left margin
                rectangle.XForm.PinY.Value = marginInches; // top margin

                // Save the updated diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
