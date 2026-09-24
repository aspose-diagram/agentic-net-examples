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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Theme name that was applied (hard‑coded for this example)
            string themeName = "Bubble";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Create a custom property to store the theme name
                    Prop themeProp = new Prop();
                    themeProp.Name = "AppliedTheme";
                    themeProp.Label.Value = "Applied Theme";
                    themeProp.Value.Val = themeName;
                    themeProp.Type.Value = TypePropValue.String;

                    // Add the custom property to the shape
                    shape.Props.Add(themeProp);
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
