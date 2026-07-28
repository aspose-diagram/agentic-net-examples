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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Path where the modified diagram will be saved
            string outputPath = "output.vsdx";

            // The actual name to replace the placeholder with
            string userName = "John Doe";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through the text runs (Txt objects) within the shape's text collection
                    foreach (var item in shape.Text.Value)
                    {
                        if (item is Txt txt)
                        {
                            // Replace the placeholder "[Name]" with the actual user name
                            if (txt.Text != null && txt.Text.Contains("[Name]"))
                            {
                                txt.Text = txt.Text.Replace("[Name]", userName);
                            }
                        }
                    }
                }
            }

            // Save the modified diagram back to a file (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
