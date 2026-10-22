using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, master name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <masterName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetMasterName = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only shapes that are based on the specified master
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Iterate over each paragraph in the shape
                            for (int i = 0; i < shape.Paras.Count; i++)
                            {
                                // Set paragraph horizontal alignment to Justify
                                shape.Paras[i].HorzAlign.Value = HorzAlignValue.Justify;
                            }
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Paragraph alignment updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}