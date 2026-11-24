using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define header position: centered horizontally, near the top edge.
                    double headerPinX = pageWidth / 2.0;          // Center X
                    double headerPinY = pageHeight - 0.5;        // 0.5 inches from top

                    // Add a rectangle shape to serve as the header.
                    // The fourth argument (isCalculate) is a plain bool.
                    long shapeId = page.AddShape(headerPinX, headerPinY, "Rectangle", false);

                    // Retrieve the newly added shape by its ID.
                    Shape headerShape = page.Shapes.GetShape(shapeId);

                    // Set a reasonable size for the header shape.
                    headerShape.XForm.Width.Value = 3.0;   // 3 inches wide
                    headerShape.XForm.Height.Value = 0.5; // 0.5 inches tall

                    // Clear any existing text and add the current date.
                    headerShape.Text.Value.Clear();
                    string currentDate = DateTime.Now.ToString("d"); // Short date pattern
                    headerShape.Text.Value.Add(new Txt(currentDate));

                    // Optional: ensure the shape is not hidden.
                    // The Del property is of type BOOL; set to FALSE to keep the shape.
                    if (headerShape.Del == BOOL.True)
                    {
                        // No direct setter; Del is read‑only BOOL cell.
                        // If needed, you could delete the shape, but we keep it visible.
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}