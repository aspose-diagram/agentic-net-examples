using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
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
                // Iterate through pages to find the pentagon shape
                bool pentagonFound = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses the "Pentagon" master
                        if (shape.Master != null && string.Equals(shape.Master.Name, "Pentagon", StringComparison.OrdinalIgnoreCase))
                        {
                            // Clear any existing text
                            shape.Text.Value.Clear();

                            // Add the centered label text
                            shape.Text.Value.Add(new Txt("Annotation"));

                            // Center the text vertically within the shape
                            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                            // Align the text block to the shape's center and size
                            shape.TextXForm.TxtPinX.Value = shape.XForm.PinX.Value;
                            shape.TextXForm.TxtPinY.Value = shape.XForm.PinY.Value;
                            shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;
                            shape.TextXForm.TxtHeight.Value = shape.XForm.Height.Value;

                            pentagonFound = true;
                            break; // Assuming only one pentagon needs labeling
                        }
                    }
                    if (pentagonFound) break;
                }

                if (!pentagonFound)
                {
                    Console.Error.WriteLine("No pentagon shape was found in the diagram.");
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