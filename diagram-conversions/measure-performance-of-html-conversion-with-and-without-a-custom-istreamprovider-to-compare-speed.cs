using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // Provides a MemoryStream for the HTML export
    public void InitStream(StreamProviderOptions options)
    {
        options.Stream = new MemoryStream();
    }

    // Closes and disposes the stream after export
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Dispose();
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram (create/load rule)
            Diagram diagram = new Diagram(diagramPath);

            // -------------------- Without custom IStreamProvider --------------------
            Stopwatch swWithout = Stopwatch.StartNew();

            // Use default HTML save options
            HTMLSaveOptions optionsWithout = new HTMLSaveOptions();

            // Save the diagram to HTML (save rule)
            diagram.Save("output_without_provider.html", optionsWithout);

            swWithout.Stop();
            Console.WriteLine($"Conversion without custom IStreamProvider: {swWithout.ElapsedMilliseconds} ms");

            // -------------------- With custom IStreamProvider --------------------
            Stopwatch swWith = Stopwatch.StartNew();

            // Configure HTML save options with a custom stream provider
            HTMLSaveOptions optionsWith = new HTMLSaveOptions
            {
                StreamProvider = new CustomStreamProvider()
            };

            // Save the diagram; the provider supplies the output stream
            diagram.Save("output_with_provider.html", optionsWith);

            swWith.Stop();
            Console.WriteLine($"Conversion with custom IStreamProvider: {swWith.ElapsedMilliseconds} ms");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}