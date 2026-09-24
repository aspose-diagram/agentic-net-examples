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
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each text run in the shape
                    foreach (var item in shape.Text.Value)
                    {
                        if (item is Txt txt && txt.Text != null)
                        {
                            // Replace the trademark symbol (™) with the word 'Trademark'
                            if (txt.Text.Contains("\u2122"))
                            {
                                txt.Text = txt.Text.Replace("\u2122", "Trademark");
                            }
                        }
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
