using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input VSDX path and output VSDX path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramBackgroundSimplifier <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Process each page
            foreach (Page page in diagram.Pages)
            {
                // Get the background page linked to this foreground page (if any)
                Page backgroundPage = page.BackPage;
                if (backgroundPage == null)
                    continue; // No background page to process

                // Determine page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Mark existing foreign (image) shapes for deletion
                foreach (Shape shape in backgroundPage.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign)
                    {
                        shape.Del = BOOL.True;
                    }
                }

                // Add a solid color rectangle covering the entire page
                double rectPinX = pageWidth / 2.0;
                double rectPinY = pageHeight / 2.0;
                long rectId = backgroundPage.DrawRectangle(rectPinX, rectPinY, pageWidth, pageHeight);
                Shape rectShape = backgroundPage.Shapes.GetShape(rectId);

                // Set solid fill (pattern 1) and desired color (e.g., light gray)
                rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray color

                // Remove outline stroke
                rectShape.Line.LinePattern.Value = 0;               // No line

                // Ensure the rectangle stays behind other content
                rectShape.SendToBack();
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }