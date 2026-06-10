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

            // Obtain a stream that represents the network source.
            // In a real scenario this would be a NetworkStream; here we use a MemoryStream for illustration.
            using (Stream networkStream = GetNetworkStream())
            {
                // Load the Visio diagram from the stream.
                Diagram diagram = new Diagram(networkStream);

                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                // Access a specific page (e.g., the first page).
                Page page = diagram.Pages[0];

                // Example modification: change page dimensions.
                // Width and height are in inches.
                page.PageSheet.PageProps.PageWidth.Value = 11.0;   // 11 inches
                page.PageSheet.PageProps.PageHeight.Value = 8.5;  // 8.5 inches

                // Reset the stream to the beginning and truncate it before saving.
                if (!networkStream.CanSeek)
                    throw new Exception("The provided stream must support seeking.");

                networkStream.Position = 0;
                networkStream.SetLength(0);

                // Save the modified diagram back to the same stream in VSDX format.
                diagram.Save(networkStream, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder method to simulate obtaining a network stream.
    // Replace this with actual network stream acquisition logic.
    private static Stream GetNetworkStream()
    {
        // For demonstration, load a local file into a MemoryStream.
        // Ensure the file path points to a valid Visio file when used in production.
        const string sampleFilePath = "sample.vsdx";
        if (!File.Exists(sampleFilePath))
            throw new FileNotFoundException($"Sample file not found: {sampleFilePath}");

        byte[] fileBytes = File.ReadAllBytes(sampleFilePath);
        return new MemoryStream(fileBytes);
    }
}
