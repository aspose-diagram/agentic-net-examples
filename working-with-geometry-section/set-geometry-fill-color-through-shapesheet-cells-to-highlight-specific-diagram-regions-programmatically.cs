using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Aspose.Diagram.Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Aspose.Diagram.Shape shape = page.Shapes[shapeIndex];

                        // Example condition: highlight shapes with even IDs.
                        if (shape.ID % 2 == 0)
                        {
                            // Ensure the shape has a solid fill pattern.
                            shape.Fill.FillPattern.Value = 1; // 1 = solid

                            // Set the foreground fill color to a bright yellow.
                            shape.Fill.FillForegnd.Value = "#FFFF00";

                            // Optionally set background fill color (not required for solid fill).
                            shape.Fill.FillBkgnd.Value = "#FFFFFF";

                            // Ensure the fill is fully opaque (0.0 = opaque, 1.0 = transparent).
                            shape.Fill.FillForegndTrans.Value = 0.0;
                        }
                    }
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}