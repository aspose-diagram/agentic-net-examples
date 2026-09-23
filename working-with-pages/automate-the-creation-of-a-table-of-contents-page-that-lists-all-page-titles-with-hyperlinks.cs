using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramTocGenerator <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine a new unique page ID
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a new page for the Table of Contents
            Page tocPage = new Page(maxPageId + 1);
            tocPage.Name = "Table of Contents";
            diagram.Pages.Add(tocPage);

            // Page dimensions (in inches)
            double pageWidth = tocPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = tocPage.PageSheet.PageProps.PageHeight.Value;

            // Layout parameters
            double margin = 0.5;               // 0.5 inch margin
            double shapeHeight = 0.3;          // Height of each TOC entry
            double verticalSpacing = 0.1;      // Space between entries
            double currentY = pageHeight - margin - shapeHeight; // Start from top

            // Iterate over all pages except the TOC page itself
            foreach (Page targetPage in diagram.Pages)
            {
                if (targetPage.ID == tocPage.ID)
                    continue; // Skip the TOC page

                // Create a rectangle shape that will hold the page title
                double pinX = margin + (pageWidth - 2 * margin) / 2; // Center horizontally
                long shapeId = tocPage.DrawRectangle(pinX, currentY, pageWidth - 2 * margin, shapeHeight);
                Shape tocShape = tocPage.Shapes.GetShape(shapeId);

                // Set the visible text to the page name
                tocShape.Text.Value.Clear();
                tocShape.Text.Value.Add(new Txt(targetPage.Name));

                // Add a hyperlink that points to the target page
                Hyperlink link = new Hyperlink();
                link.SubAddress.Value = targetPage.Name; // Internal page reference
                link.Description.Value = $"Go to page \"{targetPage.Name}\"";
                tocShape.Hyperlinks.Add(link);

                // Move down for the next entry
                currentY -= (shapeHeight + verticalSpacing);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }