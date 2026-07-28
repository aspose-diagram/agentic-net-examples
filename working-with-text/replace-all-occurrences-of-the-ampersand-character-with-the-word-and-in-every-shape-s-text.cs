using System.IO;
using System;
using Aspose.Diagram;

class ReplaceAmpersand
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape's text contains '&'
                    string currentText = shape.GetPureText();
                    if (!string.IsNullOrEmpty(currentText) && currentText.Contains("&"))
                    {
                        // Replace '&' with 'and' in the shape's text
                        shape.ReplaceText("&", "and");
                        // Refresh shape geometry after text change
                        shape.RefreshData();
                    }
                }
            }

            // Save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
