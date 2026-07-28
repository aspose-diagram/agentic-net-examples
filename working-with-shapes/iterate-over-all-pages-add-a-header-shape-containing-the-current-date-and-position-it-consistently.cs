using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: HeaderShapeExample <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram from the specified file.
                Diagram diagram = new Diagram(inputPath);

                // Current date string to be placed in the header.
                string dateText = DateTime.Now.ToString("d"); // e.g., 7/24/2026

                // Iterate over each page and add a header text shape.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define header position: centered horizontally, 0.5 inches from the top edge.
                    double headerPinX = pageWidth / 2.0;
                    double headerPinY = pageHeight - 0.5; // Visio origin is bottom-left.

                    // Define size of the header text shape.
                    double headerWidth = 2.0;   // inches
                    double headerHeight = 0.5;  // inches

                    // Add the text shape to the page.
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize(in inches)
                    Shape headerShape = page.AddText(
                        headerPinX,
                        headerPinY,
                        headerWidth,
                        headerHeight,
                        dateText,
                        "Arial",
                        "#000000",
                        0.2); // 0.2 inches ≈ 14.4 points

                    // Optional: ensure the shape is not deleted.
                    if (headerShape.Del == BOOL.True)
                    {
                        headerShape.Del = BOOL.False;
                    }
                }

                // Save the modified diagram as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }