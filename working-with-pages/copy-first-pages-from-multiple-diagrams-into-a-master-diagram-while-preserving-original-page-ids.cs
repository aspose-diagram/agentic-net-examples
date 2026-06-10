using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

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
            var masterDiagram = new Diagram();

            foreach (var filePath in sourceFiles)
            {
                // Load the source diagram
                var sourceDiagram = new Diagram(filePath);

                // Get the first page of the source diagram
                var sourcePage = sourceDiagram.Pages[0];

                // Create a new page in the master diagram with the same ID as the source page
                var newPage = new Page(sourcePage.ID);

                // Copy the page sheet (contains shapes, styles, etc.)
                newPage.PageSheet.Copy(sourcePage.PageSheet);

                // Preserve additional page properties
                newPage.Name = sourcePage.Name;
                newPage.NameU = sourcePage.NameU;
                newPage.Background = sourcePage.Background;
                newPage.AssociatedPage = sourcePage.AssociatedPage;
                newPage.BackPage = sourcePage.BackPage;
                newPage.ViewCenterX = sourcePage.ViewCenterX;
                newPage.ViewCenterY = sourcePage.ViewCenterY;
                newPage.ViewScale = sourcePage.ViewScale;

                // Add the new page to the master diagram
                masterDiagram.Pages.Add(newPage);
            }

            // Save the master diagram containing the copied pages
            masterDiagram.Save("MasterDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
