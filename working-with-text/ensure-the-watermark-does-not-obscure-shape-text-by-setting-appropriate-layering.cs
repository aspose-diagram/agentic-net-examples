using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // Add a sample shape (Rectangle) with some text
                // -------------------------------------------------
                // Define position and size for the rectangle (in inches)
                double rectPinX = 4.0;
                double rectPinY = 5.0;
                double rectWidth = 3.0;
                double rectHeight = 2.0;

                // Add the rectangle shape using the built‑in master name "Rectangle"
                long rectShapeId = page.AddShape(rectPinX, rectPinY, rectWidth, rectHeight, "Rectangle");
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Set the shape's text
                rectShape.Text.Value.Clear();
                rectShape.Text.Value.Add(new Txt("Sample Text"));
                // Bring the rectangle to the front so it appears above the watermark
                rectShape.BringToFront();

                // -------------------------------------------------
                // Add a full‑page watermark
                // -------------------------------------------------
                // Retrieve page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Position the watermark at the center of the page
                double watermarkPinX = pageWidth / 2.0;
                double watermarkPinY = pageHeight / 2.0;

                // Add the watermark text shape (font size is in inches; 0.5 inches ≈ 36 points)
                Shape watermarkShape = page.AddText(
                    watermarkPinX,          // pinX (center)
                    watermarkPinY,          // pinY (center)
                    pageWidth,              // width (cover full page)
                    pageHeight,             // height (cover full page)
                    "CONFIDENTIAL",         // text
                    "Arial",                // font name
                    "#CCCCCC",              // font color (light gray)
                    0.5                     // font size in inches
                );

                // Send the watermark to the back so it does not obscure other shapes
                watermarkShape.SendToBack();

                // -------------------------------------------------
                // Save the diagram to a VSDX file
                // -------------------------------------------------
                string outputPath = "WatermarkedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'. Watermark placed behind other shapes.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }