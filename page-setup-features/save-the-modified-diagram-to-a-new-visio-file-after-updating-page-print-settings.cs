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

            // Load the existing Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Create print options and modify page‑print related settings
            PrintSaveOptions printOptions = new PrintSaveOptions();
            // Example: enable page enlargement when printing/saving
            printOptions.EnlargePage = true;
            // Specify the output format (VDX)
            printOptions.SaveFormat = SaveFileFormat.Vdx;

            // Save the modified diagram to a new file (lifecycle rule: save)
            diagram.Save("output.vdx", printOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
