using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least two pages (zero‑based index 1 is page two).
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page to clone.");
                    return;
                }

                // Retrieve the source page (page two).
                Page sourcePage = diagram.Pages[1];

                // Create a new blank page.
                Page clonedPage = new Page();

                // Assign a unique ID to the new page (max existing ID + 1).
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }
                clonedPage.ID = maxPageId + 1;

                // Copy the contents of the source page into the new page.
                clonedPage.Copy(sourcePage);

                // Rename the cloned page.
                clonedPage.Name = "ClonedPage";

                // Add the cloned page to the diagram.
                diagram.Pages.Add(clonedPage);

                // ------------------------------------------------------------
                // Create a new stylesheet with distinct visual settings.
                // ------------------------------------------------------------
                StyleSheet newStyle = new StyleSheet();

                // Assign a unique stylesheet ID.
                newStyle.ID = diagram.StyleSheets.Count + 1;

                // ----- Text style (Char) -----
                Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
                textChar.IX = 0; // first character style entry
                textChar.Color.Value = "#FF0000"; // red text
                newStyle.Chars.Add(textChar);

                // ----- Line style -----
                newStyle.Line.LineColor.Value = "#00FF00"; // green line color
                newStyle.Line.LineWeight.Value = 0.02; // thin line

                // ----- Fill style -----
                newStyle.Fill.FillPattern.Value = 1; // solid fill
                newStyle.Fill.FillForegnd.Value = "#0000FF"; // blue fill

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(newStyle);

                // Apply the new stylesheet to the cloned page.
                // ApplyStyle(lineStyleId, fillStyleId, textStyleId)
                clonedPage.ApplyStyle(newStyle.ID, newStyle.ID, newStyle.ID);

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output file.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}