using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramTocGenerator <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the existing diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine the maximum existing page ID to assign a new unique ID
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Create a new page for the Table of Contents
                Page tocPage = new Page(maxPageId + 1);
                tocPage.Name = "Table of Contents";
                tocPage.NameU = "Table of Contents";

                // Copy page size from the first existing page (if any)
                if (diagram.Pages.Count > 0)
                {
                    Page referencePage = diagram.Pages[0];
                    tocPage.PageSheet.PageProps.PageWidth.Value = referencePage.PageSheet.PageProps.PageWidth.Value;
                    tocPage.PageSheet.PageProps.PageHeight.Value = referencePage.PageSheet.PageProps.PageHeight.Value;
                }

                // Add the TOC page to the diagram
                diagram.Pages.Add(tocPage);

                // Layout parameters for TOC entries
                double startX = 1.0; // inches from left
                double startY = 1.0; // inches from top
                double entryHeight = 0.3; // height of each text shape
                double entryWidth = 5.0; // width of each text shape
                double verticalSpacing = 0.4; // space between entries

                double currentY = startY;

                // Iterate over all pages except the newly added TOC page
                foreach (Page page in diagram.Pages)
                {
                    // Skip the TOC page itself
                    if (page.ID == tocPage.ID)
                        continue;

                    // Use the page's Name as the displayed title
                    string pageTitle = page.Name;

                    // Add a text shape on the TOC page
                    Shape tocEntry = tocPage.AddText(startX, currentY, entryWidth, entryHeight, pageTitle);

                    // Create a hyperlink that points to the target page (internal link)
                    Hyperlink link = new Hyperlink();
                    // SubAddress refers to the target page's universal name
                    link.SubAddress.Value = page.NameU;
                    // Optional description (tooltip)
                    link.Description.Value = $"Go to page \"{pageTitle}\"";

                    // Add the hyperlink to the shape
                    tocEntry.Hyperlinks.Add(link);

                    // Move to the next vertical position
                    currentY += entryHeight + verticalSpacing;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Table of Contents page created and diagram saved successfully.");
        }
    }