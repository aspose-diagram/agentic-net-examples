using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioProcessor
{
    // Loads a Visio diagram from the specified file path and validates its structure.
    public static Diagram LoadAndValidate(string filePath)
    {
        // Detect the file format to ensure correct loading options.
        FileFormatInfo formatInfo = FileFormatUtil.DetectFileFormat(filePath);

        // Create load options based on the detected format.
        LoadOptions loadOptions = new LoadOptions(formatInfo.LoadFormat);

        // Load the diagram using the constructor that accepts a file path and load options.
        Diagram diagram = new Diagram(filePath, loadOptions);

        // Validate that the diagram contains at least one page.
        if (diagram.Pages == null || diagram.Pages.Count == 0)
        {
            throw new InvalidOperationException("The diagram does not contain any pages.");
        }

        // Validate each page has at least one shape.
        foreach (Page page in diagram.Pages)
        {
            if (page.Shapes == null || page.Shapes.Count == 0)
            {
                throw new InvalidOperationException($"Page '{page.Name}' does not contain any shapes.");
            }
        }

        // Additional validation can be performed using the Validation property if needed.
        // Example: check for validation errors (if any are reported).
        // if (diagram.Validation != null && diagram.Validation.HasErrors)
        // {
        //     throw new InvalidOperationException("Diagram validation reported errors.");
        // }

        // If all checks pass, return the loaded diagram.
        return diagram;
    }

    // Example usage.
    static void Main()
    {
        string inputPath = @"C:\Diagrams\sample.vsdx";

        try
        {
            Diagram diagram = LoadAndValidate(inputPath);
            Console.WriteLine("Diagram loaded and validated successfully.");
            // Proceed with conversion or further processing here.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
