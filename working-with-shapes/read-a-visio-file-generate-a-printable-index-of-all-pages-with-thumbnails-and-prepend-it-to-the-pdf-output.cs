using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate input arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioIndexPdf <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPdfPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new page that will serve as the index
            Page indexPage = new Page();
            indexPage.Name = "Index";
            indexPage.NameU = "Index";

            // Add the index page to the diagram and move it to the first position
            diagram.Pages.Add(indexPage);
            indexPage.MoveTo(0); // Position at the beginning

            // Layout parameters for thumbnails and text
            double startX = 0.5; // inches from left
            double startY = 0.5; // inches from top
            double thumbWidth = 2.0; // inches
            double thumbHeight = 2.0; // inches
            double verticalSpacing = 0.3; // inches between entries
            double pageWidth = diagram.Pages[0].PageSheet.PageProps.PageWidth.Value;
            double pageHeight = diagram.Pages[0].PageSheet.PageProps.PageHeight.Value;

            // Prepare a list of original pages (skip the newly added index page)
            List<Page> originalPages = new List<Page>();
            for (int i = 1; i < diagram.Pages.Count; i++)
            {
                originalPages.Add(diagram.Pages[i]);
            }

            // Iterate through each original page to create an entry in the index
            double currentY = startY;
            int pageNumber = 1;
            foreach (Page page in originalPages)
            {
                // ----- Generate thumbnail image for the current page -----
                // Save the page as a PNG image into a memory stream
                using (MemoryStream thumbStream = new MemoryStream())
                {
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = pageNumber; // 1‑based page index in the original diagram
                    imgOptions.PageCount = 1;
                    imgOptions.ExportHiddenPage = false;
                    diagram.Save(thumbStream, imgOptions);
                    thumbStream.Position = 0;

                    // ----- Insert a placeholder rectangle for the thumbnail -----
                    // (In a full implementation you would embed the image data into the shape.
                    //  Here we add a simple rectangle as a visual placeholder.)
                    long shapeId = indexPage.AddShape(startX, currentY, thumbWidth, thumbHeight, "Rectangle", false);
                    Shape thumbPlaceholder = indexPage.Shapes.GetShape(shapeId);
                    // Optional: set a light gray fill to indicate a thumbnail area
                    thumbPlaceholder.Fill.FillForegnd.Value = "#D3D3D3";

                    // ----- Add page title text next to the thumbnail -----
                    double textX = startX + thumbWidth + 0.2;
                    double textY = currentY + thumbHeight / 2.0;
                    double textWidth = pageWidth - textX - 0.5;
                    double textHeight = 0.3; // small height for a single line of text

                    long textShapeId = indexPage.AddShape(textX, textY, textWidth, textHeight, "Rectangle", false);
                    Shape textShape = indexPage.Shapes.GetShape(textShapeId);
                    // Clear any default text and add our custom label
                    textShape.Text.Value.Clear();
                    string label = $"Page {pageNumber}: {page.Name}";
                    textShape.Text.Value.Add(new Txt(label));
                    // Center the text vertically
                    textShape.TextXForm.TxtLocPinY.Value = 0.5;
                    textShape.TextXForm.TxtPinY.Value = 0.0;
                }

                // Move to the next line for the following entry
                currentY += thumbHeight + verticalSpacing;
                pageNumber++;
            }

            // ----- Save the final diagram as a PDF, with the index page first -----
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // fallback font
            pdfOptions.ExportHiddenPage = false;
            // Ensure the format tracker is set (required when using Aspose.Diagram saving)
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine($"PDF with index page saved to: {outputPdfPath}");
        }
    }