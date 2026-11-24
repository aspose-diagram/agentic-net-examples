using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output Visio file, website URL
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <websiteUrl>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string websiteUrl = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether a rectangle shape was found
            bool rectangleFound = false;

            // Iterate through all pages and shapes to locate a rectangle shape
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Access the page
                Page page = diagram.Pages[pageIndex];

                // Iterate over shapes in the page
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    // Retrieve the shape
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify rectangle shapes by their NameU (Visio default name for rectangles)
                    if (shape.NameU != null && shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        // Add display text to the shape (optional but clarifies the hyperlink)
                        shape.Text.Value.Add(new Txt("Click Here"));

                        // Create a new hyperlink instance
                        Hyperlink link = new Hyperlink();

                        // Assign a name for the hyperlink (optional)
                        link.Name = "WebLink";

                        // Set the address to the provided website URL
                        link.Address.Value = websiteUrl;

                        // Optionally, set a description (tooltip) for the hyperlink
                        link.Description.Value = "Open website";

                        // Add the hyperlink to the shape's Hyperlinks collection
                        shape.Hyperlinks.Add(link);

                        rectangleFound = true;
                        // Break out of loops after processing the first rectangle
                        break;
                    }
                }

                if (rectangleFound)
                    break;
            }

            if (!rectangleFound)
            {
                Console.Error.WriteLine("No rectangle shape was found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}