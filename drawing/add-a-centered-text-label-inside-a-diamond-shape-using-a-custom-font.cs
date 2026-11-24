using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the first page of the diagram.
                Page page = diagram.Pages[0];

                // Define points that form a diamond shape (flat double array).
                double[] diamondPoints = new double[]
                {
                    2, 0,   // top
                    4, 2,   // right
                    2, 4,   // bottom
                    0, 2,   // left
                    2, 0    // close the shape
                };

                // Draw the diamond polyline; returns the structural shape ID.
                long shapeId = page.DrawPolyline(diamondPoints);

                // Retrieve the newly created shape.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add the custom label.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Custom Font Label"));

                // Add a paragraph to control horizontal alignment.
                Para para = new Para();
                shape.Paras.Add(para);
                // Center the text horizontally within the shape.
                para.HorzAlign.Value = HorzAlignValue.Center; // corrected enum member

                // Center the text vertically within the shape.
                shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                // Add character formatting for the first text run.
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = 0; // Index of the character run.
                ch.FontName.Value = "Arial Unicode MS"; // Custom font name.
                ch.Color.Value = "#FF0000"; // Red color in hex format.
                ch.Size.Value = 12.0 / 72.0; // Font size: 12 points (Visio uses inches, 1pt = 1/72 inch).
                shape.Chars.Add(ch);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
