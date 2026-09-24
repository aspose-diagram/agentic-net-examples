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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

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

            // Current system date string (customize format as needed)
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Iterate over the text runs within the shape
                foreach (var item in shape.Text.Value)
                {
                    if (item is Txt txtRun && txtRun.Text.Contains("[Date]"))
                    {
                        // Replace the placeholder with the current date
                        txtRun.Text = txtRun.Text.Replace("[Date]", currentDate);
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Placeholder '[Date]' replaced with '{currentDate}' on page 3. Saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
