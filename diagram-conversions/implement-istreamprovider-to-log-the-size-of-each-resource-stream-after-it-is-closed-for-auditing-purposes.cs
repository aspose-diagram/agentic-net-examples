using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class AuditingStreamProvider : IStreamProvider
{
    // Called by Aspose.Diagram before writing a resource stream.
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Ensure the directory for the resource exists.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a file stream for the resource.
            FileStream fs = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
            options.Stream = fs;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing stream for '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }

    // Called by Aspose.Diagram after the resource stream has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        try
        {
            if (options.Stream != null)
            {
                long size = options.Stream.Length;
                // Log the size of the resource for auditing.
                Console.WriteLine($"Resource saved: {options.DefaultPath}, Size: {size} bytes");
                options.Stream.Close();
                options.Stream = null;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error closing stream for '{options.DefaultPath}': {ex.Message}");
            throw;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputHtmlPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML export options with the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new AuditingStreamProvider();

                // Save the diagram as HTML. The second argument must be a SaveOptions instance.
                diagram.Save(outputHtmlPath, htmlOptions);
                Console.WriteLine($"Diagram successfully exported to HTML: {outputHtmlPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}