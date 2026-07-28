using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Load the diagram with error handling
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through each page and safely access PageProps
        foreach (Page page in diagram.Pages)
        {
            try
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;
                Console.WriteLine($"Page '{page.Name}' size: {width} x {height} inches");

                // Example modification: increase dimensions by 1 inch
                page.PageSheet.PageProps.PageWidth.Value = width + 1;
                page.PageSheet.PageProps.PageHeight.Value = height + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to access PageProps for page '{page.Name}': {ex.Message}");
            }
        }

        // Save the modified diagram with error handling
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
        finally
        {
            diagram.Dispose();
        }
    }
}
