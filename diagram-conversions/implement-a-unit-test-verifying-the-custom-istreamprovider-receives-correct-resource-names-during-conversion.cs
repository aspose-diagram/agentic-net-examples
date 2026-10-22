using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class MyStreamProvider : IStreamProvider
{
    // Stores the resource names (default paths) received during conversion.
    public List<string> ReceivedResourceNames { get; } = new List<string>();

    // Called by Aspose.Diagram when a new resource stream is required.
    public void InitStream(StreamProviderOptions options)
    {
        // Record the default path which identifies the resource (e.g., image file name).
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            ReceivedResourceNames.Add(options.DefaultPath);
        }

        // Provide a writable stream for the resource. Here we use a MemoryStream
        // because the actual content is not needed for the test.
        options.Stream = new MemoryStream();
    }

    // Called after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        // Dispose the stream we created in InitStream.
        options.Stream?.Dispose();
        options.Stream = null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Create HTML save options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            MyStreamProvider provider = new MyStreamProvider();
            htmlOptions.StreamProvider = provider;

            // Perform the conversion to HTML.
            diagram.Save(outputPath, htmlOptions);

            // Verify that the stream provider received at least one resource name.
            if (provider.ReceivedResourceNames.Count == 0)
            {
                throw new Exception("IStreamProvider did not receive any resource names during conversion.");
            }

            // Output the received resource names for verification.
            Console.WriteLine("IStreamProvider received the following resource names:");
            foreach (string name in provider.ReceivedResourceNames)
            {
                Console.WriteLine(name);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}