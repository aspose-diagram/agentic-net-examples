using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to modify
            string diagramPath = "input.vsdx";

            // Path to the configuration file (first line = width in inches, second line = height in inches)
            string configPath = "pageSize.config";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read page size from configuration
            double pageWidth;
            double pageHeight;
            try
            {
                string[] lines = File.ReadAllLines(configPath);
                if (lines.Length < 2)
                    throw new Exception("Configuration file must contain at least two lines: width and height.");

                if (!double.TryParse(lines[0], out pageWidth))
                    throw new Exception("Invalid width value in configuration.");

                if (!double.TryParse(lines[1], out pageHeight))
                    throw new Exception("Invalid height value in configuration.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Apply the page size to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = pageWidth;
                page.PageSheet.PageProps.PageHeight.Value = pageHeight;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with new page size ({pageWidth}\" x {pageHeight}\") to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
