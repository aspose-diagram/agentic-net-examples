using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Source page (first page) and shape to copy
            Page sourcePage = diagram.Pages[0];
            Shape sourceShape = sourcePage.Shapes[0]; // first shape on the source page

            // Ensure the target page named "Page-3" exists; create it if necessary
            Page targetPage = diagram.Pages.GetPage("Page-3");
            if (targetPage == null)
            {
                // Determine a new unique page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }

                // Create and add the new page
                Page newPage = new Page(maxId + 1);
                newPage.Name = "Page-3";
                diagram.Pages.Add(newPage);
                targetPage = newPage;
            }

            // Retrieve the master name of the source shape (fallback to a basic master if null)
            string masterName = sourceShape.Master != null ? sourceShape.Master.Name : "Rectangle";

            // Add a new shape on the target page using the same master and position
            long newShapeId = targetPage.AddShape(
                sourceShape.XForm.PinX.Value,
                sourceShape.XForm.PinY.Value,
                masterName,
                false);

            // Retrieve the newly added shape instance
            Shape targetShape = targetPage.Shapes.GetShape(newShapeId);

            // Copy text runs (Txt objects) from source to target shape
            foreach (var item in sourceShape.Text.Value)
            {
                if (item is Txt txt)
                {
                    targetShape.Text.Value.Add(new Txt(txt.Text));
                }
            }

            // Copy paragraph formatting from source to target shape
            foreach (Para para in sourceShape.Paras)
            {
                targetShape.Paras.Add(para);
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
