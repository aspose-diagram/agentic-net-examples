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

            // Path to the background image file (PNG, JPG, etc.)
            string imagePath = "background.png";
            // Output Visio file
            string outputPath = "result.vsdx";

            // Create a new diagram instance
            using (Diagram diagram = new Diagram())
            {
                // Ensure at least one foreground page exists
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Use the first page to obtain page dimensions (in inches)
                Page referencePage = diagram.Pages[0];
                double pageWidth = referencePage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = referencePage.PageSheet.PageProps.PageHeight.Value;

                // Find the highest existing page ID to generate a unique ID for the background page
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }

                // Create a new background page
                Page backgroundPage = new Page(maxId + 1);
                backgroundPage.Name = "Background";
                backgroundPage.Background = BOOL.True;
                // Apply the same dimensions as other pages
                backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
                backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

                // Insert the background image covering the entire page area
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    long shapeId = backgroundPage.AddShape(0, 0, pageWidth, pageHeight, imgStream);
                    Shape bgShape = backgroundPage.Shapes.GetShape(shapeId);
                    // Solid fill (pattern 1) and no outline (pattern 0)
                    bgShape.Fill.FillPattern.Value = 1;
                    bgShape.Line.LinePattern.Value = 0;
                    // Ensure the image stays behind other content and cannot be selected
                    bgShape.SendToBack();
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Assign the background page to every foreground page
                foreach (Page pg in diagram.Pages)
                {
                    if (pg.Background == BOOL.False)
                    {
                        pg.BackPage = backgroundPage;
                    }
                }

                // Save the diagram with the background applied to all pages
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully with background image applied to all pages.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
