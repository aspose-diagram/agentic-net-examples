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

                // Input Visio file and watermark image paths
                string inputVisioPath = "input.vsdx";
                string watermarkImagePath = "watermark.png";
                string outputVisioPath = "output_with_watermark.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the center position for the watermark shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the image as a shape covering the whole page
                    using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        // AddShape returns the shape ID (long)
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);

                        // Retrieve the shape object to modify its properties
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the image shape to the back so it appears behind other shapes
                        watermarkShape.SendToBack();

                        // Make the watermark non‑selectable
                        watermarkShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }