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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [shapeName] [rows] [cols]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeName = args.Length >= 3 ? args[2] : null;
        int rows = args.Length >= 4 && int.TryParse(args[3], out int r) ? r : 3;
        int cols = args.Length >= 5 && int.TryParse(args[4], out int c) ? c : 3;

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the active page.
            Page page = diagram.ActivePage;

            // Locate the shape to duplicate.
            Shape originalShape = null;
            if (!string.IsNullOrEmpty(shapeName))
            {
                // Search by NameU.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape s = page.Shapes[i];
                    if (s.NameU == shapeName)
                    {
                        originalShape = s;
                        break;
                    }
                }

                if (originalShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU \"{shapeName}\" not found on the active page.");
                    return;
                }
            }
            else
            {
                // Use the first shape on the page if no name is provided.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The active page contains no shapes to duplicate.");
                    return;
                }
                originalShape = page.Shapes[0];
            }

            // Ensure the shape has an associated master (required for duplication).
            if (originalShape.Master == null || string.IsNullOrEmpty(originalShape.Master.Name))
            {
                Console.Error.WriteLine("The selected shape does not have a master shape and cannot be duplicated.");
                return;
            }

            string masterName = originalShape.Master.Name;

            // Retrieve original shape dimensions and position.
            double width = originalShape.XForm.Width.Value;
            double height = originalShape.XForm.Height.Value;
            double basePinX = originalShape.XForm.PinX.Value;
            double basePinY = originalShape.XForm.PinY.Value;

            // Define spacing between duplicated shapes.
            double marginX = 0.5; // inches
            double marginY = 0.5; // inches

            // Duplicate the shape in a rows x cols grid.
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Skip the original shape position (row 0, col 0) if desired.
                    // Here we keep the original and add duplicates elsewhere.
                    if (row == 0 && col == 0)
                        continue;

                    double newPinX = basePinX + col * (width + marginX);
                    double newPinY = basePinY + row * (height + marginY);

                    // Add a new shape using the same master.
                    long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);

                    // Optionally retrieve the newly added shape for further customization.
                    // Shape newShape = page.Shapes.GetShape(newShapeId);
                }
            }

            // Prepare PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Use a common font as fallback.
                DefaultFont = "Arial",
                ExportHiddenPage = false,
                PageIndex = 0,
                PageCount = 1,
                SplitMultiPages = false
            };

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram saved successfully to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}