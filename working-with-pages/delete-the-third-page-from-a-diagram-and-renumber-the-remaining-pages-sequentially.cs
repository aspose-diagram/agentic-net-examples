using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing diagram (replace with your file path)
            var diagram = new Diagram("input.vsdx");

            // Delete the third page (index 2, zero‑based) if it exists
            if (diagram.Pages.Count >= 3)
            {
                diagram.Pages.RemoveAt(2);
            }

            // Renumber the remaining pages sequentially starting from 1
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                diagram.Pages[i].ID = i + 1;
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
