using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <sourcePageName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string sourcePageName = args[1];
        string outputPath = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the source page by name
                Page sourcePage = null;
                foreach (Page p in diagram.Pages)
                {
                    if (p.Name == sourcePageName)
                    {
                        sourcePage = p;
                        break;
                    }
                }

                if (sourcePage == null)
                {
                    Console.Error.WriteLine($"Source page \"{sourcePageName}\" not found in the diagram.");
                    return;
                }

                // Create a new page instance
                Page newPage = new Page();

                // Add the new page to the diagram's page collection
                diagram.Pages.Add(newPage);

                // Copy the entire PageSheet (including shapes, layers, etc.) from the source page
                newPage.Copy(sourcePage);

                // Preserve background settings
                // Copy the Background flag (BOOL) from the source page
                newPage.Background = sourcePage.Background;

                // If the source page references a background page, link the same background page
                if (sourcePage.BackPage != null)
                {
                    newPage.BackPage = sourcePage.BackPage;
                }

                // Optionally give the new page a distinct name
                newPage.Name = sourcePage.Name + "_Copy";

                // Save the modified diagram to the specified output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Page copied successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}