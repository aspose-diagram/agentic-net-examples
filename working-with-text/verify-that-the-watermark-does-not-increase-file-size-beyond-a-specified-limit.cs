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

                // Paths to the original and the watermarked diagram files
                string inputPath = "input.vsdx";
                string outputPath = "output_with_watermark.vsdx";

                // Maximum allowed increase in file size (in bytes)
                long maxSizeIncrease = 500_000; // example: 500 KB

                // Load the original diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Access the first page (you can iterate if needed)
                    Page page = diagram.Pages[0];

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add a full‑page watermark text (centered)
                    // Font size is specified in inches (0.5 inches ≈ 36 points)
                    page.AddText(
                        pinX: pageWidth / 2,          // center X
                        pinY: pageHeight / 2,         // center Y
                        width: pageWidth,             // full width
                        height: pageHeight,           // full height
                        text: "CONFIDENTIAL",
                        fontName: "Arial",
                        fontColor: "#CCCCCC",         // light gray
                        size: 0.5                     // font size in inches
                    );

                    // Save the diagram with the watermark
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                // Compare file sizes
                long originalSize = new FileInfo(inputPath).Length;
                long watermarkedSize = new FileInfo(outputPath).Length;
                long sizeDifference = watermarkedSize - originalSize;

                // Verify the size increase does not exceed the limit
                if (sizeDifference > maxSizeIncrease)
                {
                    throw new Exception($"Watermark increased file size by {sizeDifference} bytes, which exceeds the allowed limit of {maxSizeIncrease} bytes.");
                }
                else
                {
                    Console.WriteLine($"Watermark added successfully. Size increase: {sizeDifference} bytes (within the allowed limit).");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }