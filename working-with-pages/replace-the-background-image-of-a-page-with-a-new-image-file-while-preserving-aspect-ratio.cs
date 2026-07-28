using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file, new background image, and output file paths
                string diagramPath = "input.vsdx";
                string imagePath = "background.png";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Get the first page (adjust index if needed)
                    Page page = diagram.Pages[0];

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Load the image to obtain its size and resolution
                    double imgInchesWidth, imgInchesHeight;
                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(imgStream))
                    {
                        // Convert pixel dimensions to inches using DPI
                        imgInchesWidth = img.Width / img.HorizontalResolution;
                        imgInchesHeight = img.Height / img.VerticalResolution;
                    }

                    // Calculate scaling factor to fit the image within the page while preserving aspect ratio
                    double widthScale = pageWidth / imgInchesWidth;
                    double heightScale = pageHeight / imgInchesHeight;
                    double scale = Math.Min(widthScale, heightScale);

                    double finalImgWidth = imgInchesWidth * scale;
                    double finalImgHeight = imgInchesHeight * scale;

                    // Center the image on the page (PinX, PinY represent the shape's center)
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Insert the image as a shape on the page
                    long shapeId;
                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        shapeId = page.AddShape(pinX, pinY, finalImgWidth, finalImgHeight, imgStream);
                    }

                    // Retrieve the shape to apply additional properties
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Send the image shape to the back so it appears as a background
                    page.SendToBack(shapeId);

                    // Make the background non‑selectable
                    bgShape.Protection.LockSelect.Value = BOOL.True;

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Background image replaced and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }