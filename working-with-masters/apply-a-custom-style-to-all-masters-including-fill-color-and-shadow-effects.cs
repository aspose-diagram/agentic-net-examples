using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Styled_" + Path.GetFileName(inputPath));

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Iterate over each shape within the master
                foreach (Shape shape in master.Shapes)
                {
                    // Apply a solid fill color (light blue) to the shape
                    shape.Fill.FillPattern.Value = 1; // Solid fill
                    shape.Fill.FillForegnd.Value = "#ADD8E6"; // Light blue foreground

                    // Apply a simple drop shadow
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple; // Drop shadow type
                    shape.Fill.ShdwForegnd.Value = "#000000"; // Shadow color (black)
                    shape.Fill.ShdwForegndTrans.Value = 0.3; // 30% shadow transparency
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}