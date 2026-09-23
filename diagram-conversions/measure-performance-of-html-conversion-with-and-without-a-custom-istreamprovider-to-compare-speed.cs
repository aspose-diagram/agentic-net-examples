using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Called before each resource (HTML, images, CSS) is written
    public void InitStream(StreamProviderOptions options)
    {
        // Use a memory stream to capture the output; you could also direct to a custom folder
        options.Stream = new MemoryStream();
    }

    // Called after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        // Clean up the stream
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "input.vsdx";

            // Output files for the two scenarios
            string outputDefault = "output_default.html";
            string outputCustom = "output_custom.html";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------
            // Export HTML without custom IStreamProvider
            // ------------------------------
            Stopwatch swDefault = Stopwatch.StartNew();

            HTMLSaveOptions defaultOptions = new HTMLSaveOptions();
            diagram.Save(outputDefault, defaultOptions);

            swDefault.Stop();

            // ------------------------------
            // Export HTML with custom IStreamProvider
            // ------------------------------
            Stopwatch swCustom = Stopwatch.StartNew();

            HTMLSaveOptions customOptions = new HTMLSaveOptions();
            customOptions.StreamProvider = new CustomStreamProvider();
            diagram.Save(outputCustom, customOptions);

            swCustom.Stop();

            // Report timings
            Console.WriteLine($"Default HTML export time: {swDefault.ElapsedMilliseconds} ms");
            Console.WriteLine($"Custom IStreamProvider HTML export time: {swCustom.ElapsedMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}