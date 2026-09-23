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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Each master can contain one or more shapes
                foreach (Shape shape in master.Shapes)
                {
                    // Iterate over all text runs within the shape
                    foreach (object item in shape.Text.Value)
                    {
                        if (item is Txt txt && !string.IsNullOrEmpty(txt.Text))
                        {
                            // Convert the text to uppercase while keeping formatting unchanged
                            txt.Text = txt.Text.ToUpperInvariant();
                        }
                    }

                    // Alignment and other formatting are stored in paragraph/shape properties.
                    // No changes are needed here because we only modify the text content.
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
