using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, help document URL or file path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <helpDocumentPathOrUrl>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string helpPath = args[2];

        // Guard against missing input diagram file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputPath}");
            return;
        }

        // If the help document is a local file, ensure it exists.
        if (!helpPath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !helpPath.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
            !File.Exists(helpPath))
        {
            Console.Error.WriteLine($"Help document file not found: {helpPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page as the target.
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape that has an associated master.
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                if (shape.Master != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape with a master was found on the first page.");
                return;
            }

            // Ensure the Hyperlinks collection is initialized.
            if (targetShape.Hyperlinks == null)
            {
                // The Hyperlinks collection should be non-null by default, but guard just in case.
                Console.Error.WriteLine("The target shape's Hyperlinks collection is null.");
                return;
            }

            // Create a new hyperlink instance.
            Hyperlink link = new Hyperlink();
            link.Name = "HelpLink";
            // Set the address to the provided help document path or URL.
            link.Address.Value = helpPath;
            // Optionally, set a description that appears as tooltip.
            link.Description.Value = "Open help documentation";

            // Add the hyperlink to the shape.
            targetShape.Hyperlinks.Add(link);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Hyperlink added successfully. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}