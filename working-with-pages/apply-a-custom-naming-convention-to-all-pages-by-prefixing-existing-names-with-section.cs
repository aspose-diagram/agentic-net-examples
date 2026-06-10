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

            // Load the existing Visio file
            Diagram diagram = new Diagram("input.vsdx");

            // Prefix each page's universal name with "Section_"
            foreach (Page page in diagram.Pages)
            {
                string currentName = page.NameU ?? string.Empty;
                page.NameU = "Section_" + currentName;
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
