using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Create a new stylesheet
            StyleSheet style = new StyleSheet();

            // Assign a unique ID (next available)
            style.ID = diagram.StyleSheets.Count + 1;

            // Give the stylesheet a name (optional but helpful)
            style.Name = "LightGrayFill";

            // Set the fill foreground color to light gray (hex code #D3D3D3)
            style.Fill.FillForegnd.Value = "#D3D3D3";

            // Ensure the fill pattern is solid (value 1)
            style.Fill.FillPattern.Value = 1;

            // Add the stylesheet to the diagram's stylesheet collection
            diagram.StyleSheets.Add(style);

            // Confirmation output
            Console.WriteLine($"Added stylesheet '{style.Name}' with ID {style.ID} and fill color {style.Fill.FillForegnd.Value}");
        }
    }
}
