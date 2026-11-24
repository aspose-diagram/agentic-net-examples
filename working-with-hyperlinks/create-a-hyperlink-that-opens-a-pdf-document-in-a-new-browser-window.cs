using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input Visio file, output Visio file, PDF URL.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <pdfUrl>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pdfUrl = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one window; add one if necessary.
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 800;
                    window.WindowHeight = 600;
                    window.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(window);
                }

                // Work with the first page of the diagram.
                Page page = diagram.Pages[0];

                // Add a simple rectangle shape that will hold the hyperlink.
                // Parameters: pinX, pinY, width, height, master name.
                double pinX = 5.0;
                double pinY = 5.0;
                double width = 2.0;
                double height = 1.0;
                long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle");

                // Retrieve the newly added shape.
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Skip processing if the shape is marked as deleted.
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("The newly created shape is marked as deleted. Aborting.");
                    return;
                }

                // Ensure the Hyperlinks collection is instantiated.
                if (shape.Hyperlinks == null)
                {
                    Console.Error.WriteLine("Hyperlinks collection is null. Cannot add hyperlink.");
                    return;
                }

                // Create a new hyperlink that points to the specified PDF URL.
                Hyperlink link = new Hyperlink();
                link.Name = "PdfLink";
                link.Address.Value = pdfUrl;          // Target PDF document.
                link.NewWindow.Value = BOOL.True;    // Open in a new browser window.

                // Add the hyperlink to the shape.
                shape.Hyperlinks.Add(link);

                // Save the modified diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}