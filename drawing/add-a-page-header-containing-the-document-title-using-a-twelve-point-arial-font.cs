using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                string title = string.IsNullOrWhiteSpace(diagram.DocumentProps.Title) ? "Untitled Document" : diagram.DocumentProps.Title;
                diagram.HeaderFooter.HeaderCenter = title;

                diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
                diagram.HeaderFooter.HeaderFooterFont.Weight = 400;
                diagram.HeaderFooter.HeaderFooterFont.Height = -16; // 12 pt * 1.333 ≈ 16, negative as required

                diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}