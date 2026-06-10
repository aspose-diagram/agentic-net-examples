using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_with_toc.vsdx";

                // Load the existing diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Create a new page for the Table of Contents
                    Page tocPage = new Page();
                    tocPage.Name = "Table of Contents";
                    diagram.Pages.Add(tocPage);

                    // Reference to the newly added TOC page (last page in collection)
                    Page toc = diagram.Pages[diagram.Pages.Count - 1];

                    // Positioning variables for the list entries
                    double startX = 1.0;      // inches from left
                    double startY = 1.0;      // inches from top
                    double entryHeight = 0.3; // height of each text shape
                    double entryWidth = 3.0;  // width of each text shape
                    double verticalSpacing = 0.5; // space between entries

                    // Iterate over all existing pages except the TOC page itself
                    for (int i = 0; i < diagram.Pages.Count - 1; i++)
                    {
                        Page targetPage = diagram.Pages[i];
                        string pageTitle = targetPage.Name; // Use the page's name as the title

                        // Calculate Y position for this entry
                        double pinY = startY + i * verticalSpacing;

                        // Add a text shape with the page title
                        Shape entryShape = toc.AddText(startX, pinY, entryWidth, entryHeight, pageTitle);

                        // Create a hyperlink that points to the target page within the same document
                        Hyperlink link = new Hyperlink();
                        // SubAddress refers to the internal page name
                        link.SubAddress.Value = targetPage.Name;
                        // Optional tooltip description
                        link.Description.Value = $"Go to page \"{targetPage.Name}\"";

                        // Attach the hyperlink to the shape
                        entryShape.Hyperlinks.Add(link);
                    }

                    // Save the modified diagram with the TOC page
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Table of Contents page created and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }