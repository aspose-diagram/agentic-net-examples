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

            // Load the diagram using the appropriate load format
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Ensure there is at least one page and one shape to modify
            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];
                if (page.Shapes.Count > 0)
                {
                    // Modify the first shape found on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Clear existing text and add new text
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt("Modified by Aspose.Diagram"));
                        break; // Only modify the first shape
                    }
                }
            }

            // Save the modified diagram to a new VSDX file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
