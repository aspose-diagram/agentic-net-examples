using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths of source Visio files whose first pages will be copied
            var sourceFiles = new List<string>
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx",
                "Diagram3.vsdx"
            };

            // Create an empty master diagram
            using (var master = new Diagram())
            {
                foreach (var filePath in sourceFiles)
                {
                    // Load each source diagram
                    using (var src = new Diagram(filePath))
                    {
                        // Skip if the source diagram has no pages
                        if (src.Pages.Count == 0) continue;

                        // Get the first page from the source diagram
                        var srcPage = src.Pages[0];

                        // Create a new page for the master diagram
                        var newPage = new Page();

                        // Preserve original identifiers and basic properties
                        newPage.ID = srcPage.ID;
                        newPage.Name = srcPage.Name;
                        newPage.NameU = srcPage.NameU;
                        newPage.Background = srcPage.Background;
                        newPage.AssociatedPage = srcPage.AssociatedPage;
                        newPage.BackPage = srcPage.BackPage;
                        newPage.ReviewerID = srcPage.ReviewerID;
                        newPage.ViewCenterX = srcPage.ViewCenterX;
                        newPage.ViewCenterY = srcPage.ViewCenterY;
                        newPage.ViewScale = srcPage.ViewScale;

                        // Copy the complete pagesheet (shapes, styles, etc.)
                        newPage.PageSheet.Copy(srcPage.PageSheet);

                        // Add the prepared page to the master diagram
                        master.Pages.Add(newPage);
                    }
                }

                // Save the resulting master diagram
                master.Save("MasterDiagram.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
