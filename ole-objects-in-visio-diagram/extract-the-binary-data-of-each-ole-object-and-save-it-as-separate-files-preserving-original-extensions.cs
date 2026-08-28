using System;
using System.IO;
using Aspose.Diagram;

class ExtractOleObjects
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Output folder for extracted OLE files
            string outputFolder = "ExtractedOleObjects";
            Directory.CreateDirectory(outputFolder);

            int oleCounter = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    ForeignData foreignData = shape.ForeignData;
                    if (foreignData == null) continue;

                    // Embedded OLE object data
                    byte[] oleBytes = foreignData.ObjectData;
                    if (oleBytes == null || oleBytes.Length == 0) continue;

                    // Determine file extension
                    string sourceName = foreignData.ObjectSourceFullName;
                    string extension = ".bin"; // fallback

                    if (!string.IsNullOrEmpty(sourceName))
                    {
                        extension = Path.GetExtension(sourceName);
                        if (string.IsNullOrEmpty(extension))
                            extension = ".bin";
                    }

                    // Build output file name
                    string fileName = $"OleObject_{oleCounter}{extension}";
                    string filePath = Path.Combine(outputFolder, fileName);

                    // Save the binary data to file
                    File.WriteAllBytes(filePath, oleBytes);
                    Console.WriteLine($"Extracted OLE object to: {filePath}");

                    oleCounter++;
                }
            }

            // Optionally, save the diagram unchanged (demonstrating use of Save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
