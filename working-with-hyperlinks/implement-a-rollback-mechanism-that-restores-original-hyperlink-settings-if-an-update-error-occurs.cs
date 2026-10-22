using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a backup copy for rollback purposes
        string backupPath = inputPath + ".bak";
        try
        {
            File.Copy(inputPath, backupPath, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create backup file: {ex.Message}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to update hyperlinks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Update each hyperlink's address
                        for (int i = 0; i < shape.Hyperlinks.Count; i++)
                        {
                            Hyperlink link = shape.Hyperlinks[i];
                            // Example update: prepend "https://example.com/updated?orig=" to the original address
                            string originalAddress = link.Address?.Value ?? string.Empty;
                            link.Address.Value = $"https://example.com/updated?orig={originalAddress}";
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(inputPath, SaveFileFormat.Vsdx);

            // If everything succeeded, delete the backup
            try
            {
                File.Delete(backupPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Warning: could not delete backup file: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // An error occurred during update; attempt rollback
            Console.Error.WriteLine($"Error during hyperlink update: {ex.Message}");
            try
            {
                // Reload the backup diagram and overwrite the original file
                Diagram backupDiagram = new Diagram(backupPath);
                backupDiagram.Save(inputPath, SaveFileFormat.Vsdx);
                Console.Error.WriteLine("Rollback successful: original hyperlink settings restored.");
            }
            catch (Exception rollbackEx)
            {
                Console.Error.WriteLine($"Rollback failed: {rollbackEx.Message}");
            }
        }
    }
}