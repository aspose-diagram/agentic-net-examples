using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ThumbnailGenerator
{
    static void Main()
    {
        try
        {

            // Assume solutionXml contains the diagram XML (VSDX) as a string.
            string solutionXml = File.ReadAllText("diagram.xml"); // replace with actual source

            // Load the diagram from the XML string using a memory stream.
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(solutionXml)))
            {
                Diagram diagram = new Diagram(ms);

                // Configure image save options for a low‑resolution thumbnail.
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Render only the first page (0‑based index).
                    PageIndex = 0,
                    // Set a low DPI to reduce size (e.g., 72 DPI).
                    Resolution = 72,
                    // Optionally scale down further if needed.
                    Scale = 0.5f
                };

                // Save the rendered thumbnail to a file.
                diagram.Save("thumbnail.png", imgOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
