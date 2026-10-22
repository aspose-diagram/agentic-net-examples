using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
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
                int pageCount = diagram.Pages.Count;

                // Iterate over all pages to add navigation hyperlinks.
                for (int i = 0; i < pageCount; i++)
                {
                    Page currentPage = diagram.Pages[i];
                    // Fixed: Page.Name is a string, not a cell with .Value
                    string currentPageName = currentPage.Name ?? $"Page-{i + 1}";

                    // ----- Add "Next Page" hyperlink (if not the last page) -----
                    if (i < pageCount - 1)
                    {
                        Page nextPage = diagram.Pages[i + 1];
                        // Fixed: Page.Name is a string, not a cell with .Value
                        string nextPageName = nextPage.Name ?? $"Page-{i + 2}";

                        // Add a rectangle shape that will act as the "Next" button.
                        // Position it near the bottom‑right corner of the page.
                        long nextShapeId = currentPage.AddShape(8.0, 0.5, "Rectangle", false);
                        Shape nextShape = currentPage.Shapes.GetShape(nextShapeId);

                        // Create and configure the hyperlink to the next page.
                        Hyperlink nextLink = new Hyperlink();
                        nextLink.Description.Value = "Go to next page";
                        nextLink.Address.Value = ""; // Internal link, no external address.
                        nextLink.SubAddress.Value = nextPageName;
                        nextLink.NewWindow.Value = BOOL.False;

                        // Attach the hyperlink to the shape.
                        nextShape.Hyperlinks.Add(nextLink);
                    }

                    // ----- Add "Previous Page" hyperlink (if not the first page) -----
                    if (i > 0)
                    {
                        Page prevPage = diagram.Pages[i - 1];
                        // Fixed: Page.Name is a string, not a cell with .Value
                        string prevPageName = prevPage.Name ?? $"Page-{i}";

                        // Add a rectangle shape that will act as the "Previous" button.
                        // Position it near the bottom‑left corner of the page.
                        long prevShapeId = currentPage.AddShape(0.5, 0.5, "Rectangle", false);
                        Shape prevShape = currentPage.Shapes.GetShape(prevShapeId);

                        // Create and configure the hyperlink to the previous page.
                        Hyperlink prevLink = new Hyperlink();
                        prevLink.Description.Value = "Go to previous page";
                        prevLink.Address.Value = "";
                        prevLink.SubAddress.Value = prevPageName;
                        prevLink.NewWindow.Value = BOOL.False;

                        // Attach the hyperlink to the shape.
                        prevShape.Hyperlinks.Add(prevLink);
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
