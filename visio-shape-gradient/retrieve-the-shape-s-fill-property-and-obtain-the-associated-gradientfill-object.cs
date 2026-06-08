using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Find the first shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                targetShape = shp;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Retrieve the Fill property of the shape
            Fill fill = targetShape.Fill;

            // Obtain the associated GradientFill object
            GradientFill gradient = fill.GradientFill;

            // Output some gradient fill details
            Console.WriteLine("Gradient Enabled: " + gradient.GradientEnabled.Value);
            Console.WriteLine("Gradient Direction: " + gradient.GradientDir.Value);
            Console.WriteLine("Gradient Stops Count: " + gradient.GradientStops.Count);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
