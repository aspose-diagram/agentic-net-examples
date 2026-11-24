using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
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
                // Ensure the diagram has at least three pages (zero‑based index 2)
                if (diagram.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The diagram does not contain a third page.");
                    return;
                }

                // Retrieve the third page (page index 2)
                Page page = diagram.Pages[2];

                // Iterate over all shapes on the third page
                foreach (Shape shape in page.Shapes)
                {
                    // Set text rotation to 90 degrees (π/2 radians)
                    shape.TextXForm.TxtAngle.Value = Math.PI / 2;
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Text rotation applied and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}