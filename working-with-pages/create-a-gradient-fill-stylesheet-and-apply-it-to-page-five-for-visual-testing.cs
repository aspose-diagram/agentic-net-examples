using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Ensure the diagram has at least 5 pages
            while (diagram.Pages.Count < 5)
            {
                // Determine a new unique page ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                Page newPage = new Page();
                newPage.ID = maxId + 1;
                diagram.Pages.Add(newPage);
            }

            // Create a new stylesheet with a gradient fill
            StyleSheet gradientStyle = new StyleSheet();
            gradientStyle.ID = diagram.StyleSheets.Count + 1; // unique ID

            // Configure gradient fill
            gradientStyle.Fill.FillPattern.Value = 25; // gradient pattern
            gradientStyle.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            gradientStyle.Fill.GradientFill.GradientDir.Value = 0; // direction (0 = left to right)
            gradientStyle.Fill.GradientFill.GradientStops.Clear();
            // Start color (red) at position 0
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));
            // End color (green) at position 1
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(gradientStyle);

            // Apply the stylesheet to page five (index 4)
            Page pageFive = diagram.Pages[4];
            pageFive.ApplyStyle(gradientStyle.ID, gradientStyle.ID, gradientStyle.ID);

            // Save the diagram for visual testing
            diagram.Save("GradientTest.vsdx", SaveFileFormat.Vsdx);
        }

        Console.WriteLine("Gradient stylesheet applied to page five and diagram saved as GradientTest.vsdx");
    }
}
