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

                // Path to the source Visio file (replace with actual path)
                string sourcePath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(sourcePath);

                // Example modification: change the first page size
                Page firstPage = diagram.Pages[0];
                firstPage.PageSheet.PageProps.PageWidth.Value = 11.0;   // width in inches
                firstPage.PageSheet.PageProps.PageHeight.Value = 8.5;   // height in inches

                // Save the modified diagram to a memory stream in VSDX format
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                    // Reset stream position to the beginning for further processing
                    memoryStream.Position = 0;

                    // Example: write the stream length to console
                    Console.WriteLine($"Diagram saved to memory stream. Length: {memoryStream.Length} bytes.");

                    // The memoryStream can now be used for transmission, e.g., sending over a network
                    // byte[] diagramBytes = memoryStream.ToArray();
                    // ... (transmission logic)
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }