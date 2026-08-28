using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (empty with a default page)
        using (Diagram diagram = new Diagram())
        {
            // Define a single stylesheet with a solid white fill
            StyleSheet whiteStyle = new StyleSheet();
            whiteStyle.ID = diagram.StyleSheets.Count + 1; // assign a unique ID
            whiteStyle.Fill.FillPattern.Value = 1; // solid fill
            whiteStyle.Fill.FillForegnd.Value = "#FFFFFF"; // white color

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(whiteStyle);

            // Apply the stylesheet to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply the same style for fill, line, and character formatting
                page.ApplyStyle(whiteStyle.ID, whiteStyle.ID, whiteStyle.ID);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
