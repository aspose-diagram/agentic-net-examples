using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden information: shapes, masters, and styles.
                int removeFlags = (int)(RemoveHiddenInfoItem.Shapes |
                                         RemoveHiddenInfoItem.Masters |
                                         RemoveHiddenInfoItem.Styles);
                diagram.RemoveHiddenInformation(removeFlags);

                // Verify that no hidden layers remain in any page.
                bool hiddenLayerFound = false;
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through layers via the page's PageSheet.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Layer visibility is stored as Aspose.Diagram.BOOL.
                        if (layer.Visible.Value == BOOL.False)
                        {
                            hiddenLayerFound = true;
                            Console.WriteLine($"Hidden layer detected: \"{layer.Name.Value}\" on page \"{page.Name}\"");
                        }
                    }
                }

                if (hiddenLayerFound)
                {
                    // If hidden layers are still present, raise an error.
                    throw new Exception("Hidden layers were found after removal of hidden information.");
                }
                else
                {
                    Console.WriteLine("Verification passed: No hidden layers present.");
                }

                // Save the cleaned diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}