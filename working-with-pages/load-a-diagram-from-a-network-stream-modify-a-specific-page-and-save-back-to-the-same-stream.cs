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

            // Simulate a network stream by loading a Visio file into a MemoryStream
            string inputPath = "input.vsdx";
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream networkStream = new MemoryStream())
                {
                    fileStream.CopyTo(networkStream);
                    networkStream.Position = 0; // Reset for processing

                    // Modify the first page (index 0) of the diagram
                    ProcessDiagram(networkStream, 0);

                    // For demonstration, write the updated stream back to a file
                    networkStream.Position = 0;
                    using (FileStream outFile = new FileStream("output.vsdx", FileMode.Create, FileAccess.Write))
                    {
                        networkStream.CopyTo(outFile);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ProcessDiagram(Stream networkStream, int pageIndex)
    {
        // Aspose.Diagram cannot load directly from a stream, so use a temporary file
        string tempFile = Path.GetTempFileName();
        try
        {
            // Write the incoming stream content to the temporary file
            using (FileStream tempFs = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                networkStream.CopyTo(tempFs);
            }

            // Load the diagram from the temporary file
            Diagram diagram = new Diagram(tempFile);

            // Validate the requested page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");

            // Retrieve the specific page
            Page page = diagram.Pages[pageIndex];

            // Example modification: set page size to A4 (8.27 x 11.69 inches)
            page.PageSheet.PageProps.PageWidth.Value = 8.27;
            page.PageSheet.PageProps.PageHeight.Value = 11.69;

            // Prepare the original stream for writing the updated diagram
            networkStream.SetLength(0);
            networkStream.Position = 0;

            // Save the modified diagram back into the same stream using VSDX format
            diagram.Save(networkStream, SaveFileFormat.Vsdx);
        }
        finally
        {
            // Delete the temporary file
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }
}
