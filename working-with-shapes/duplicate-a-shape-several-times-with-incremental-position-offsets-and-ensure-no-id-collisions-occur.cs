using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx"; // replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                // Work with the first page
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape to duplicate
                if (page.Shapes.Count == 0)
                    throw new Exception("The page contains no shapes to duplicate.");

                // Get the first shape as the source shape
                Shape sourceShape = page.Shapes.GetShape(0);

                // Verify the source shape has a master (required for AddShape)
                if (sourceShape.Master == null)
                    throw new Exception("Source shape does not have an associated master.");

                // Store original position
                double originalPinX = sourceShape.XForm.PinX.Value;
                double originalPinY = sourceShape.XForm.PinY.Value;

                // Store original text (if any)
                string originalText = sourceShape.Text.Value.ToString();

                // Define duplication parameters
                int duplicateCount = 5;          // number of copies to create
                double offsetX = 1.0;            // horizontal offset per copy (in inches)
                double offsetY = 0.5;            // vertical offset per copy (in inches)

                // Loop to create duplicates
                for (int i = 1; i <= duplicateCount; i++)
                {
                    // Calculate new position
                    double newPinX = originalPinX + i * offsetX;
                    double newPinY = originalPinY + i * offsetY;

                    // Add a new shape based on the same master
                    long newShapeId = page.AddShape(newPinX, newPinY, sourceShape.Master.Name);

                    // Retrieve the newly added shape for further customization
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy the text from the source shape
                    if (!string.IsNullOrWhiteSpace(originalText))
                    {
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(originalText));
                    }

                    // Additional property copying can be done here if needed
                    // Example: copy fill color
                    // newShape.Fill.FillForegnd.Value = sourceShape.Fill.FillForegnd.Value;
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx"; // replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }