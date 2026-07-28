using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchProcessor
{
    // Custom field value to add to each shape
    private const string CustomFieldValue = "MyCustomField";

    static void Main(string[] args)
    {
        // Directory containing Visio files; adjust as needed
        string directoryPath = @"C:\VisioFiles";

        // Process each Visio file in the directory
        foreach (string filePath in Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly))
        {
            // Filter supported Visio extensions
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsd" && extension != ".vsdx" && extension != ".vdx")
                continue;

            // Load the diagram using the provided constructor
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Add or update a custom field (using Data1 as an example)
                    shape.Data1 = CustomFieldValue;
                }
            }

            // Save the modified diagram back to the original file using the provided Save method
            // Preserve the original format based on file extension
            SaveFileFormat saveFormat = GetSaveFormatFromExtension(extension);
            diagram.Save(filePath, saveFormat);
        }
    }

    // Helper to map file extension to SaveFileFormat enumeration
    private static SaveFileFormat GetSaveFormatFromExtension(string extension)
    {
        switch (extension)
        {
            case ".vsd":
                return SaveFileFormat.Vsdx; // Save as VSDX to retain compatibility
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vdx":
                return SaveFileFormat.Vdx;
            default:
                return SaveFileFormat.Vsdx;
        }
    }
}
