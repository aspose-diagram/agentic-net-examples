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

            // Assign a unique ID (Visio expects IDs to start from 1)
            style.ID = diagram.StyleSheets.Count + 1;

            // Optional: give the stylesheet a name for easier identification
            style.Name = "LightGrayFill";

            // Set the fill pattern to solid (1) and the foreground color to light gray
            style.Fill.FillPattern.Value = 1;               // Solid fill
            style.Fill.FillForegnd.Value = "#D3D3D3";        // Light gray color in HEX

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(style);

            // Save the diagram to verify the stylesheet was added (optional)
            diagram.Save("StyledDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }