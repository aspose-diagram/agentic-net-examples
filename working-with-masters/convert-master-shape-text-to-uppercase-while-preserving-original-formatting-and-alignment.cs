using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (can be passed as arguments or hard‑coded)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Each master can contain multiple shapes
                foreach (Shape shape in master.Shapes)
                {
                    // Ensure the shape has text runs
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        // Iterate over the text collection (contains Txt and Cp objects)
                        foreach (var item in shape.Text.Value)
                        {
                            // Only modify actual text runs (Txt)
                            if (item is Txt txt && !string.IsNullOrEmpty(txt.Text))
                            {
                                // Convert the text to uppercase while leaving formatting (Cp) untouched
                                txt.Text = txt.Text.ToUpperInvariant();
                            }
                        }
                    }
                }
            }

            // Save the modified diagram, preserving all original formatting and alignment
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
