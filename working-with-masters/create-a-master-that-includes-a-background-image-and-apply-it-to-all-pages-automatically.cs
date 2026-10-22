using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        // Background image file path
        string imagePath = "background.png";
        // Output Visio file path
        string outputPath = "output.vsdx";

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load image bytes once – they will be stored in the master
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // -----------------------------------------------------------------
                // Create a custom master that contains the background image
                // -----------------------------------------------------------------
                Master bgMaster = new Master();

                // Assign a unique ID and name for the master
                bgMaster.ID = diagram.Masters.Count + 1;
                bgMaster.Name = "BackgroundMaster";
                bgMaster.UniqueID = Guid.NewGuid();
                bgMaster.BaseID = Guid.NewGuid();

                // Create a shape that holds the image as a foreign bitmap
                Shape masterShape = new Shape();

                // Mark the shape as a foreign (image) shape
                masterShape.Type = TypeValue.Foreign;

                // Set foreign data to the loaded image bytes (PNG format)
                masterShape.ForeignData.ForeignType = ForeignType.Bitmap;
                masterShape.ForeignData.CompressionType = CompressionType.PNG;
                masterShape.ForeignData.Value = imageBytes;

                // Initialize size – will be resized per page later
                masterShape.XForm.Width.Value = 1.0;
                masterShape.XForm.Height.Value = 1.0;
                masterShape.XForm.PinX.Value = 0.0;
                masterShape.XForm.PinY.Value = 0.0;

                // Add the shape to the master’s shape collection
                bgMaster.Shapes.Add(masterShape);

                // Add the master to the diagram
                diagram.Masters.Add(bgMaster);

                // -----------------------------------------------------------------
                // Apply the background master to every page in the diagram
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the background shape
                    double centerX = pageWidth / 2.0;
                    double centerY = pageHeight / 2.0;

                    // Add a shape based on the custom master to the current page
                    // The AddShape method returns the shape ID (long)
                    long shapeId = diagram.AddShape(centerX, centerY, bgMaster.Name, page.ID);

                    // Retrieve the shape object from the page
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Resize the shape to cover the entire page
                    bgShape.XForm.Width.Value = pageWidth;
                    bgShape.XForm.Height.Value = pageHeight;
                    bgShape.XForm.PinX.Value = centerX;
                    bgShape.XForm.PinY.Value = centerY;

                    // Send the shape to back so it appears behind other content
                    bgShape.SendToBack();

                    // Make the background non‑selectable
                    bgShape.Protection.LockSelect.Value = BOOL.True;

                    // Optional: remove outline by setting line pattern to none
                    bgShape.Line.LinePattern.Value = 0;

                    // Optional: ensure solid fill (pattern 1) – can be customized as needed
                    bgShape.Fill.FillPattern.Value = 1;
                }

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Background master applied to all pages successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}