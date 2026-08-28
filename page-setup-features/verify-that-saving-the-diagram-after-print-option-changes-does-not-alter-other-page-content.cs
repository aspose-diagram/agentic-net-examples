using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

namespace DiagramPrintOptionVerification
{
    // Helper class to store essential page content for comparison
    class PageContent
    {
        public List<long> ShapeIds { get; } = new List<long>();
        public Dictionary<long, string> ShapeTexts { get; } = new Dictionary<long, string>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the original diagram
                Diagram originalDiagram = new Diagram(inputPath);

                // Capture original page content
                var originalPagesContent = CaptureDiagramContent(originalDiagram);

                // Modify print options on each page
                foreach (Page page in originalDiagram.Pages)
                {
                    // Set orientation to Landscape
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Set scaling to 50%
                    page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                    page.PageSheet.PrintProps.ScaleY.Value = 0.5;

                    // Enable fit to sheet (print on one page)
                    page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                    page.PageSheet.PrintProps.PagesX.Value = 1;
                    page.PageSheet.PrintProps.PagesY.Value = 1;

                    // Set margins (1/8 inch = 0.125)
                    page.PageSheet.PrintProps.PageTopMargin.Value = 0.125;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = 0.125;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = 0.125;
                    page.PageSheet.PrintProps.PageRightMargin.Value = 0.125;
                }

                // Save the modified diagram
                originalDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Reload the saved diagram for verification
                Diagram savedDiagram = new Diagram(outputPath);
                var savedPagesContent = CaptureDiagramContent(savedDiagram);

                // Verify that page content (shapes and their text) is unchanged
                if (originalPagesContent.Count != savedPagesContent.Count)
                    throw new Exception("Page count mismatch after saving.");

                for (int i = 0; i < originalPagesContent.Count; i++)
                {
                    PageContent original = originalPagesContent[i];
                    PageContent saved = savedPagesContent[i];

                    // Compare shape counts
                    if (original.ShapeIds.Count != saved.ShapeIds.Count)
                        throw new Exception($"Shape count mismatch on page index {i}.");

                    // Compare each shape's text
                    foreach (long shapeId in original.ShapeIds)
                    {
                        if (!saved.ShapeTexts.TryGetValue(shapeId, out string savedText))
                            throw new Exception($"Shape ID {shapeId} missing after save on page index {i}.");

                        string originalText = original.ShapeTexts[shapeId];
                        if (originalText != savedText)
                            throw new Exception($"Text mismatch on shape ID {shapeId} on page index {i}.");
                    }
                }

                Console.WriteLine("Verification succeeded: print option changes did not alter page content.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Captures shape IDs and plain text for each page in the diagram
        private static List<PageContent> CaptureDiagramContent(Diagram diagram)
        {
            var pagesContent = new List<PageContent>();

            foreach (Page page in diagram.Pages)
            {
                var content = new PageContent();

                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    long id = shape.ID;
                    content.ShapeIds.Add(id);

                    // Retrieve plain text; empty string if no text
                    string text = shape.Text.Value.Text ?? string.Empty;
                    content.ShapeTexts[id] = text;
                }

                pagesContent.Add(content);
            }

            return pagesContent;
        }
    }
}