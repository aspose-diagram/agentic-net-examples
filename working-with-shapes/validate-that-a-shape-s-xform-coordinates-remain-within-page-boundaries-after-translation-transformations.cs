using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Translation offsets (in inches)
                double offsetX = 1.0;   // move right by 1 inch
                double offsetY = 0.5;   // move up by 0.5 inch

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply translation
                        shape.Move(offsetX, offsetY);

                        // Retrieve shape geometry
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate shape bounds
                        double left = pinX - (width / 2.0);
                        double right = pinX + (width / 2.0);
                        double top = pinY - (height / 2.0);
                        double bottom = pinY + (height / 2.0);

                        // Validate horizontal bounds
                        if (left < 0.0 || right > pageWidth)
                        {
                            string message = $"Shape ID {shape.ID} on page '{page.Name}' exceeds horizontal boundaries after translation. Left={left}, Right={right}, PageWidth={pageWidth}.";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }

                        // Validate vertical bounds
                        if (top < 0.0 || bottom > pageHeight)
                        {
                            string message = $"Shape ID {shape.ID} on page '{page.Name}' exceeds vertical boundaries after translation. Top={top}, Bottom={bottom}, PageHeight={pageHeight}.";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }
                    }
                }

                // Save the diagram (using DiagramSaveOptions to demonstrate saving)
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine("Validation completed successfully. Diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }