using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Iterate over each shape within the master
                foreach (Shape shape in master.Shapes)
                {
                    // Apply a solid fill pattern
                    shape.Fill.FillPattern.Value = 1; // 1 = solid

                    // Set the fill foreground color (example: orange)
                    shape.Fill.FillForegnd.Value = "#FFCC00";

                    // Configure simple drop shadow
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                    shape.Fill.ShdwForegnd.Value = "#000000";      // Shadow color: black
                    shape.Fill.ShdwForegndTrans.Value = 0.3;       // 30% transparency
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}