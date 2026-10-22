using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new page for the Table of Contents
                Page tocPage = new Page();
                diagram.Pages.Add(tocPage);
                tocPage.Name = "Table of Contents";
                tocPage.NameU = "Table of Contents";

                // Layout variables for placing TOC entries (in inches)
                double startX = 1.0;
                double startY = 1.0;
                double lineHeight = 0.5;

                // Iterate over existing pages (excluding the newly added TOC page)
                int pageCount = diagram.Pages.Count;
                for (int i = 0; i < pageCount - 1; i++)
                {
                    Page srcPage = diagram.Pages[i];

                    // Add a rectangle shape that will serve as the hyperlink entry
                    // The fourth argument is a bool indicating whether to calculate geometry; use false
                    long shapeId = tocPage.AddShape(startX, startY + i * lineHeight, "Rectangle", false);
                    Shape entryShape = tocPage.Shapes.GetShape((int)shapeId);

                    // Clear any existing text and set the displayed text to the source page title
                    entryShape.Text.Value.Clear();
                    entryShape.Text.Value.Add(new Txt(srcPage.NameU));

                    // Create a hyperlink that points to the source page
                    Hyperlink link = new Hyperlink();
                    link.SubAddress.Value = srcPage.NameU;               // Target page name
                    link.Description.Value = $"Go to page {srcPage.NameU}";
                    link.NewWindow.Value = BOOL.False;                  // Open in same window

                    // Add the hyperlink to the shape
                    entryShape.Hyperlinks.Add(link);
                }

                // Save the modified diagram to the specified output path in VSDX format
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