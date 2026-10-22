using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input Visio file not found at '{inputPath}'.");
            return;
        }

        // Output Visio diagram path
        string outputPath = "output.vsdx";

        // Target PDF document to open via hyperlink
        string pdfPath = "document.pdf";
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: Target PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure at least one window exists
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowHeight = 600;
                    window.WindowWidth = 800;
                    window.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(window);
                }

                // Access the first page (create one if none exist)
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Add a rectangle shape to the page
                long shapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle");
                Shape shape = page.Shapes.GetShape(shapeId);

                // Verify the shape is not marked as deleted
                if (shape.Del == BOOL.True)
                {
                    Console.Error.WriteLine("Error: Created shape is marked as deleted.");
                    return;
                }

                // Ensure the Hyperlinks collection is available
                if (shape.Hyperlinks == null)
                {
                    Console.Error.WriteLine("Error: Shape's Hyperlinks collection is null.");
                    return;
                }

                // Create a new hyperlink pointing to the PDF document
                Hyperlink link = new Hyperlink();
                link.Name = "OpenPDF";
                link.Address.Value = pdfPath;               // Target PDF file path
                // The NewWindow property is read‑only; default behavior opens in a new window for external files
                link.Description.Value = "Open PDF in new window";

                // Add the hyperlink to the shape
                shape.Hyperlinks.Add(link);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Hyperlink added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}