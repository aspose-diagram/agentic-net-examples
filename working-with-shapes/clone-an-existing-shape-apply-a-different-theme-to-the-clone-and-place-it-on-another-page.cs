using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the source diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page as the source page
                Page sourcePage = diagram.Pages[0];

                // Ensure the source page has at least one shape
                if (sourcePage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The source page contains no shapes to clone.");
                    return;
                }

                // Get the first shape on the source page
                Shape sourceShape = sourcePage.Shapes[0];

                // Create a new shape instance and copy the source shape's data into it
                Shape clonedShape = new Shape();
                clonedShape.Copy(sourceShape);

                // Optionally offset the cloned shape's position to avoid exact overlap
                clonedShape.XForm.PinX.Value = sourceShape.XForm.PinX.Value + 2.0;
                clonedShape.XForm.PinY.Value = sourceShape.XForm.PinY.Value + 2.0;

                // Apply a different preset theme to the cloned shape
                clonedShape.PresetTheme = PresetThemeValue.Bubble;
                clonedShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Determine the target page (second page if exists, otherwise create a new one)
                Page targetPage;
                if (diagram.Pages.Count > 1)
                {
                    targetPage = diagram.Pages[1];
                }
                else
                {
                    // Find the maximum existing page ID
                    int maxPageId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxPageId)
                            maxPageId = p.ID;
                    }

                    // Create a new page with a unique ID and a name
                    Page newPage = new Page();
                    newPage.ID = maxPageId + 1;
                    newPage.Name = "ClonedPage";

                    // Add the new page to the diagram
                    diagram.Pages.Add(newPage);
                    targetPage = newPage;
                }

                // Add the cloned shape to the target page
                targetPage.Shapes.Add(clonedShape);

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}