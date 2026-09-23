using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                int hyperlinkCount = 0;

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Count each hyperlink attached to the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            hyperlinkCount++;
                        }
                    }
                }

                // Output the summary for the current page
                Console.WriteLine($"Page '{page.Name}' (ID: {page.ID}) contains {hyperlinkCount} hyperlink(s).");
            }

            // Optionally save the diagram (unchanged) to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
