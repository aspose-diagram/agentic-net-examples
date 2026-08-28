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

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page (you can adjust the index if needed)
            Page page = diagram.Pages[0];

            // Find the first rectangle shape on the page
            Shape rectangle = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master and compare its name to "Rectangle"
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    rectangle = shape;
                    break;
                }
            }

            if (rectangle == null)
            {
                throw new Exception("No rectangle shape found on the first page.");
            }

            // Set the text direction of the rectangle to vertical (column layout)
            rectangle.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
