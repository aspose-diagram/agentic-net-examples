using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Remove hidden information (shapes and masters). The enum does not contain a Layers flag,
            // so we omit it and rely on layer visibility checks after removal.
            int removeFlags = (int)(RemoveHiddenInfoItem.Shapes |
                                    RemoveHiddenInfoItem.Masters);
            diagram.RemoveHiddenInformation(removeFlags);

            // Verify that no hidden layers remain (layers with Visible == FALSE)
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Visible.Value == BOOL.False)
                    {
                        throw new Exception($"Hidden layer detected: {layer.Name.Value}");
                    }
                }
            }

            // Output Visio file path
            string outputPath = "output.vsdx";

            // Save the cleaned diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}