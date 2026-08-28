using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioLoader
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = @"C:\Diagrams\sample.vsdx";

            // Detect the file format of the Visio document
            FileFormatInfo formatInfo = FileFormatUtil.DetectFileFormat(filePath);

            // Prepare load options based on the detected format
            LoadOptions loadOptions = new LoadOptions(formatInfo.LoadFormat);

            // Load the diagram using the constructor that accepts a file path and LoadOptions
            Diagram diagram = new Diagram(filePath, loadOptions);

            // Basic validation of the diagram structure
            if (diagram.Pages == null || diagram.Pages.Count == 0)
            {
                throw new InvalidOperationException("The diagram does not contain any pages.");
            }

            // Optional: iterate through pages to ensure each has at least one shape
            foreach (Page page in diagram.Pages)
            {
                if (page.Shapes == null || page.Shapes.Count == 0)
                {
                    Console.WriteLine($"Warning: Page '{page.Name}' contains no shapes.");
                }
            }

            // At this point the diagram is loaded and validated; further conversion logic can follow.
            Console.WriteLine("Diagram loaded and validated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
