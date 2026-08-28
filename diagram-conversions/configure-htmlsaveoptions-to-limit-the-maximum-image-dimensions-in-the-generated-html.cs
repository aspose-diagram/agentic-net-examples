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

            // Configure HTML export options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Limit the size of generated images (width, height in inches)
            // Adjust these values as needed for your scenario
            htmlOptions.PageSize = new PageSize(8f, 10f); // 8 inches wide, 10 inches high

            // Optional: set the DPI for the generated images
            htmlOptions.Resolution = 96; // 96 dots per inch

            // Save the diagram as HTML using the configured options
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine("Diagram exported to HTML with limited image dimensions.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
