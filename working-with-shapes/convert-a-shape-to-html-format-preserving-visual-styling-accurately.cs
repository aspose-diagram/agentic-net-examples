using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToHtmlExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0) and the first shape on that page (index 0)
            // Adjust indices as needed for your specific diagram
            Shape shape = diagram.Pages[0].Shapes[0];

            // Create HTML save options – default settings preserve visual styling
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Convert the shape to HTML and save to a file (replace with desired output path)
            shape.ToHTML("shape_output.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
