using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <newNetworkSharePath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string newSharePath = args[1];
        string outputPath = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify OLE objects via ForeignData and its ObjectData.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null)
                        {
                            // Update each hyperlink's address to the new network share location.
                            foreach (Aspose.Diagram.Hyperlink link in shape.Hyperlinks)
                            {
                                // Replace the hyperlink address.
                                link.Address.Value = newSharePath;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}