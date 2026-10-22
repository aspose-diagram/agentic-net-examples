using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, placeholder image path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagram.vsdx> <placeholderImage.png> <outputDiagram.vsdx>");
            return;
        }

        string inputPath = args[0];
        string placeholderImagePath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderImagePath))
        {
            Console.Error.WriteLine($"Placeholder image file not found: {placeholderImagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Collect shapes that need replacement to avoid modifying the collection while iterating
                List<Aspose.Diagram.Shape> shapesToReplace = new List<Aspose.Diagram.Shape>();

                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify foreign (OLE) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && !string.IsNullOrEmpty(shape.ForeignData.ObjectSourceFullName))
                    {
                        string sourceName = shape.ForeignData.ObjectSourceFullName.ToLowerInvariant();

                        // Check if the OLE object is a PowerPoint file
                        if (sourceName.EndsWith(".ppt") || sourceName.EndsWith(".pptx"))
                        {
                            shapesToReplace.Add(shape);
                        }
                    }
                }

                // Replace each identified OLE shape with the placeholder image
                foreach (Aspose.Diagram.Shape oleShape in shapesToReplace)
                {
                    // Preserve position and size using XForm properties
                    double pinX = oleShape.XForm.PinX.Value;
                    double pinY = oleShape.XForm.PinY.Value;
                    double width = oleShape.XForm.Width.Value;
                    double height = oleShape.XForm.Height.Value;

                    // Remove the original OLE shape
                    page.Shapes.Remove(oleShape);

                    // Add a picture shape using the placeholder image
                    using (FileStream imgStream = new FileStream(placeholderImagePath, FileMode.Open, FileAccess.Read))
                    {
                        // The AddShape method with a FileStream inserts an image at the specified location and size
                        page.AddShape(pinX, pinY, width, height, imgStream);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}