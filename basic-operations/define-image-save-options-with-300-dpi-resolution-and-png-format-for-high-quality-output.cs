using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Create a new diagram
        var diagram = new Diagram();

        // Add a simple rectangle to have some content
        var page = diagram.Pages[0];
        page.DrawRectangle(2.0, 2.0, 4.0, 3.0);

        // Configure image save options: PNG format with 300 DPI resolution
        var saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
        saveOptions.Resolution = 300f; // DPI

        // Save the diagram as a high‑quality PNG image
        string outputPath = "output.png";
        diagram.Save(outputPath, saveOptions);

        Console.WriteLine($"Diagram saved to '{outputPath}' with 300 DPI PNG format.");
    }
}
