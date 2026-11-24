using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageCount = diagram.Pages.Count;

                // Iterate over each page to add navigation hyperlinks.
                for (int i = 0; i < pageCount; i++)
                {
                    Page page = diagram.Pages[i];

                    // ---------- Add "Next Page" button ----------
                    if (i < pageCount - 1)
                    {
                        // Add a rectangle shape that will act as the "Next Page" button.
                        long shapeId = page.AddShape(1.0, 1.0, "Rectangle", false);
                        Shape shape = page.Shapes.GetShape(shapeId);

                        if (shape != null)
                        {
                            // Replace any existing text with "Next Page".
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt("Next Page"));

                            // Create a hyperlink that points to the next page.
                            Hyperlink link = new Hyperlink();
                            link.Description.Value = "Navigate to the next page";
                            link.Address.Value = ""; // Empty address indicates an internal link.
                            link.SubAddress.Value = diagram.Pages[i + 1].Name; // Target page name.
                            link.NewWindow.Value = BOOL.False;

                            // Add the hyperlink to the shape.
                            shape.Hyperlinks.Add(link);
                        }
                    }

                    // ---------- Add "Previous Page" button ----------
                    if (i > 0)
                    {
                        // Add a rectangle shape that will act as the "Previous Page" button.
                        long shapeId = page.AddShape(2.0, 1.0, "Rectangle", false);
                        Shape shape = page.Shapes.GetShape(shapeId);

                        if (shape != null)
                        {
                            // Replace any existing text with "Previous Page".
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt("Previous Page"));

                            // Create a hyperlink that points to the previous page.
                            Hyperlink link = new Hyperlink();
                            link.Description.Value = "Navigate to the previous page";
                            link.Address.Value = "";
                            link.SubAddress.Value = diagram.Pages[i - 1].Name;
                            link.NewWindow.Value = BOOL.False;

                            // Add the hyperlink to the shape.
                            shape.Hyperlinks.Add(link);
                        }
                    }
                }

                // Save the modified diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}