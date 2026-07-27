using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Detect the file format (optional, uses the provided FileFormatUtil)
            var formatInfo = FileFormatUtil.DetectFileFormat(filePath);

            // Load the diagram using the detected format via LoadOptions
            var loadOptions = new LoadOptions(formatInfo.LoadFormat);
            var diagram = new Diagram(filePath, loadOptions);

            // Basic structural validation: ensure the document contains at least one page
            if (diagram.Pages == null || diagram.Pages.Count == 0)
            {
                throw new InvalidOperationException("The loaded diagram does not contain any pages.");
            }

            // Additional validation can be performed using the Validation property if needed
            // Example (if the Validation class provides an IsValid flag):
            // if (diagram.Validation != null && !diagram.Validation.IsValid)
            // {
            //     throw new InvalidOperationException("Diagram validation failed.");
            // }

            // If we reach this point, the diagram is considered structurally valid
            Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
