using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace AsposeDiagramIStreamProviderTest
{
    // Custom IStreamProvider that records the default path for each resource.
    public class RecordingStreamProvider : IStreamProvider
    {
        // List to store the resource names (DefaultPath) received during export.
        public List<string> ReceivedPaths { get; } = new List<string>();

        // Called by Aspose.Diagram before writing a resource.
        public void InitStream(StreamProviderOptions options)
        {
            // Record the path/name of the resource.
            ReceivedPaths.Add(options.DefaultPath);
            // Provide a writable stream (in-memory) for the resource.
            options.Stream = new MemoryStream();
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the temporary stream if it was created.
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {
                // Create an empty diagram.
                Diagram diagram = new Diagram();

                // Add a simple shape to ensure at least one external resource is generated.
                // This creates a rectangle on the first page.
                diagram.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle", 0);

                // Prepare HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                RecordingStreamProvider provider = new RecordingStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML. The provider will be invoked for each resource.
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                // Verify that the provider received at least one resource name.
                if (provider.ReceivedPaths.Count == 0)
                {
                    throw new Exception("IStreamProvider was not invoked during HTML export.");
                }

                // Output the collected resource paths for diagnostic purposes.
                Console.WriteLine("IStreamProvider received the following resource paths:");
                foreach (string path in provider.ReceivedPaths)
                {
                    Console.WriteLine(path);
                }

                Console.WriteLine("Test passed: IStreamProvider was called correctly.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream.
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}