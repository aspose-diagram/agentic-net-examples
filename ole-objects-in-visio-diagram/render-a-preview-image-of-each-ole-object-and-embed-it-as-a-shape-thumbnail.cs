using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class OlePreviewGenerator
{
    static void Main()
    {
        try
        {

            // Load existing Visio diagram (uses provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE objects: they have ForeignData with non‑empty ObjectData
                    if (shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Render the shape to an image (PNG) using ToImage overload with stream
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            shape.ToImage(imgStream, imgOptions);

                            // Store the generated preview bytes back into the shape's ForeignData.ImageData
                            shape.ForeignData.ImageData = imgStream.ToArray();
                        }
                    }
                }
            }

            // Save the modified diagram (uses provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
