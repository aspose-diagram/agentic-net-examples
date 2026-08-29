using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains a Text object
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        // Convert the text collection to a string for inspection
                        string currentText = shape.Text.Value.ToString();

                        // Check for the target word
                        if (currentText.Contains("Confidential"))
                        {
                            // Replace only the target word, preserving surrounding text
                            shape.ReplaceText("Confidential", "Public");

                            // Refresh shape geometry after text change
                            shape.RefreshData();
                        }
                    }
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
