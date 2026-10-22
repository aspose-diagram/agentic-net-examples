using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_locked.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load diagram inside a using block as required
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Iterate through all pages to access their layer collections
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each layer on the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Identify the layer named "Architecture"
                        if (layer.Name.Value == "Architecture")
                        {
                            // Lock the layer to prevent edits
                            layer.Lock.Value = BOOL.True;
                        }
                    }
                }

                // Save the modified diagram with the locked layer
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Write any errors to the error console
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}