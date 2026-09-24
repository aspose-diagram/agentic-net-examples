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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least two pages
            if (diagram.Pages.Count < 2)
            {
                Console.WriteLine("The diagram does not contain a second page.");
                return;
            }

            // Get the second page (index 1)
            Page page = diagram.Pages[1];

            // Iterate over all shapes on the second page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the plain text of the shape
                string plainText = shape.Text.Value.Text;

                // If the text contains the target phrase, replace it in each Txt run
                if (!string.IsNullOrEmpty(plainText) && plainText.Contains("Draft"))
                {
                    // Iterate through the text runs (Txt objects) within the shape
                    foreach (var item in shape.Text.Value)
                    {
                        if (item is Txt txt && txt.Text.Contains("Draft"))
                        {
                            txt.Text = txt.Text.Replace("Draft", "Final");
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Replacement complete. Saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
