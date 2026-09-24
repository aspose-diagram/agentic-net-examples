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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_cleaned.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Specify the page index to process (0 = first page)
            int pageIndex = 0;
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            // Retrieve the target page
            Page page = diagram.Pages[pageIndex];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Obtain the plain text of the shape
                string shapeText = shape.Text.Value.Text;

                // If the shape contains no visible text, remove all its paragraphs
                if (string.IsNullOrWhiteSpace(shapeText))
                {
                    shape.Paras.Clear();
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
