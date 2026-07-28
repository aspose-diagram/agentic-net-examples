using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string filePath = "sample.vsdx";
            Diagram diagram = new Diagram(filePath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.WriteLine("Failed to retrieve a shape.");
                return;
            }

            // Access inherited fill settings
            var inheritFill = shape.InheritFill;

            Console.WriteLine("Inherited Fill Settings:");
            Console.WriteLine($"  Fill Foreground Color: {inheritFill.FillForegnd.Value}");
            Console.WriteLine($"  Fill Background Color: {inheritFill.FillBkgnd.Value}");
            Console.WriteLine($"  Fill Pattern: {inheritFill.FillPattern.Value}");
            Console.WriteLine($"  Shadow Foreground Color: {inheritFill.ShdwForegnd.Value}");
            Console.WriteLine($"  Shadow Pattern: {inheritFill.ShdwPattern.Value}");
            Console.WriteLine($"  Shape Shadow Type: {inheritFill.ShapeShdwType.Value}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
