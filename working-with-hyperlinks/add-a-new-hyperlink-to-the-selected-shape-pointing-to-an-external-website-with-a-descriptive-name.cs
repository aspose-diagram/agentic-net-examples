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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the shape to modify (example: shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 1 not found.");
                return;
            }

            // Create a new hyperlink
            Hyperlink hyperlink = new Hyperlink();
            hyperlink.Name = "ExampleLink";
            hyperlink.Address.Value = "https://www.example.com";
            hyperlink.Description.Value = "Example Website";

            // Add the hyperlink to the shape
            shape.Hyperlinks.Add(hyperlink);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
