using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least two pages
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page to clone.");
                    return;
                }

                // Retrieve the second page (index 1)
                Page sourcePage = diagram.Pages[1];

                // Create a new blank page
                Page clonedPage = new Page();

                // Determine a unique ID for the new page
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }
                clonedPage.ID = maxPageId + 1;

                // Copy the contents of the source page into the new page
                clonedPage.Copy(sourcePage);

                // Rename the cloned page
                clonedPage.Name = "ClonedPage";

                // Add the cloned page to the diagram
                diagram.Pages.Add(clonedPage);

                // ------------------------------------------------------------
                // Create a new stylesheet to apply to the cloned page
                // ------------------------------------------------------------
                StyleSheet newStyle = new StyleSheet();

                // Assign a unique ID for the stylesheet
                newStyle.ID = diagram.StyleSheets.Count + 1;

                // Example style settings: red line color and green fill
                newStyle.Line.LineColor.Value = "#FF0000";      // Red line
                newStyle.Fill.FillForegnd.Value = "#00FF00";   // Green fill

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(newStyle);

                // Apply the new stylesheet to the cloned page
                // ApplyStyle takes (LineStyleId, FillStyleId, CharStyleId)
                clonedPage.ApplyStyle(newStyle.ID, newStyle.ID, newStyle.ID);

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}