using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to analyze
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the width and height from the shape's XForm (in inches)
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Log the bounding box dimensions
                    Console.WriteLine($"Shape ID {shape.ID} - Width: {width} in, Height: {height} in");
                }
            }

            // Optional: save the diagram after analysis (uncomment if needed)
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
