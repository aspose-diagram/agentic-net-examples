using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load a Visio diagram (replace with your actual file path)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page in the diagram
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (if any)
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.WriteLine("No shapes found on the page.");
                return;
            }

            // Ensure the shape has paragraph information
            if (shape.Paras == null || shape.Paras.Count == 0)
            {
                Console.WriteLine("The selected shape contains no paragraphs.");
                return;
            }

            // Get the plain text of the shape and split it into individual paragraphs
            // Shape.Text.Value.Text returns the concatenated text of all paragraphs
            string[] paragraphTexts = shape.Text.Value.Text
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            // Iterate over the paragraph collection and output each paragraph's content
            int index = 0;
            foreach (Para para in shape.Paras)
            {
                string text = index < paragraphTexts.Length ? paragraphTexts[index] : string.Empty;
                Console.WriteLine($"Paragraph {index + 1}: {text}");
                index++;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
