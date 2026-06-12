using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 7
            Shape shape = page.Shapes.GetShape(7);

            // Get the current plain text of the shape
            string text = shape.Text.Value.Text;

            // Ensure the shape has a text run containing the original text
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(text));

            // Clear any existing character formatting entries
            shape.Chars.Clear();

            // Apply bold formatting to the first three characters
            for (int i = 0; i < text.Length; i++)
            {
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = i; // character index

                // Set bold style for the first three characters, otherwise leave undefined
                if (i < 3)
                    ch.Style.Value = StyleValue.Bold;
                else
                    ch.Style.Value = StyleValue.Undefined;

                shape.Chars.Add(ch);
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
