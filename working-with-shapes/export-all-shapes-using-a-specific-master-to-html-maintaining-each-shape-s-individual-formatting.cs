using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file, master name, output folder
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <masterName> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string outputFolder = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists or create it
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and that its name matches the requested master
                        if (shape.Master != null && shape.Master.Name == masterName)
                        {
                            // Prepare a unique file name for each shape export
                            string htmlFileName = $"Shape_{shape.ID}_{Guid.NewGuid():N}.html";
                            string htmlPath = Path.Combine(outputFolder, htmlFileName);

                            // Create default HTML save options
                            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                            // Export the individual shape to HTML while preserving its formatting
                            shape.ToHTML(htmlPath, htmlOptions);

                            Console.WriteLine($"Exported shape ID {shape.ID} to {htmlPath}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}