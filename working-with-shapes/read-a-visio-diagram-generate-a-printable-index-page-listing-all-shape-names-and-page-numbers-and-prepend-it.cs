using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_with_index.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Build the index content: each line contains page number and shape names
            StringBuilder indexBuilder = new StringBuilder();
            indexBuilder.AppendLine("Index of Shapes");
            indexBuilder.AppendLine("================");
            indexBuilder.AppendLine();

            // Iterate through all pages to collect shape information
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                int pageNumber = i + 1; // Visio pages are 1‑based for display

                // Header for each page
                indexBuilder.AppendLine($"Page {pageNumber}:");

                // Iterate through shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use the universal name of the shape; fallback to ID if name is empty
                    string shapeName = !string.IsNullOrWhiteSpace(shape.NameU)
                        ? shape.NameU
                        : $"Shape_{shape.ID}";

                    indexBuilder.AppendLine($"  - {shapeName}");
                }

                indexBuilder.AppendLine(); // Blank line between pages
            }

            // Create a new page that will hold the index
            Page indexPage = new Page();
            indexPage.NameU = "Index";

            // Add the index page to the diagram and move it to the beginning
            diagram.Pages.Add(indexPage);
            indexPage.MoveTo(0); // Position the index page as the first page

            // Add a rectangle shape that will contain the index text
            // The fourth parameter (isCalculate) must be a plain bool, not BOOL
            long shapeId = indexPage.AddShape(1.0, 1.0, "Rectangle", false);

            // Retrieve the shape object to set its text
            Shape indexShape = indexPage.Shapes.GetShape((int)shapeId);

            // Clear any existing text and assign the generated index text
            indexShape.Text.Value.Clear();
            indexShape.Text.Value.Add(new Txt(indexBuilder.ToString()));

            // Save the modified diagram with the new index page prepended
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}