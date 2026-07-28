using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through every page and shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's text without formatting
                    string pureText = shape.GetPureText();

                    // If the shape contains text, replace line breaks with spaces
                    if (!string.IsNullOrEmpty(pureText))
                    {
                        string singleLine = pureText
                            .Replace("\r\n", " ")
                            .Replace("\n", " ")
                            .Replace("\r", " ");

                        // Set the modified text back to the shape (no formatting)
                        shape.Text.Value.SetWholeText(singleLine);

                        // Refresh shape geometry to reflect the text change
                        shape.RefreshData();
                    }
                }
            }

            // Save the updated diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
