using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_with_index.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build the index content: each line contains page number and shape name
                var indexBuilder = new System.Text.StringBuilder();
                int pageNumber = 1;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Use the universal name of the shape; fallback to "(Unnamed)" if null
                        string shapeName = shape.NameU ?? "(Unnamed)";
                        indexBuilder.AppendLine($"Page {pageNumber}: {shapeName}");
                    }
                    pageNumber++;
                }

                // Create a new page for the index
                Page indexPage = new Page();
                diagram.Pages.Add(indexPage);
                // Move the new page to the first position (prepend)
                indexPage.MoveTo(0);
                indexPage.Name = "Index";

                // Add a rectangle shape to hold the index text.
                // Use the built‑in "Rectangle" master; position at (1,1) inches.
                double pinX = 1.0;
                double pinY = 1.0;
                double rectWidth = 8.0; // inches
                double rectHeight = Math.Max(1.0, indexBuilder.ToString().Split('\n').Length * 0.2);
                long rectId = indexPage.AddShape(pinX, pinY, rectWidth, rectHeight, "Rectangle", false);
                Shape rectShape = indexPage.Shapes.GetShape((int)rectId);

                // Clear any existing text and set the multiline index content
                rectShape.Text.Value.Clear();
                rectShape.Text.Value.Add(new Txt(indexBuilder.ToString()));

                // Ensure the rectangle size matches the desired dimensions
                rectShape.XForm.Width.Value = rectWidth;
                rectShape.XForm.Height.Value = rectHeight;

                // Save the modified diagram with the index page prepended
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with index page: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}