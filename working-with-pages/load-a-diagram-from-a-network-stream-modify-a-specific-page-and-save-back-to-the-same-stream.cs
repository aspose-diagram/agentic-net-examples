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

            // Obtain a network stream that contains a Visio diagram.
            // In a real scenario this could be a NetworkStream from a socket.
            // Here we use a placeholder method to illustrate.
            Stream networkStream = GetNetworkStream();

            // Load the diagram from the stream.
            // Diagram(Stream) constructor reads the Visio file from the provided stream.
            Diagram diagram = new Diagram(networkStream);

            // Modify a specific page (e.g., the first page).
            // Access pages via diagram.Pages[index] as required.
            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];

                // Example modification: set page size to 11 x 8.5 inches.
                page.PageSheet.PageProps.PageWidth.Value = 11.0;
                page.PageSheet.PageProps.PageHeight.Value = 8.5;
            }

            // Prepare the stream for writing the updated diagram.
            // Reset position to the beginning and truncate existing content.
            if (networkStream.CanSeek)
            {
                networkStream.Position = 0;
                networkStream.SetLength(0);
            }

            // Save the modified diagram back to the same stream.
            // Use the Save(Stream, SaveFileFormat) overload.
            diagram.Save(networkStream, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder method representing acquisition of a network stream.
    // Replace with actual network stream retrieval logic as needed.
    static Stream GetNetworkStream()
    {
        // For demonstration, we create a MemoryStream.
        // In practice, this would be a NetworkStream or other readable/writable stream.
        byte[] dummyData = File.ReadAllBytes("input.vsdx"); // Ensure the file exists.
        return new MemoryStream(dummyData);
    }
}
