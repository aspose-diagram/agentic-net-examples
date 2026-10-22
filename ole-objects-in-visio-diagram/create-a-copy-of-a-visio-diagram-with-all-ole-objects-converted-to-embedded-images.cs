using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <sourceVisioPath> <outputVisioPath>");
            return;
        }

        string sourcePath = args[0];
        string outputPath = args[1];

        // Guard: ensure the source file exists.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            Diagram diagram = new Diagram(sourcePath);

            // Iterate through each page in the diagram.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Collect OLE (foreign) shapes first to avoid modifying the collection while iterating.
                List<Aspose.Diagram.Shape> oleShapes = new List<Aspose.Diagram.Shape>();
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify OLE objects: foreign shapes with embedded object data.
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        oleShapes.Add(shape);
                    }
                }

                // Process each identified OLE shape.
                foreach (Aspose.Diagram.Shape oleShape in oleShapes)
                {
                    // Export the OLE shape to a PNG image stored in a temporary file.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Convert the OLE shape to an image.
                        oleShape.ToImage(ms, new ImageSaveOptions(SaveFileFormat.Png));
                        ms.Position = 0;

                        // Write the image bytes to a temporary file.
                        string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                        File.WriteAllBytes(tempImagePath, ms.ToArray());

                        // Preserve the original shape's position and size.
                        double pinX = oleShape.XForm.PinX.Value;
                        double pinY = oleShape.XForm.PinY.Value;
                        double width = oleShape.XForm.Width.Value;
                        double height = oleShape.XForm.Height.Value;

                        // Insert the image as a picture shape using the temporary file.
                        using (FileStream fs = new FileStream(tempImagePath, FileMode.Open, FileAccess.Read))
                        {
                            // AddShape with a FileStream inserts an image.
                            long pictureShapeId = page.AddShape(pinX, pinY, width, height, fs);
                            Aspose.Diagram.Shape pictureShape = page.Shapes.GetShape(pictureShapeId);

                            // Send the new picture to the back so it does not obscure other content.
                            pictureShape.SendToBack();
                        }

                        // Remove the original OLE shape from the page.
                        page.Shapes.Remove(oleShape);

                        // Clean up the temporary image file.
                        try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }
                    }
                }
            }

            // Save the modified diagram as a new file (creates a copy with images instead of OLE objects).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}