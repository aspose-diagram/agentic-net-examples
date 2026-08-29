using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the full plain text of the shape
                    string fullText = shape.Text.Value.Text;

                    // Skip shapes that have no text
                    if (string.IsNullOrWhiteSpace(fullText))
                        continue;

                    // Split the text into paragraphs (Visio uses line breaks)
                    string[] paragraphs = fullText.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine($"Shape ID {shape.ID} - Paragraphs:");
                    // Output each paragraph's content
                    for (int i = 0; i < paragraphs.Length; i++)
                    {
                        Console.WriteLine($"  Paragraph {i + 1}: {paragraphs[i]}");
                    }
                    Console.WriteLine(); // Blank line for readability
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }