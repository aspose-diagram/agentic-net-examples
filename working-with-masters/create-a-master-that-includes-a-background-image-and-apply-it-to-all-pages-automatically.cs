using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one foreground page
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Use the first existing page to obtain page size
                Page firstPage = diagram.Pages[0];
                double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

                // -------------------------------------------------
                // 1. Create a background page
                // -------------------------------------------------
                Page backgroundPage = new Page();
                backgroundPage.Background = BOOL.True;                     // Mark as background page
                backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;
                diagram.Pages.Add(backgroundPage);                         // Add to diagram

                // -------------------------------------------------
                // 2. Insert the background image onto the background page
                // -------------------------------------------------
                // Adjust the path to your image file as needed
                const string imagePath = "background.png";

                if (!File.Exists(imagePath))
                    throw new FileNotFoundException($"Background image not found: {imagePath}");

                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // Place the image to cover the whole page
                    long shapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, imgStream);
                    Shape bgShape = backgroundPage.Shapes.GetShape((int)shapeId);

                    // Ensure the shape is sent to back and locked from selection
                    bgShape.SendToBack();
                    bgShape.Protection.LockSelect.Value = BOOL.True;

                    // Set a solid fill pattern (1 = solid) so the image is visible
                    bgShape.Fill.FillPattern.Value = 1;
                }

                // -------------------------------------------------
                // 3. Apply the background page to all foreground pages
                // -------------------------------------------------
                foreach (Page pg in diagram.Pages)
                {
                    // Skip the background page itself
                    if (pg.Background == BOOL.True)
                        continue;

                    pg.BackPage = backgroundPage;
                }

                // -------------------------------------------------
                // 4. Save the diagram
                // -------------------------------------------------
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
