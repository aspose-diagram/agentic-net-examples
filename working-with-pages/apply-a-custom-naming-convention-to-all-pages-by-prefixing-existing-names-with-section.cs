using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Apply custom naming convention to each page
            foreach (Page page in diagram.Pages)
            {
                // Prefix the current page name with "Section_"
                page.Name = "Section_" + page.Name;
            }

            // Save the updated diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
