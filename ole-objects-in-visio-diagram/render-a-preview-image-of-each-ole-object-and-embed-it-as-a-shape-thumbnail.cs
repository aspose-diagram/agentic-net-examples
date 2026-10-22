using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary).
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Collect OLE shapes first to avoid modifying the collection while iterating.
                List<Aspose.Diagram.Shape> oleShapes = new List<Aspose.Diagram.Shape>();
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify OLE objects via ForeignData and ObjectType.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Ensure there is actual binary data.
                        if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                        {
                            oleShapes.Add(shape);
                        }
                    }
                }

                // Process each OLE shape.
                foreach (Aspose.Diagram.Shape oleShape in oleShapes)
                {
                    try
                    {
                        // Prepare image save options for PNG format.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                        // Render the OLE object to a memory stream.
                        using (MemoryStream ms = new MemoryStream())
                        {
                            oleShape.ToImage(ms, imgOptions);
                            ms.Position = 0;

                            // Save the preview image to a file (used later for insertion).
                            string imageFileName = $"Ole_{oleShape.ID}.png";
                            string imagePath = Path.Combine(outputFolder, imageFileName);
                            File.WriteAllBytes(imagePath, ms.ToArray());

                            // Retrieve original shape geometry.
                            double pinX = oleShape.XForm.PinX.Value;
                            double pinY = oleShape.XForm.PinY.Value;
                            double width = oleShape.XForm.Width.Value;
                            double height = oleShape.XForm.Height.Value;

                            // Insert the preview image as a picture shape.
                            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                            {
                                // AddShape overload that accepts a FileStream creates a picture shape.
                                long pictureShapeId = page.AddShape(pinX, pinY, width, height, fs);
                                // Retrieve the newly added shape (optional, can be used for further tweaks).
                                Aspose.Diagram.Shape pictureShape = page.Shapes.GetShape(pictureShapeId);
                                // No additional properties need to be set; the image is already embedded.
                            }

                            // Hide the original OLE shape by setting its dimensions to zero.
                            oleShape.XForm.Width.Value = 0;
                            oleShape.XForm.Height.Value = 0;
                        }
                    }
                    catch (Exception shapeEx)
                    {
                        Console.Error.WriteLine($"Error processing OLE shape ID {oleShape.ID}: {shapeEx.Message}");
                    }
                }
            }

            // Save the modified diagram.
            string outputDiagramPath = Path.Combine(outputFolder, "Diagram_With_OLE_Thumbnails.vsdx");
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing completed. Modified diagram saved to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}