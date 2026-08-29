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

            // Load the source diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (source page)
            Page sourcePage = diagram.Pages[0];

            // Find the first non‑deleted shape on the source page
            Shape sourceShape = null;
            foreach (Shape shape in sourcePage.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    sourceShape = shape;
                    break;
                }
            }

            if (sourceShape == null)
            {
                Console.WriteLine("No shape found to copy.");
                return;
            }

            // Ensure a page named "Page-3" exists; create it if necessary
            Page targetPage = diagram.Pages.GetPage("Page-3");
            if (targetPage == null)
            {
                // Determine the maximum existing page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }

                // Create a new page with a unique ID and the required name
                Page newPage = new Page(maxId + 1);
                newPage.Name = "Page-3";
                diagram.Pages.Add(newPage);
                targetPage = newPage;
            }

            // Verify the source shape has an associated master (required for AddShape)
            if (sourceShape.Master == null)
            {
                Console.WriteLine("Source shape does not have a master; cannot copy.");
                return;
            }

            // Add a new shape to the target page using the same master and geometry
            long newShapeId = targetPage.AddShape(
                sourceShape.XForm.PinX.Value,
                sourceShape.XForm.PinY.Value,
                sourceShape.XForm.Width.Value,
                sourceShape.XForm.Height.Value,
                sourceShape.Master.Name);

            // Retrieve the newly added shape instance
            Shape targetShape = targetPage.Shapes.GetShape(newShapeId);

            // Copy visual formatting and other properties from the source shape
            sourceShape.Copy(targetShape);

            // Preserve paragraphs (formatting) from the source shape
            targetShape.Paras.Clear();
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
