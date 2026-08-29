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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        // Get the plain text (without formatting)
                        string pureText = shape.GetPureText();

                        // If the text contains line breaks, replace them with spaces
                        if (!string.IsNullOrEmpty(pureText) && (pureText.Contains("\r") || pureText.Contains("\n")))
                        {
                            string singleLineText = pureText
                                .Replace("\r\n", " ")
                                .Replace("\n", " ")
                                .Replace("\r", " ");

                            // Set the modified text back to the shape (without formatting)
                            shape.Text.Value.SetWholeText(singleLineText);

                            // Refresh shape data to reflect the text change
                            shape.RefreshData();
                        }
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
