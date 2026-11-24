using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - output Visio file path
        // 2 - shape name to locate (NameU)
        // 3 - target page name to link to (NameU)
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <shapeName> <targetPageName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeName = args[2];
        string targetPageName = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the target page by its universal name (NameU)
                Page targetPage = null;
                foreach (Page page in diagram.Pages)
                {
                    if (page.NameU != null && page.NameU.Equals(targetPageName, StringComparison.OrdinalIgnoreCase))
                    {
                        targetPage = page;
                        break;
                    }
                }

                if (targetPage == null)
                {
                    Console.Error.WriteLine($"Target page \"{targetPageName}\" not found in the diagram.");
                    return;
                }

                // Locate the shape by its universal name (NameU) across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        if (shape.NameU != null && shape.NameU.Equals(shapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape \"{shapeName}\" not found in the diagram.");
                    return;
                }

                // Create a new hyperlink that points to the target page
                Hyperlink hyperlink = new Hyperlink();

                // Set hyperlink properties using the .Value accessor (cell‑based)
                hyperlink.Description.Value = $"Navigate to page \"{targetPageName}\"";
                // For internal page navigation, Address is empty and SubAddress holds the page name
                hyperlink.Address.Value = "";
                hyperlink.SubAddress.Value = targetPage.NameU; // Use page name directly
                // Open in the same window
                hyperlink.NewWindow.Value = BOOL.False;

                // Ensure the Hyperlinks collection is initialized (it should be, but guard anyway)
                if (targetShape.Hyperlinks == null)
                {
                    Console.Error.WriteLine("Hyperlinks collection is null on the target shape.");
                    return;
                }

                // Add the hyperlink to the shape
                targetShape.Hyperlinks.Add(hyperlink);

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Hyperlink added to shape \"{shapeName}\" linking to page \"{targetPageName}\". Saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
