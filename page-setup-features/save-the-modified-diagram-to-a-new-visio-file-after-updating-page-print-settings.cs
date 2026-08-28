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
            Diagram diagram = new Diagram("input.vdx");

            // Update page print settings (example: enable page enlargement when printing)
            PrintSaveOptions printOptions = new PrintSaveOptions();
            printOptions.EnlargePage = true; // set desired print option

            // (Optional) Apply any additional print-related settings here.
            // For saving, configure DiagramSaveOptions as needed.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();
            saveOptions.AutoFitPageToDrawingContent = true; // ensure page fits drawing content

            // Save the modified diagram to a new Visio file
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
