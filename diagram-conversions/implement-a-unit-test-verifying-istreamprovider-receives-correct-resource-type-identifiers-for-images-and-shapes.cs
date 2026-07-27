using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class TestStreamProvider : IStreamProvider
{
    public int CallCount { get; private set; } = 0;

    public void InitStream(StreamProviderOptions options)
    {
        CallCount++;
        // Provide a dummy stream if the API expects one.
        if (options.Stream == null)
        {
            options.Stream = new MemoryStream();
        }
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // No cleanup required for this test.
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram.
            Diagram diagram = new Diagram();

            // Add a rectangle shape.
            long rectId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

            // Add an image (foreign) shape using an empty memory stream as placeholder.
            using (MemoryStream imgStream = new MemoryStream())
            {
                long imgId = diagram.ActivePage.AddShape(4.0, 4.0, 2.0, 2.0, imgStream);
            }

            // Set up HTML export options with the custom stream provider.
            TestStreamProvider provider = new TestStreamProvider();
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = provider;

            // Export the diagram to HTML.
            string outputPath = "test_output.html";
            diagram.Save(outputPath, htmlOptions);

            // Verify that the stream provider was called for both resources (shape and image).
            if (provider.CallCount != 2)
            {
                throw new Exception($"Expected 2 stream provider calls, but received {provider.CallCount}.");
            }
            else
            {
                Console.WriteLine("IStreamProvider received correct number of resource callbacks.");
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}