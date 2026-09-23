using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Example: assume we have a byte array containing a Visio diagram (VSDX format)
            // In a real scenario this could come from a database, network stream, etc.
            byte[] diagramData = File.ReadAllBytes("input.vsdx");

            // Load the diagram from the memory stream.
            // Aspose.Diagram does not support direct loading from a Stream, so we write the bytes to a temporary file first.
            string tempInputPath = Path.GetTempFileName();
            File.WriteAllBytes(tempInputPath, diagramData);
            Diagram diagram = new Diagram(tempInputPath);

            // Iterate through all pages and shapes to modify hyperlink targets.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Hyperlinks != null)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Set a new address for each hyperlink.
                            // Replace with the desired target URL.
                            link.Address.Value = "https://newexample.com";
                        }
                    }
                }
            }

            // Save the updated diagram back into a memory stream.
            using (MemoryStream outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                // Reset the stream position if it will be read later.
                outputStream.Position = 0;

                // Example: write the updated diagram to a file for verification.
                File.WriteAllBytes("output.vsdx", outputStream.ToArray());
            }

            // Clean up the temporary file.
            File.Delete(tempInputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
