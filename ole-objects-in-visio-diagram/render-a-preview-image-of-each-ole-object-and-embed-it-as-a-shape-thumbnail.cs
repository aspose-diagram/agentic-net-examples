using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: verify output folder exists (create if missing)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE (foreign) objects via ForeignData
                    if (shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Build a file name for the preview image
                        string imageFileName = $"OleShape_{shape.ID}.png";
                        string imagePath = Path.Combine(outputFolder, imageFileName);

                        // Export the OLE shape to a PNG image
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        shape.ToImage(imagePath, imgOptions);

                        // Embed the preview as a thumbnail shape next to the original OLE shape
                        // Use the same dimensions as the original shape for consistency
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Offset the thumbnail slightly to avoid overlap
                        double offsetX = width + 0.5; // half-inch offset (Visio units are inches)
                        double thumbnailPinX = pinX + offsetX;
                        double thumbnailPinY = pinY;

                        // Open the generated PNG file as a stream
                        using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            // Add a picture shape using the image stream
                            long thumbnailShapeId = page.AddShape(thumbnailPinX, thumbnailPinY, width, height, imgStream);
                            // Retrieve the newly added shape to set a name (optional)
                            Shape thumbnailShape = page.Shapes.GetShape(thumbnailShapeId);
                            if (thumbnailShape != null)
                            {
                                thumbnailShape.Name = $"Thumbnail_{shape.ID}";
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the output folder
            string outputDiagramPath = Path.Combine(outputFolder, "Diagram_With_Thumbnails.vsdx");
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing completed. Output saved to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}