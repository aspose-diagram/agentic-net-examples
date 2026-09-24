using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Text collection
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        // Iterate over each text run in the shape
                        for (int i = 0; i < shape.Text.Value.Count; i++)
                        {
                            // The collection can contain Txt or Cp objects; process only Txt
                            if (shape.Text.Value[i] is Txt txt)
                            {
                                // Replace '&' with "and"
                                txt.Text = txt.Text.Replace("&", "and");
                            }
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
