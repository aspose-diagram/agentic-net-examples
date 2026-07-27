using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for the source Visio file and the background image.
                string diagramPath = "input.vsdx";
                string imagePath = "background.png";
                string outputPath = "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Process each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the center position for the background shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Insert the image as a shape that spans the full page.
                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        // AddShape overload that accepts an image stream.
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                        // Retrieve the created shape.
                        Shape bgShape = page.Shapes.GetShape(shapeId);

                        // Send the shape to the back so it appears behind other content.
                        bgShape.SendToBack();

                        // Make the background non‑selectable.
                        bgShape.Protection.LockSelect.Value = BOOL.True;

                        // Set the fill pattern to a picture texture (pattern value 25).
                        bgShape.Fill.FillPattern.Value = 25;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up.
                diagram.Dispose();

                Console.WriteLine("Background image tiled and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }