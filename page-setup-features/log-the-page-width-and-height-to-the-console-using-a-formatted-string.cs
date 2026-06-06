using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page in the diagram
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (values are in inches)
            double width = page.PageSheet.PageProps.PageWidth.Value;
            double height = page.PageSheet.PageProps.PageHeight.Value;

            // Log the dimensions using a formatted string
            Console.WriteLine($"Page size: Width = {width} inches, Height = {height} inches");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
