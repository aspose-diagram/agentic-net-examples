using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source VSD file
            using (var diagram = new Diagram("input.vsd"))
            {
                // Save the diagram as PDF using the Save method with SaveFileFormat.Pdf
                diagram.Save("output.pdf", SaveFileFormat.Pdf);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
