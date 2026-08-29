using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create a custom stylesheet (ID must be unique)
            StyleSheet centerStyle = new StyleSheet();
            centerStyle.ID = diagram.StyleSheets.Count + 1;
            // Optional: give the style a name if the property exists
            // centerStyle.Name = "CenterStyle";

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(centerStyle);

            // Access the second page (index 1)
            Page pageTwo = diagram.Pages[1];

            // Iterate over all shapes on page two
            foreach (Shape shape in pageTwo.Shapes)
            {
                // Apply the custom stylesheet to the shape's text
                shape.TextStyle = centerStyle;

                // Align each paragraph's text to center
                foreach (Para para in shape.Paras)
                {
                    para.HorzAlign.Value = HorzAlignValue.Center;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
