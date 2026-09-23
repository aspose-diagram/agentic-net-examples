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

            // Path to the source Visio file (replace with an actual file path)
            string sourcePath = "input.vsdx";
            // Path for the generated SVG file
            string svgPath = "output.svg";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, master name, isCalculate (bool)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the newly added shape
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a hyperlink and assign its properties
            Hyperlink link = new Hyperlink();
            link.Name = "ExampleLink";
            link.Address.Value = "https://example.com";
            link.Description.Value = "Visit Example.com";

            // Add the hyperlink to the shape
            shape.Hyperlinks.Add(link);

            // Verify that the hyperlink was added correctly
            if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
            {
                throw new Exception("Hyperlink collection is empty after addition.");
            }

            bool addressMatches = false;
            foreach (Hyperlink hl in shape.Hyperlinks)
            {
                if (hl.Address != null && hl.Address.Value == "https://example.com")
                {
                    addressMatches = true;
                    break;
                }
            }

            if (!addressMatches)
            {
                throw new Exception("Hyperlink address does not match the expected value.");
            }

            // Save the diagram as SVG
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            diagram.Save(svgPath, svgOptions);

            Console.WriteLine("SVG file created successfully at: " + svgPath);
            Console.WriteLine("Hyperlink on shape verified and preserved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
