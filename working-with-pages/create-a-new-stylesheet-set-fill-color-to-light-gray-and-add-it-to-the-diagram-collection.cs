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

        // Assign a unique ID (next available)
        style.ID = diagram.StyleSheets.Count + 1;

        // Optional: give the stylesheet a name
        style.Name = "LightGrayFill";

        // Set fill pattern to solid (1)
        style.Fill.FillPattern.Value = 1;

        // Set fill foreground color to light gray
        style.Fill.FillForegnd.Value = "#D3D3D3";

        // Add the stylesheet to the diagram's collection
        diagram.StyleSheets.Add(style);

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
