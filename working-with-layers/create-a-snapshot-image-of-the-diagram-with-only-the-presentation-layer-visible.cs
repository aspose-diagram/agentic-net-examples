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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each layer in the page's sheet
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Show only the layer named "Presentation", hide others
                        if (layer.Name != null && layer.Name.Value == "Presentation")
                        {
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure image export options (PNG format, first page only)
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.PageIndex = 0;   // Export the first page
                imgOptions.PageCount = 1;   // Export a single page

                // Save the snapshot image
                diagram.Save(outputPath, imgOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}