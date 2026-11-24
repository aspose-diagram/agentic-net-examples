using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine the maximum existing page ID (Page.ID is an int)
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Create a new page that will serve as the Table of Contents
                Page tocPage = new Page();
                tocPage.Name = "Table of Contents";
                tocPage.ID = maxPageId + 1; // assign a unique ID

                // Add the TOC page to the diagram
                diagram.Pages.Add(tocPage);

                // Layout parameters for the TOC entries (in inches)
                double startX = 1.0;   // horizontal position
                double startY = 1.0;   // starting vertical position
                double deltaY = 0.5;   // space between entries

                int entryIndex = 0;

                // Iterate over all pages except the newly added TOC page
                foreach (Page page in diagram.Pages)
                {
                    // Skip the TOC page itself
                    if (page == tocPage)
                        continue;

                    // Calculate position for the current entry
                    double posY = startY + entryIndex * deltaY;

                    // Add a rectangle shape that will hold the page title and hyperlink
                    // The fourth argument (isCalculate) must be a bool
                    long shapeId = tocPage.AddShape(startX, posY, "Rectangle", false);
                    // GetShape expects an int, so cast the returned long ID
                    Shape shape = tocPage.Shapes.GetShape((int)shapeId);

                    // Clear any existing text and add the page's title
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(page.NameU));

                    // Create a hyperlink that points to the target page within the same document
                    Hyperlink link = new Hyperlink();
                    link.SubAddress.Value = page.NameU;          // target page name
                    link.NewWindow.Value = BOOL.False;           // open in the same window

                    // Attach the hyperlink to the shape
                    shape.Hyperlinks.Add(link);

                    entryIndex++;
                }

                // Save the modified diagram to the specified output path as VSDX
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}