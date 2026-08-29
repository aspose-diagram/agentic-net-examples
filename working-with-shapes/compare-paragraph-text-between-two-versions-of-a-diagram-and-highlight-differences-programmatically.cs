using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the two diagram versions
            string diagramPath1 = "DiagramV1.vsdx";
            string diagramPath2 = "DiagramV2.vsdx";
            string outputPath = "DiagramDiffHighlighted.vsdx";

            // Load the diagrams
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Iterate through pages of the first diagram
            foreach (Page page1 in diagram1.Pages)
            {
                // Find the corresponding page in the second diagram by ID
                Page page2 = diagram2.Pages.GetPage(page1.ID);
                if (page2 == null)
                {
                    continue; // No matching page, skip
                }

                // Iterate through shapes on the page
                foreach (Shape shape1 in page1.Shapes)
                {
                    // Find shape with the same ID in the second diagram
                    Shape shape2 = page2.Shapes.GetShape(shape1.ID);
                    if (shape2 == null)
                    {
                        continue; // No matching shape, skip
                    }

                    // Retrieve plain text from both shapes
                    string text1 = shape1.Text.Value.Text ?? string.Empty;
                    string text2 = shape2.Text.Value.Text ?? string.Empty;

                    // Compare the texts
                    if (!string.Equals(text1, text2, StringComparison.Ordinal))
                    {
                        // Highlight differences by setting a yellow fill color
                        shape1.Fill.FillForegnd.Value = "#FFFF00";
                        shape2.Fill.FillForegnd.Value = "#FFFF00";
                    }
                }
            }

            // Save the highlighted diagram
            diagram1.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
