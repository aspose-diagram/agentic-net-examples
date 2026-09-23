using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get the diagram file path from command‑line arguments or ask the user.
        string filePath;
        if (args.Length > 0)
        {
            filePath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the Visio file: ");
            filePath = Console.ReadLine()?.Trim() ?? string.Empty;
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("No file path provided. Exiting.");
            return;
        }

        try
        {
            // Load the diagram. The using block ensures resources are released.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through each page and increase its width by 10 %.
                foreach (Page page in diagram.Pages)
                {
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                    page.PageSheet.PageProps.PageWidth.Value = currentWidth * 1.10;
                }

                // Overwrite the original file. Adjust the SaveFileFormat if the source
                // file uses a different Visio format (e.g., Vdx, Vsx, etc.).
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Page widths increased by 10 % and file saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
