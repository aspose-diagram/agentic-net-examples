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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_cleaned.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Specify the page index (0‑based). Change as needed.
            int pageIndex = 0;

            // Validate page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                throw new Exception($"Page index {pageIndex} is out of range.");
            }

            // Get the target page
            Page page = diagram.Pages[pageIndex];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve the plain text of the shape
                string shapeText = shape.Text.Value.ToString();

                // If the shape has no visible text, remove its paragraph formatting
                if (string.IsNullOrWhiteSpace(shapeText))
                {
                    // Clear all paragraph entries (they represent empty paragraphs)
                    shape.Paras.Clear();
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
