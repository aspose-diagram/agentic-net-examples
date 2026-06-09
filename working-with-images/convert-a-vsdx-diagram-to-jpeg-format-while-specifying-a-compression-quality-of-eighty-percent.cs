using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

        string outputPath = "output.jpg";

        try
        {
            Diagram diagram = new Diagram(inputPath);
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            saveOptions.JpegQuality = 80; // Compression quality (0-100)
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}