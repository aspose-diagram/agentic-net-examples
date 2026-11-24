using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
                // Combine flags to remove hidden information (shapes, masters, styles, data connections if supported).
                int removeFlags = (int)(
                    RemoveHiddenInfoItem.Shapes |
                    RemoveHiddenInfoItem.Masters |
                    RemoveHiddenInfoItem.Styles
                    // Uncomment the following line if the DataConnections flag is available in the referenced version:
                    // | RemoveHiddenInfoItem.DataConnections
                );

                // Remove hidden information from the diagram.
                diagram.RemoveHiddenInformation(removeFlags);

                // Verify that no hidden layers remain (layer.Visible == BOOL.False).
                bool hiddenLayerFound = false;
                foreach (Page page in diagram.Pages)
                {
                    // Access layers via the page's PageSheet.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // If a layer is not visible, it is considered hidden.
                        if (layer.Visible.Value == BOOL.False)
                        {
                            hiddenLayerFound = true;
                            // Use the layer index (IX) as an identifier to avoid potential .Name issues.
                            Console.WriteLine($"Hidden layer detected: Index {layer.IX} on page \"{page.Name}\"");
                        }
                    }
                }

                if (hiddenLayerFound)
                {
                    Console.Error.WriteLine("Verification failed: hidden layers are still present after removal.");
                    return;
                }
                else
                {
                    Console.WriteLine("Verification succeeded: no hidden layers found.");
                }

                // Save the cleaned diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}