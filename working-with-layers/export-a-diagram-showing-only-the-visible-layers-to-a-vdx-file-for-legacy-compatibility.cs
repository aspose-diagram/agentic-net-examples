using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output VDX file path
        string outputPath = "output.vdx";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page and its layers
                foreach (Page page in diagram.Pages)
                {
                    // Ensure only visible layers remain visible; hidden layers stay hidden
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // No action needed because hidden layers are already marked as FALSE.
                        // If you need to explicitly hide them, you could set:
                        // if (layer.Visible.Value == Aspose.Diagram.BOOL.False)
                        //     layer.Visible.Value = Aspose.Diagram.BOOL.False;
                    }
                }

                // Save the diagram to VDX format (legacy compatibility)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

            Console.WriteLine($"Diagram exported successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}