using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Create a simple rectangle shape on the page
                // Parameters: pinX, pinY, width, height (all in inches)
                long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Add a hyperlink to the shape
                Hyperlink link = new Hyperlink();
                link.Name = "ExampleLink";
                link.Address.Value = "https://example.com";
                // Set the description which appears as a tooltip on mouse hover
                link.Description.Value = "Open Example Website";

                // Attach the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }