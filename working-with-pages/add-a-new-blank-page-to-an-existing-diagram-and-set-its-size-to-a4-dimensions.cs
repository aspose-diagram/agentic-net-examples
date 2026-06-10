using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Determine the highest existing page ID
                int maxPageId = 0;
                foreach (Page page in diagram.Pages)
                {
                    if (page.ID > maxPageId)
                        maxPageId = page.ID;
                }

                // Create a new blank page with the next ID
                Page newPage = new Page(maxPageId + 1);
                newPage.Name = $"Page-{maxPageId + 1}";

                // Set the page size to A4 dimensions (in inches)
                newPage.PageSheet.PageProps.PageWidth.Value = 8.27;   // A4 width
                newPage.PageSheet.PageProps.PageHeight.Value = 11.69; // A4 height

                // Add the new page to the diagram
                diagram.Pages.Add(newPage);

                // Save the updated diagram back to a Visio file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }