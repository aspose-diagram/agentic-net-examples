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
            // Ensure the diagram has at least five pages
            while (diagram.Pages.Count < 5)
            {
                diagram.Pages.Add(new Page());
            }

            // Create a stylesheet that defines a gradient fill
            StyleSheet gradientStyle = new StyleSheet();
            gradientStyle.ID = diagram.StyleSheets.Count + 1; // unique ID

            // Set fill to gradient pattern (value 25)
            gradientStyle.Fill.FillPattern.Value = 25;
            // Enable the gradient
            gradientStyle.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            // Set gradient direction (0 = left‑to‑right)
            gradientStyle.Fill.GradientFill.GradientDir.Value = 0;
            // Clear any existing gradient stops
            gradientStyle.Fill.GradientFill.GradientStops.Clear();
            // Add a blue stop at the start (position 0)
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));
            // Add a green stop at the end (position 1)
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(gradientStyle);

            // Retrieve page five (zero‑based index 4) and apply the stylesheet
            Page pageFive = diagram.Pages[4];
            pageFive.ApplyStyle(gradientStyle.ID, gradientStyle.ID, gradientStyle.ID);

            // Save the diagram for visual verification
            string outputPath = "GradientFillTest.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }

        Console.WriteLine("Diagram saved with gradient fill stylesheet applied to page five.");
    }
}
