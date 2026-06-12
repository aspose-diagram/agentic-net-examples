using System;
using System.IO;
using Aspose.Diagram;
using System.Collections.Generic;

class WatermarkExample
{
    static void Main()
    {
        try
        {

            // Load an existing diagram
            Diagram diagram = new Diagram("input.vsdx"); // load rule

            // User‑defined palette (hex RGB strings)
            var palette = new Dictionary<string, string>
            {
                { "Primary",   "#1E90FF" }, // DodgerBlue
                { "Secondary", "#FF4500" }, // OrangeRed
                { "Accent",    "#32CD32" }  // LimeGreen
            };

            // Select the desired color from the palette
            string watermarkColor = palette["Primary"]; // customize as needed

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add watermark text with the custom color
            double pinX = 5.0;      // X coordinate of the text center
            double pinY = 5.0;      // Y coordinate of the text center
            double width = 2.0;    // Width of the text box
            double height = 0.5;   // Height of the text box
            Shape watermark = page.AddText(pinX, pinY, width, height,
                                           "CONFIDENTIAL", "Arial", watermarkColor, 48);

            // Optional: set transparency (0 = opaque, 1 = fully transparent)
            // watermark.Char.ColorTrans = new DoubleValue(0.2); // 20% transparent

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx); // save rule

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
