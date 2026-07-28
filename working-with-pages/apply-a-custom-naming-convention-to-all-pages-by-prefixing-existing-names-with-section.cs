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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Prefix each page's name with "Section_"
            foreach (Page page in diagram.Pages)
            {
                // Update the visible name
                page.Name = "Section_" + page.Name;

                // Update the universal name as well (keeps consistency)
                page.NameU = "Section_" + page.NameU;
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
