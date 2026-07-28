using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be processed.
            // Replace with the actual file path or pass it as a command‑line argument.
            string filePath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram. The Diagram constructor handles file loading.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the current page width (in inches).
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Increase the width by 10 percent.
                    page.PageSheet.PageProps.PageWidth.Value = currentWidth * 1.10;
                }

                // Overwrite the original file with the updated diagram.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
