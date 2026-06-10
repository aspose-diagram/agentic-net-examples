using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Watermark settings
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Arial";
                string fontColor = "#808080"; // Gray color in hex
                double fontSizePoints = 72; // 72 points = 1 inch
                double opacityPercent = 50; // 0-100, 50% transparent
                double rotationDegrees = 45; // Rotation angle

                // Convert font size from points to inches (Aspose.Diagram expects inches)
                double fontSizeInches = fontSizePoints / 72.0;
                // Convert rotation to radians (Aspose.Diagram XForm.Angle uses radians)
                double rotationRadians = rotationDegrees * Math.PI / 180.0;

                // Apply watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add a full‑page text shape as the watermark
                    Shape watermarkShape = page.AddText(
                        pinX,               // pinX (center)
                        pinY,               // pinY (center)
                        pageWidth,          // width (covers full page)
                        pageHeight,         // height (covers full page)
                        watermarkText,      // text
                        fontName,           // font name
                        fontColor,          // font color (hex)
                        fontSizeInches);    // font size in inches

                    // Set transparency (percentage, 0 = opaque, 100 = fully transparent)
                    watermarkShape.Fill.FillForegndTrans.Value = opacityPercent;

                    // Rotate the watermark shape
                    watermarkShape.XForm.Angle.Value = rotationRadians;
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }