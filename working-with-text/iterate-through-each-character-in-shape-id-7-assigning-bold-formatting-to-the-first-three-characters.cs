using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            if (shape == null)
            {
                throw new Exception("Shape with ID 7 not found.");
            }

            // Get the plain text of the shape
            string plainText = shape.Text.Value.Text;
            if (string.IsNullOrEmpty(plainText))
            {
                throw new Exception("Shape with ID 7 contains no text.");
            }

            // Clear any existing character formatting runs
            shape.Chars.Clear();

            // Iterate through each character and apply bold to the first three
            for (int i = 0; i < plainText.Length; i++)
            {
                Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                ch.IX = i; // character index

                if (i < 3)
                {
                    // Apply bold style
                    ch.Style.Value = StyleValue.Bold;
                }
                else
                {
                    // No special style (undefined)
                    ch.Style.Value = StyleValue.Undefined;
                }

                shape.Chars.Add(ch);
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
