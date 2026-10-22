using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path; default to "output.vsdx" in the current directory.
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page height (in inches).
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define a proportional factor for line thickness (1% of page height).
                const double thicknessFactor = 0.01;

                // Iterate over all shapes on the page.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only 1‑D connector shapes.
                    if (shape.OneD)
                    {
                        // Calculate new line weight based on page height.
                        double newWeight = pageHeight * thicknessFactor;

                        // Apply the new line weight (in inches).
                        shape.Line.LineWeight.Value = newWeight;
                    }
                }
            }

            // Save the modified diagram to the specified output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}