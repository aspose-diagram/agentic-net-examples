using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // 1. Create an index page and prepend it to the diagram
            // -----------------------------------------------------------------
            Page indexPage = new Page();
            diagram.Pages.Add(indexPage);
            // Move the newly added page to the first position (index 0)
            indexPage.MoveTo(0);
            indexPage.Name = "Index";
            indexPage.NameU = "Index";

            // Variables to position index entries vertically
            double startX = 1.0;   // inches from left
            double startY = 10.0;  // start near top of page
            double entryHeight = 0.5;
            double entryWidth = 6.0;
            double verticalSpacing = 0.6;

            // Counter for index entries
            int entryCount = 0;

            // -----------------------------------------------------------------
            // 2. Iterate through existing pages and shapes to create detail pages
            // -----------------------------------------------------------------
            // Note: We start from page index 1 because index page is at position 0
            for (int pageIdx = 1; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                Page sourcePage = diagram.Pages[pageIdx];

                foreach (Shape sourceShape in sourcePage.Shapes)
                {
                    // Skip deleted shapes
                    if (sourceShape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a master (required for recreation)
                    if (sourceShape.Master == null)
                        continue;

                    // ---------------------------------------------------------
                    // Create a detail page for the current shape
                    // ---------------------------------------------------------
                    Page detailPage = new Page();
                    diagram.Pages.Add(detailPage);
                    // Give the detail page a unique name
                    string detailPageName = $"Shape_{sourceShape.ID}_Page";
                    detailPage.Name = detailPageName;
                    detailPage.NameU = detailPageName;

                    // Retrieve geometry from the source shape
                    double pinX = sourceShape.XForm.PinX.Value;
                    double pinY = sourceShape.XForm.PinY.Value;
                    double width = sourceShape.XForm.Width.Value;
                    double height = sourceShape.XForm.Height.Value;

                    // Add the shape to the detail page using its master name
                    long newShapeId = detailPage.AddShape(pinX, pinY, width, height,
                                                          sourceShape.Master.Name, false);
                    // Retrieve the newly added shape to copy text
                    Shape newShape = detailPage.Shapes.GetShape((int)newShapeId);
                    if (newShape != null)
                    {
                        // Copy plain text from source shape to the new shape
                        string plainText = sourceShape.Text.Value.Text;
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(plainText));
                    }

                    // ---------------------------------------------------------
                    // Add an entry on the index page linking to this detail page
                    // ---------------------------------------------------------
                    double entryY = startY - entryCount * verticalSpacing;
                    Shape indexEntry = indexPage.AddText(startX, entryY, entryWidth, entryHeight,
                                                         $"Shape ID {sourceShape.ID}");
                    // Create a hyperlink that points to the detail page (internal link)
                    Hyperlink link = new Hyperlink();
                    link.Address.Value = "";                     // No external address
                    link.SubAddress.Value = detailPageName;      // Internal page reference
                    link.Description.Value = $"Go to page for shape {sourceShape.ID}";
                    indexEntry.Hyperlinks.Add(link);

                    entryCount++;
                }
            }

            // -----------------------------------------------------------------
            // 3. Save the modified diagram as a PDF with the index page first
            // -----------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}