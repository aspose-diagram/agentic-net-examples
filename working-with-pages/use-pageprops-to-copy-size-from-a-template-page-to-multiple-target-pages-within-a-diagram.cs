using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the source diagram (containing the template page) and the output diagram.
                string sourcePath = "templateDiagram.vsdx";
                string outputPath = "updatedDiagram.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(sourcePath);

                // Ensure there is at least one page to act as the template.
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram does not contain any pages.");

                // Use the first page as the template page.
                Aspose.Diagram.Page templatePage = diagram.Pages[0];
                double templateWidth = templatePage.PageSheet.PageProps.PageWidth.Value;
                double templateHeight = templatePage.PageSheet.PageProps.PageHeight.Value;

                // Apply the template size to all other pages.
                for (int i = 1; i < diagram.Pages.Count; i++)
                {
                    Aspose.Diagram.Page targetPage = diagram.Pages[i];
                    targetPage.PageSheet.PageProps.PageWidth.Value = templateWidth;
                    targetPage.PageSheet.PageProps.PageHeight.Value = templateHeight;
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }