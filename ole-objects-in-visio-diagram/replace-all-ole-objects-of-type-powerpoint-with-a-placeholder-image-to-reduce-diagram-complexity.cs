using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, placeholder image path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <placeholderImagePath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string placeholderImagePath = args[1];
        string outputDiagramPath = args[2];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: verify placeholder image file exists
        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"Placeholder image file not found: {placeholderImagePath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Read placeholder image bytes once
                byte[] placeholderBytes = File.ReadAllBytes(placeholderImagePath);
                string placeholderFileName = Path.GetFileName(placeholderImagePath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify foreign (OLE) shapes
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ObjectSourceFullName != null)
                        {
                            // Check if the OLE object is a PowerPoint object
                            if (shape.ForeignData.ObjectSourceFullName.IndexOf("PowerPoint", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                shape.ForeignData.ObjectSourceFullName.EndsWith(".ppt", StringComparison.OrdinalIgnoreCase) ||
                                shape.ForeignData.ObjectSourceFullName.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase))
                            {
                                // Replace the OLE data with the placeholder image bytes
                                shape.ForeignData.ObjectData = placeholderBytes;
                                shape.ForeignData.ObjectSourceFullName = placeholderFileName;

                                // Optionally ensure the object is not shown as an icon
                                shape.ForeignData.ShowAsIcon = BOOL.False;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}