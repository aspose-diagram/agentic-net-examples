using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool fieldUpdated = false;

                // Iterate through all pages and shapes to find a shape with at least one custom property (Prop)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Props collection with at least one item
                        if (shape.Props != null && shape.Props.Count > 0)
                        {
                            // Update the first existing property to calculate area = Width * Height
                            // The formula is set as a Visio expression string
                            Prop prop = shape.Props[0];
                            prop.Value.Ufev.F = "Width*Height";
                            // Use Undefined unit for the formula result
                            prop.Value.Ufev.Unit = MeasureConst.Undefined;

                            fieldUpdated = true;
                            // Break after updating the first found property
                            break;
                        }
                    }

                    if (fieldUpdated)
                        break;
                }

                if (!fieldUpdated)
                {
                    Console.Error.WriteLine("No shape with an existing custom property (Prop) was found in the diagram.");
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}