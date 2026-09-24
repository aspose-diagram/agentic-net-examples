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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Example modification: set the size of the first page
            Page page = diagram.Pages[0];
            page.PageSheet.PageProps.PageWidth.Value = 11;   // width in inches
            page.PageSheet.PageProps.PageHeight.Value = 8.5; // height in inches

            // Save the modified diagram into a memory stream (VSDX format)
            using (MemoryStream memoryStream = new MemoryStream())
            {
                diagram.Save(memoryStream, SaveFileFormat.Vsdx);
                memoryStream.Position = 0; // reset for further reading

                // Example output: display the size of the generated stream
                Console.WriteLine($"Diagram saved to memory stream. Length = {memoryStream.Length} bytes.");

                // The memoryStream can now be passed to a web service or further processed.
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
