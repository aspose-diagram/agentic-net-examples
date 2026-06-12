using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least three pages
            if (diagram.Pages.Count < 3)
            {
                Console.WriteLine("The diagram does not contain a third page.");
                return;
            }

            // Get the third page (zero‑based index)
            Page page = diagram.Pages[2];

            // Current system date string (you can adjust the format as needed)
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Iterate over the text runs within the shape
                foreach (var fmt in shape.Text.Value)
                {
                    if (fmt is Txt txt && txt.Text != null && txt.Text.Contains("[Date]"))
                    {
                        // Replace the placeholder with the current date
                        txt.Text = txt.Text.Replace("[Date]", currentDate);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
