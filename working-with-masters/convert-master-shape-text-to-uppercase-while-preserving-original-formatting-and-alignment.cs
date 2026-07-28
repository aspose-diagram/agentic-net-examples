using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_uppercase.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Iterate through each shape within the master
                foreach (Shape shape in master.Shapes)
                {
                    // Access the collection of text runs (Txt objects) in the shape
                    var textRuns = shape.Text.Value;

                    // Convert each text run to uppercase while keeping formatting intact
                    foreach (var item in textRuns)
                    {
                        if (item is Txt txt && !string.IsNullOrEmpty(txt.Text))
                        {
                            txt.Text = txt.Text.ToUpperInvariant();
                        }
                    }
                }
            }

            // Save the modified diagram preserving all original formatting and alignment
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
