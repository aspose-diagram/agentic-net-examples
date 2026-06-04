using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the encrypted Visio file.
            string inputPath = "encrypted.vsdx";

            // LoadOptions can be used for format‑specific settings,
            // but this version of Aspose.Diagram does not support a password property.
            LoadOptions loadOptions = new LoadOptions();

            // Load the diagram. Password handling for encrypted files is not available,
            // so the file must be accessible without a password or the password must be
            // supplied by another mechanism outside of LoadOptions.
            Diagram diagram = new Diagram(inputPath, loadOptions);

            // Simple progress tracking while processing each page.
            int totalPages = diagram.Pages.Count;
            int processed = 0;

            foreach (Page page in diagram.Pages)
            {
                processed++;
                Console.WriteLine($"Processing page {processed} of {totalPages}...");
                // Insert conversion or manipulation logic here.
            }

            // Save the processed diagram.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Conversion completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
