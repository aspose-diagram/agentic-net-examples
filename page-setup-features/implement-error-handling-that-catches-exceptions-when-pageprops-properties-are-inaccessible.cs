using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Attempt to read page dimensions; these properties may be inaccessible
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        Console.WriteLine($"Page ID {page.ID}: Width = {width} inches, Height = {height} inches");
                    }
                    catch (Exception ex)
                    {
                        // Handle any exception that occurs while accessing PageProps
                        Console.WriteLine($"Error accessing PageProps for page ID {page.ID}: {ex.Message}");
                    }
                }

                // Save the diagram after processing
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
