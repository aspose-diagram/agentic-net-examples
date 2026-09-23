using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify the diagram has at least two pages to clone
            if (diagram.Pages.Count < 2)
                throw new Exception("The diagram does not contain a second page to clone.");

            // Retrieve the second page (index 1) as the source for cloning
            Page sourcePage = diagram.Pages[1];

            // Determine the highest existing page ID to assign a unique ID to the new page
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
                if (p.ID > maxPageId) maxPageId = p.ID;

            // Create a new blank page with a new unique ID and a name
            Page clonedPage = new Page(maxPageId + 1);
            clonedPage.Name = "ClonedPage";
            diagram.Pages.Add(clonedPage);

            // Copy all contents from the source page into the newly created page
            clonedPage.Copy(sourcePage);

            // -------------------------------------------------
            // Create a new stylesheet to apply to the cloned page
            // -------------------------------------------------
            StyleSheet newStyle = new StyleSheet
            {
                ID = diagram.StyleSheets.Count + 1,
                Name = "CustomStyle"
            };

            // Character formatting: red text for the first character run
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0;                     // index of the character run
            charFormat.Color.Value = "#FF0000";    // red color
            newStyle.Chars.Add(charFormat);

            // Line formatting: green solid line
            newStyle.Line.LineColor.Value = "#00FF00";               // green line color
            newStyle.Line.LinePattern.Value = LinePatternValue.Solid; // solid line pattern
            newStyle.Line.LineWeight.Value = 0.02;                    // thickness in inches

            // Fill formatting: blue solid fill
            newStyle.Fill.FillForegnd.Value = "#0000FF"; // blue fill color
            // FillPattern expects an integer; 1 corresponds to solid fill
            newStyle.Fill.FillPattern.Value = 1;

            // Add the new stylesheet to the diagram's collection
            diagram.StyleSheets.Add(newStyle);

            // Apply the stylesheet to the cloned page (TextStyleID, LineStyleID, FillStyleID)
            clonedPage.ApplyStyle(newStyle.ID, newStyle.ID, newStyle.ID);

            // Define the output file path
            string outputPath = "output.vsdx";

            // Save the modified diagram using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}