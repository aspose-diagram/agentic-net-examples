using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // -----------------------------------------------------------------
                // 1. Add a Table of Contents (TOC) page
                // -----------------------------------------------------------------
                // Determine the next available page ID
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create the TOC page and assign a unique ID and name
                Page tocPage = new Page();
                tocPage.ID = maxPageId + 1;
                tocPage.Name = "Table of Contents";
                tocPage.NameU = "Table of Contents";

                // Add the TOC page to the diagram
                diagram.Pages.Add(tocPage);

                // -----------------------------------------------------------------
                // 2. Populate the TOC page with entries linking to each page
                // -----------------------------------------------------------------
                double startX = 1.0;      // Horizontal position (in inches)
                double startY = 1.0;      // Initial vertical position (in inches)
                double entryWidth = 5.0;  // Width of the text shape (in inches)
                double entryHeight = 0.4; // Height of the text shape (in inches)
                double verticalSpacing = 0.6; // Space between entries (in inches)

                foreach (Page targetPage in diagram.Pages)
                {
                    // Skip the TOC page itself
                    if (targetPage == tocPage)
                        continue;

                    // Add a text shape on the TOC page displaying the target page name
                    Shape entryShape = tocPage.AddText(startX, startY, entryWidth, entryHeight, targetPage.Name);

                    // Create a hyperlink that points to the target page (internal link)
                    Hyperlink link = new Hyperlink();
                    // SubAddress uses the universal name of the target page
                    link.SubAddress.Value = targetPage.NameU;
                    link.Description.Value = $"Navigate to page \"{targetPage.Name}\"";

                    // Attach the hyperlink to the text shape
                    entryShape.Hyperlinks.Add(link);

                    // Move down for the next entry
                    startY += verticalSpacing;
                }

                // -----------------------------------------------------------------
                // 3. Save the diagram (including the TOC page) as VSDX
                // -----------------------------------------------------------------
                diagram.Save("DiagramWithTOC.vsdx", SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram created with a Table of Contents page.");
        }
    }