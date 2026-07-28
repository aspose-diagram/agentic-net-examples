using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (you can change the index as needed)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace 1 with the actual shape ID you want to inspect)
            Shape shape = page.Shapes.GetShape(1);

            // Retrieve the paragraph collection from the shape
            // (the collection itself is not used for text extraction, but we iterate it as required)
            foreach (Aspose.Diagram.Para para in shape.Paras)
            {
                // No direct text property on Para, so we will extract the full text later
                // This loop demonstrates access to each paragraph object
            }

            // Get the complete plain text of the shape
            string fullText = shape.Text.Value.ToString();

            // Split the text into paragraphs based on line breaks
            string[] paragraphs = fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            // Output each paragraph's content
            foreach (string paragraph in paragraphs)
            {
                Console.WriteLine(paragraph);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
