using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output file paths
        string outputWithoutAutoExpand = "output_noexpand.vsdx";
        string outputWithAutoExpand = "output_withexpand.vsdx";

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Step 1: Disable page auto‑expand (DrawingResizeType) before resizing
                // -----------------------------------------------------------------
                page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.NotAutomatically;

                // Modify the page size (values are in inches)
                double newWidthInches = 20.0;
                double newHeightInches = 15.0;
                page.PageSheet.PageProps.PageWidth.Value = newWidthInches;
                page.PageSheet.PageProps.PageHeight.Value = newHeightInches;

                // Save the diagram with auto‑expand disabled
                diagram.Save(outputWithoutAutoExpand, SaveFileFormat.Vsdx);

                // -----------------------------------------------------------------
                // Step 2: Re‑enable page auto‑expand and save again
                // -----------------------------------------------------------------
                page.PageSheet.PageProps.DrawingResizeType.Value = DrawingResizeTypeValue.Automatically;

                // Save the diagram with auto‑expand re‑enabled
                diagram.Save(outputWithAutoExpand, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}