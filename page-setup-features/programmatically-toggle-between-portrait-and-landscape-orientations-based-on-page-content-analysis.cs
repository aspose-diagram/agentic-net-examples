using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths can be passed as command‑line arguments.
                // If not provided, default paths are used.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    bool shouldBeLandscape = false;

                    // Examine each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip connectors (1‑D shapes) – they have no meaningful width/height for orientation.
                        if (shape.OneD)
                            continue;

                        // Retrieve shape dimensions (in inches).
                        double shapeWidth = shape.XForm.Width.Value;
                        double shapeHeight = shape.XForm.Height.Value;

                        // If any shape is wider than it is tall, mark the page for landscape orientation.
                        if (shapeWidth > shapeHeight)
                        {
                            shouldBeLandscape = true;
                            break; // No need to check further shapes on this page.
                        }
                    }

                    // Set page orientation based on the analysis.
                    if (shouldBeLandscape)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    else
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }