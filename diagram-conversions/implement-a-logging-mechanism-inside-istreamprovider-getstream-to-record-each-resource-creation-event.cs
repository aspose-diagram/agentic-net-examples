using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Called before a resource stream is created.
    public void InitStream(StreamProviderOptions options)
    {
        // Log the resource path that will be used for the stream.
        Console.WriteLine($"[StreamProvider] InitStream called for resource: {options.DefaultPath}");

        // Create the file stream for the resource.
        try
        {
            // Ensure the directory exists.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[StreamProvider] Failed to create stream for {options.DefaultPath}: {ex.Message}");
            throw;
        }
    }

    // Called after the resource stream has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Log the closing of the stream.
        Console.WriteLine($"[StreamProvider] CloseStream called for resource: {options.DefaultPath}");

        try
        {
            options.Stream?.Close();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[StreamProvider] Failed to close stream for {options.DefaultPath}: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Path to the Aspose.Diagram license file (if you have one).
        string licensePath = "Aspose.Diagram.lic";
        if (!File.Exists(licensePath))
        {
            Console.Error.WriteLine($"License file not found: {licensePath}");
            // Continue without a license; Aspose will work in evaluation mode.
        }
        else
        {
            try
            {
                License license = new License();
                license.SetLicense(licensePath);
                Console.WriteLine("License loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load license: {ex.Message}");
                return;
            }
        }

        // Input Visio diagram file.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for HTML files and resources.
        string outputDir = "output_html";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load the diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Configure HTML save options with the custom stream provider.
        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
        {
            // Assign the custom provider that logs each resource creation.
            StreamProvider = new MyStreamProvider()
        };

        // Define the main HTML output file path.
        string htmlOutputPath = Path.Combine(outputDir, "diagram.html");

        // Save the diagram as HTML.
        try
        {
            diagram.Save(htmlOutputPath, htmlOptions);
            Console.WriteLine($"Diagram exported to HTML successfully: {htmlOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram as HTML: {ex.Message}");
        }
    }
}