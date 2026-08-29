using System.IO;
using System;
using Aspose.Diagram;

class ReplaceAmpersandInShapes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the current plain text of the shape
                    string currentText = shape.GetPureText();

                    // Check if the text contains the ampersand character
                    if (currentText != null && currentText.Contains("&"))
                    {
                        // Replace '&' with the word 'and'
                        string replacedText = currentText.Replace("&", "and");

                        // Apply the replacement to the shape
                        shape.ReplaceText(currentText, replacedText);

                        // Refresh shape data to update its geometry after text change
                        shape.RefreshData();
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
