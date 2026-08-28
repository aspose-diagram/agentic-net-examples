using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Directory containing Visio files
        string inputDirectory = @"C:\VisioFiles";

        // Get all Visio files (common extensions)
        string[] visioFiles = Directory.GetFiles(inputDirectory, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                continue; // Skip non‑Visio files

            // Load the diagram using the constructor that accepts a file path
            Diagram diagram = new Diagram(filePath);

            // Iterate through every page and every shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Add a custom field – using the Data1 property as an example
                    shape.Data1 = "MyCustomFieldValue";
                }
            }

            // Save the modified diagram, overwriting the original file
            diagram.Save(filePath, SaveFileFormat.Vsdx);

            // Release resources
            diagram.Dispose();
        }
    }
}
