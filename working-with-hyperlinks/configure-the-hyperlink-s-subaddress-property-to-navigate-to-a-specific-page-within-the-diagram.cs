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
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (you can change the index as needed)
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The selected page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                // Shapes are accessed via their long ID; iterate to get one
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve a shape from the page.");
                    return;
                }

                // Create a new hyperlink
                Hyperlink hyperlink = new Hyperlink();

                // Set a description (optional)
                hyperlink.Description.Value = "Navigate to Page 2";

                // Set the SubAddress to the target page name or ID.
                // For example, to navigate to a page named "Page-2":
                hyperlink.SubAddress.Value = "Page-2";

                // Add the hyperlink to the shape's hyperlink collection
                targetShape.Hyperlinks.Add(hyperlink);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}