using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Create a new stylesheet
        StyleSheet style = new StyleSheet();

        // Assign a unique ID
        style.ID = diagram.StyleSheets.Count + 1;

        // Optional: give the stylesheet a name
        style.Name = "LightGrayStyle";

        // Set fill pattern to solid (1) and fill foreground color to light gray
        style.Fill.FillPattern.Value = 1;          // solid fill
        style.Fill.FillForegnd.Value = "#D3D3D3"; // light gray color

        // Add the stylesheet to the diagram's collection
        diagram.StyleSheets.Add(style);

        // Save the diagram (optional verification)
        diagram.Save("StyledDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
