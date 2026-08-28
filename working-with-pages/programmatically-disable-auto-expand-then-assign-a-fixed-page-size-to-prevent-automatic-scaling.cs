using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (will contain the modified page settings)
        string outputPath = "output_fixed.vsdx";

        try
        {
            // Load the diagram inside a using block to ensure resources are released
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Disable automatic page expansion (auto‑expand)
                    page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                    // Assign a fixed page size (A4: 8.27" x 11.69")
                    page.PageSheet.PageProps.PageWidth.Value = 8.27;
                    page.PageSheet.PageProps.PageHeight.Value = 11.69;
                }

                // Save the modified diagram to the specified output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}