using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only rectangle shapes (identified by master name)
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Ensure the shape has at least one paragraph before setting alignment
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Apply centered horizontal alignment to each paragraph
                            foreach (Aspose.Diagram.Para para in shape.Paras)
                            {
                                // Set horizontal alignment to center (correct enum member)
                                para.HorzAlign.Value = HorzAlignValue.Center;
                            }
                        }

                        // Apply centered vertical alignment to the text block if it exists
                        if (shape.TextBlock != null)
                        {
                            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
                        }
                    }
                }
            }

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}