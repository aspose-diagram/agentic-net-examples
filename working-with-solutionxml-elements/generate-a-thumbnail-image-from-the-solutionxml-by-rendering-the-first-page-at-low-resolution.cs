using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ThumbnailGenerator
{
    static void Main()
    {
        try
        {

            // XML representation of the diagram (SolutionXML)
            string solutionXml = @"<Diagram>...</Diagram>"; // TODO: replace with actual XML content

            // Load the diagram from the XML string using a memory stream
            byte[] xmlBytes = System.Text.Encoding.UTF8.GetBytes(solutionXml);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                Diagram diagram = new Diagram(xmlStream);

                // Configure image save options for a low‑resolution thumbnail
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                imgOptions.PageIndex = 0;          // render the first page (0‑based)
                imgOptions.PageCount = 1;          // only one page
                imgOptions.Resolution = 72;        // low DPI for thumbnail

                // Output thumbnail file path
                string thumbnailPath = "thumbnail.png";

                // Render and save the thumbnail image
                diagram.Save(thumbnailPath, imgOptions);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
