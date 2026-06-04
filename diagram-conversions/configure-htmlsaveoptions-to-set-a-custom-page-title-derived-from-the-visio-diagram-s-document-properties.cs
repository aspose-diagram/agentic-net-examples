using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Derive a custom title from the document's built‑in or custom properties
            string title = diagram.DocumentProps.Title;

            // If the built‑in title is empty, fall back to the first custom property (if any)
            if (string.IsNullOrWhiteSpace(title) && diagram.DocumentProps.CustomProps.Count > 0)
            {
                CustomProp firstCustom = diagram.DocumentProps.CustomProps[0];
                title = firstCustom.CustomValue.ValueString;
            }

            // Configure HTML export options with the derived title
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.Title = title;          // Set the page title
            htmlOptions.SaveTitle = true;       // Ensure the title is included in the output

            // Save the diagram as HTML using the configured options
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
