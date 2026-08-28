using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the original diagram bytes (replace with your source)
            byte[] diagramBytes = File.ReadAllBytes("input.vsdx");

            // Load diagram from a memory stream
            using (MemoryStream inputStream = new MemoryStream(diagramBytes))
            {
                Diagram diagram = new Diagram(inputStream);

                // Iterate through all pages and shapes to modify hyperlink targets
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Example replacement: change "http://old.com" to "https://new.com"
                                if (link.Address != null && link.Address.Value != null &&
                                    link.Address.Value.Contains("http://old.com"))
                                {
                                    link.Address.Value = link.Address.Value.Replace(
                                        "http://old.com", "https://new.com");
                                }
                            }
                        }
                    }
                }

                // Save the updated diagram back to a new memory stream
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    outputStream.Position = 0; // Reset for further processing if needed

                    // Example: write the updated diagram to a file
                    File.WriteAllBytes("output.vsdx", outputStream.ToArray());
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
