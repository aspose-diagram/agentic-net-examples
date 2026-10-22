using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CustomStreamProvider : IStreamProvider
{
    // This method is called by Aspose.Diagram when it needs to write an external resource (e.g., image) during HTML export.
    public void InitStream(StreamProviderOptions options)
    {
        // Set a custom base URL that will be used in the generated HTML.
        // The property name is CustomPath according to the documentation.
        try
        {
            options.CustomPath = "http://example.com/resources/";
        }
        catch
        {
            // If the property does not exist in the current version, ignore silently.
        }

        // Determine a file system path where the resource will be saved.
        // options.DefaultPath contains the relative file name (e.g., "image1.png").
        string resourcesFolder = Path.Combine(Directory.GetCurrentDirectory(), "custom_resources");
        Directory.CreateDirectory(resourcesFolder);

        string filePath = Path.Combine(resourcesFolder, options.DefaultPath ?? "resource.bin");

        // Assign a writable stream to the options so Aspose can write the resource data.
        options.Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Close();
            options.Stream = null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or a default placeholder).
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directories for the two conversion scenarios.
        string defaultOutputDir = Path.Combine(Directory.GetCurrentDirectory(), "output_default");
        string customOutputDir = Path.Combine(Directory.GetCurrentDirectory(), "output_custom");
        Directory.CreateDirectory(defaultOutputDir);
        Directory.CreateDirectory(customOutputDir);

        // --------------------------------------------------------------------
        // Measure HTML conversion without a custom IStreamProvider (default behavior).
        // --------------------------------------------------------------------
        long defaultElapsedMs = 0;
        try
        {
            Stopwatch swDefault = Stopwatch.StartNew();

            // Load the diagram.
            Diagram diagramDefault = new Diagram(inputPath);

            // Export each page as a separate HTML file (HTMLSaveOptions does not support multi‑page export).
            for (int i = 0; i < diagramDefault.Pages.Count; i++)
            {
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,
                    PageCount = 1
                };

                string outPath = Path.Combine(defaultOutputDir, $"page_{i + 1}.html");
                diagramDefault.Save(outPath, htmlOptions);
            }

            swDefault.Stop();
            defaultElapsedMs = swDefault.ElapsedMilliseconds;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during default HTML conversion: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // Measure HTML conversion with a custom IStreamProvider.
        // --------------------------------------------------------------------
        long customElapsedMs = 0;
        try
        {
            Stopwatch swCustom = Stopwatch.StartNew();

            // Load the diagram again to ensure a fair comparison.
            Diagram diagramCustom = new Diagram(inputPath);

            // Export each page using a custom stream provider.
            for (int i = 0; i < diagramCustom.Pages.Count; i++)
            {
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    PageIndex = i,
                    PageCount = 1,
                    StreamProvider = new CustomStreamProvider()
                };

                string outPath = Path.Combine(customOutputDir, $"page_{i + 1}.html");
                diagramCustom.Save(outPath, htmlOptions);
            }

            swCustom.Stop();
            customElapsedMs = swCustom.ElapsedMilliseconds;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during custom HTML conversion: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // Output the performance comparison.
        // --------------------------------------------------------------------
        Console.WriteLine("HTML conversion performance:");
        Console.WriteLine($"- Default provider: {defaultElapsedMs} ms");
        Console.WriteLine($"- Custom IStreamProvider: {customElapsedMs} ms");
        Console.WriteLine($"- Difference: {Math.Abs(customElapsedMs - defaultElapsedMs)} ms");
    }
}