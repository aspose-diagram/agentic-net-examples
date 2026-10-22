using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------- Add a SolutionXML element containing a page node ----------
                // This XML can be used by external solutions to store custom data.
                SolutionXML solXml = new SolutionXML();
                solXml.Name = "CustomPageNode";
                solXml.XmlValue = "<PageNode><BackgroundStyle>CustomStyle</BackgroundStyle></PageNode>";
                diagram.SolutionXMLs.Add(solXml);

                // ---------- Determine the next page ID ----------
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    // Page.ID is an integer, not a cell, so use it directly.
                    if (existingPage.ID > maxPageId)
                    {
                        maxPageId = existingPage.ID;
                    }
                }

                // ---------- Create a new background page ----------
                Page backgroundPage = new Page();
                backgroundPage.ID = maxPageId + 1;
                backgroundPage.Name = "BackgroundPage";
                backgroundPage.Background = BOOL.True; // Mark as a background page.

                // Add the new background page to the diagram.
                diagram.Pages.Add(backgroundPage);

                // ---------- Create a style sheet for the background fill ----------
                StyleSheet bgStyle = new StyleSheet();
                bgStyle.ID = diagram.StyleSheets.Count + 1;

                // Set a solid fill pattern and a light red background color.
                bgStyle.Fill.FillPattern.Value = 1; // Solid fill.
                bgStyle.Fill.FillForegnd.Value = "#FFCCCC"; // Light red.

                // Add the style sheet to the diagram.
                diagram.StyleSheets.Add(bgStyle);

                // Apply the style to the newly created background page.
                backgroundPage.ApplyStyle(bgStyle.ID, bgStyle.ID, bgStyle.ID);

                // ---------- Save the modified diagram ----------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}