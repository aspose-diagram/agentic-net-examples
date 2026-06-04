using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        Diagram diagram = null;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to load diagram: " + ex.Message);
            return;
        }

        try
        {
            diagram.Version = "15.0";
            Console.WriteLine("Version property modified successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to modify read‑only property 'Version': " + ex.Message);
        }

        try
        {
            diagram.DocumentProps.BuildNumberCreated = "9999";
            Console.WriteLine("BuildNumberCreated property modified successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to modify read‑only property 'BuildNumberCreated': " + ex.Message);
        }

        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Failed to save diagram: " + ex.Message);
        }
    }
}