using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // 1. Add a new SolutionXML element containing a page node XML
                // ------------------------------------------------------------
                SolutionXML solXml = new SolutionXML();
                solXml.Name = "PageInfo";
                // Example XML defining a page node; adjust as needed
                solXml.XmlValue = "<Page id=\"1\" name=\"BackgroundPage\" />";
                diagram.SolutionXMLs.Add(solXml);

                // ------------------------------------------------------------
                // 2. Determine the next available page ID (int)
                // ------------------------------------------------------------
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    // Page.ID is a long; cast to int for comparison
                    if ((int)p.ID > maxPageId)
                        maxPageId = (int)p.ID;
                }

                // ------------------------------------------------------------
                // 3. Create a new background page and mark it as background
                // ------------------------------------------------------------
                Page backgroundPage = new Page();
                backgroundPage.ID = maxPageId + 1;               // Assign a unique ID (int)
                backgroundPage.Name = "BackgroundPage";          // Friendly name
                backgroundPage.Background = BOOL.True;           // Mark as background page
                diagram.Pages.Add(backgroundPage);

                // ------------------------------------------------------------
                // 4. Define a style sheet for the background fill
                // ------------------------------------------------------------
                StyleSheet bgStyle = new StyleSheet();
                bgStyle.ID = diagram.StyleSheets.Count + 1;       // Unique style ID (int)
                bgStyle.Name = "BackgroundStyle";

                // Set solid fill pattern (1 = solid)
                bgStyle.Fill.FillPattern.Value = 1;

                // Set fill foreground color (light pink)
                bgStyle.Fill.FillForegnd.Value = "#FFCCCC";

                // Optionally remove outline by setting line pattern to 0
                bgStyle.Line.LinePattern.Value = 0;

                diagram.StyleSheets.Add(bgStyle);

                // ------------------------------------------------------------
                // 5. Apply the style to the background page
                // ------------------------------------------------------------
                // ApplyStyle takes three style IDs: line, fill, and text style IDs
                backgroundPage.ApplyStyle(bgStyle.ID, bgStyle.ID, bgStyle.ID);

                // ------------------------------------------------------------
                // 6. Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}