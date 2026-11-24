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
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Optional: output directory (second argument) or use current directory
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();

        // Guard: ensure the output directory exists (create if necessary)
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate over each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Retrieve the current page
                Page page = diagram.Pages[pageIndex];

                // Prepare PDF file name for the current page
                string pdfFileName = $"Page_{pageIndex + 1}.pdf";
                string pdfPath = Path.Combine(outputDir, pdfFileName);

                // Configure PDF save options to export only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageIndex = pageIndex;   // zero‑based index of the page to start saving
                pdfOptions.PageCount = 1;           // save only one page
                pdfOptions.ExportHiddenPage = false;

                // Save the single page to PDF
                diagram.Save(pdfPath, pdfOptions);

                // After saving, open the PDF with Aspose.Pdf to add bookmarks
                // Fully qualified types are used to avoid namespace conflicts
                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath);

                // Ensure the PDF has at least one page before adding bookmarks
                if (pdfDoc.Pages.Count > 0)
                {
                    // Iterate over all shapes on the current Visio page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Create a bookmark (outline item) for the shape
                        Aspose.Pdf.OutlineItemCollection bookmark = new Aspose.Pdf.OutlineItemCollection(pdfDoc.Outlines);
                        // Use the shape's name as the bookmark title; fallback to shape ID if name is empty
                        string title = !string.IsNullOrEmpty(shape.Name) ? shape.Name : $"Shape_{shape.ID}";
                        bookmark.Title = title;

                        // Set the action to go to the first (and only) page of this PDF
                        // The page number in GoToAction is 1‑based
                        bookmark.Action = new Aspose.Pdf.Annotations.GoToAction(pdfDoc.Pages[1]);

                        // Add the bookmark to the document's outline collection
                        pdfDoc.Outlines.Add(bookmark);
                    }

                    // Save the PDF with the added bookmarks
                    pdfDoc.Save(pdfPath);
                }
                else
                {
                    Console.Error.WriteLine($"Warning: PDF '{pdfPath}' contains no pages; bookmarks were not added.");
                }
            }

            Console.WriteLine("Batch conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}