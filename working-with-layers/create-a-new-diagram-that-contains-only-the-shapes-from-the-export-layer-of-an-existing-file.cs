using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block as required.
            using (Diagram srcDiagram = new Diagram(inputPath))
            {
                // Find the layer named "Export" in each page and adjust visibility.
                foreach (Page page in srcDiagram.Pages)
                {
                    // Iterate through layers on the page's PageSheet.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare layer name (case-sensitive) with "Export".
                        if (layer.Name.Value == "Export")
                        {
                            // Ensure the Export layer is visible.
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            // Hide all other layers.
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Save the modified diagram to the output path in VSDX format.
                srcDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}