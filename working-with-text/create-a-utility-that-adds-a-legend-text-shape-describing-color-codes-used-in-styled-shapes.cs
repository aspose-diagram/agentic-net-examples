using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the legend entries that map color codes to their meanings
            string[] legendLines = new string[]
            {
                "Color1: Red",
                "Color2: Green",
                "Color3: Blue",
                "Color4: Yellow"
            };

            // Add the legend to the first page at a chosen position
            LegendUtility.AddLegend(diagram, pinX: 5.0, pinY: 5.0, legendLines);

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}

static class LegendUtility
{
    /// <summary>
    /// Adds a text shape that serves as a legend describing color codes.
    /// </summary>
    /// <param name="diagram">The Visio diagram.</param>
    /// <param name="pinX">X‑coordinate of the legend’s pin (center).</param>
    /// <param name="pinY">Y‑coordinate of the legend’s pin (center).</param>
    /// <param name="lines">Array of strings, each representing one legend entry.</param>
    public static void AddLegend(Diagram diagram, double pinX, double pinY, string[] lines)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));
        if (lines == null || lines.Length == 0) return;

        // Combine the lines using Visio’s line‑break token (\n)
        string legendText = string.Join("\\n", lines);

        // Estimate width and height for the text shape
        double width = 2.5;                                   // inches
        double lineHeight = 0.2;                              // approximate height per line (inches)
        double height = lines.Length * lineHeight + 0.2;      // add a little padding

        // Add the text shape to the first page
        Page page = diagram.Pages[0];
        Shape legendShape = page.AddText(pinX, pinY, width, height,
                                         legendText, "Arial", "0,0,0", 0.2);

        // Optional: make the legend’s background transparent (no fill)
        legendShape.Fill.FillPattern.Value = 0; // 0 = no fill
    }
}