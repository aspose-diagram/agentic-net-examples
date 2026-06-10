using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the folder containing Visio files
        string folderPath = @"C:\VisioFiles";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process all VSDX files in the folder
        string[] files = Directory.GetFiles(folderPath, "*.vsdx");
        foreach (string filePath in files)
        {
            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Ensure the diagram has at least one page
                if (diagram.Pages.Count > 0)
                {
                    // Access the first page
                    Page firstPage = diagram.Pages[0];

                    // Set the page height to 14 inches
                    firstPage.PageSheet.PageProps.PageHeight.Value = 14.0;

                    // Save the diagram back to the original file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                else
                {
                    Console.WriteLine($"No pages found in: {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }
}
