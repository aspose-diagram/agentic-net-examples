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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <newNetworkSharePath>");
            return;
        }

        string inputPath = args[0];
        string newSharePath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Prepare output file path (same folder, with suffix "_updated.vsdx").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Detect OLE (foreign) objects by checking ForeignData and its binary content.
                        if (shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Ensure the shape actually has a hyperlink collection.
                            if (shape.Hyperlinks != null)
                            {
                                // Update each hyperlink to point to the new network share location.
                                foreach (Hyperlink hyperlink in shape.Hyperlinks)
                                {
                                    // Replace the address value with the new share path.
                                    // If needed, you could preserve the original file name by appending it.
                                    hyperlink.Address.Value = newSharePath;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the output file in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram updated successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}