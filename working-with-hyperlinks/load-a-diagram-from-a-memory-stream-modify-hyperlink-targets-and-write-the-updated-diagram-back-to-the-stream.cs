using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    // Updates all hyperlink addresses in the diagram loaded from the given memory stream.
    // The stream is reset and the modified diagram is written back into the same stream.
    public static void UpdateHyperlinksInStream(MemoryStream diagramStream)
    {
        // Load the diagram from the provided stream.
        Diagram diagram = new Diagram(diagramStream);

        // Iterate through each page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a Hyperlinks collection.
                if (shape.Hyperlinks != null)
                {
                    // Iterate through each hyperlink attached to the shape.
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Example modification: replace occurrences of "old.com" with "new.com".
                        if (!string.IsNullOrEmpty(link.Address.Value) && link.Address.Value.Contains("old.com"))
                        {
                            link.Address.Value = link.Address.Value.Replace("old.com", "new.com");
                        }
                    }
                }
            }
        }

        // Prepare the stream for writing the updated diagram.
        diagramStream.Position = 0;
        diagramStream.SetLength(0);

        // Save the modified diagram back into the stream using VSDX format.
        diagram.Save(diagramStream, SaveFileFormat.Vsdx);

        // Reset position so the caller can read from the beginning.
        diagramStream.Position = 0;
    }

    // Example entry point demonstrating usage.
    public static void Main()
    {
        try
        {

            // Load an existing Visio file into a memory stream.
            string inputPath = "input.vsdx";
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                // Update hyperlinks within the diagram.
                UpdateHyperlinksInStream(ms);

                // Write the updated diagram to a new file.
                string outputPath = "output.vsdx";
                File.WriteAllBytes(outputPath, ms.ToArray());

                Console.WriteLine("Hyperlink update completed and saved to " + outputPath);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
