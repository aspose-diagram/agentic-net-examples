using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file and placeholder image path
            string visioPath = "input.vsdx";
            string placeholderImagePath = "placeholder.png";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes that are PowerPoint OLE objects
                List<Shape> shapesToReplace = new List<Shape>();

                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is a foreign (OLE) shape and has foreign data
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                    {
                        // Check if the OLE source is a PowerPoint file
                        string source = shape.ForeignData.ObjectSourceFullName;
                        if (!string.IsNullOrEmpty(source) &&
                            (source.EndsWith(".ppt", StringComparison.OrdinalIgnoreCase) ||
                             source.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase)))
                        {
                            shapesToReplace.Add(shape);
                        }
                    }
                }

                // Replace each identified OLE shape with the placeholder image
                foreach (Shape oleShape in shapesToReplace)
                {
                    // Preserve original geometry
                    double pinX = oleShape.XForm.PinX.Value;
                    double pinY = oleShape.XForm.PinY.Value;
                    double width = oleShape.XForm.Width.Value;
                    double height = oleShape.XForm.Height.Value;

                    // Remove the OLE shape from the page
                    page.Shapes.Remove(oleShape);

                    // Insert the placeholder image at the same location and size
                    using (FileStream imgStream = new FileStream(placeholderImagePath, FileMode.Open, FileAccess.Read))
                    {
                        // AddShape expects center coordinates (PinX, PinY) and dimensions
                        page.AddShape(pinX, pinY, width, height, imgStream);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
