using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the HTML of the selected shape will be saved
            string outputPath = "shape.html";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (you can change the index as needed)
            Page page = diagram.Pages[0];

            // Select a shape by its ID (replace 1 with the desired shape ID)
            Shape shape = null;
            try
            {
                shape = page.Shapes.GetShape(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving shape: {ex.Message}");
                return;
            }

            // Create default HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export the selected shape to HTML, preserving geometry and style
            shape.ToHTML(outputPath, htmlOptions);

            Console.WriteLine("Shape exported to HTML successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
