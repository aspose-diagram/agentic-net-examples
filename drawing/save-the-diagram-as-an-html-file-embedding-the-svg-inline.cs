using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class InlineSvgProvider : IStreamProvider
{
    public void InitStream(StreamProviderOptions options)
    {
        // No custom stream handling required for inline SVG export.
    }

    public void CloseStream(StreamProviderOptions options)
    {
        // No resources to release.
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            string inputPath = "input.vsdx"; // replace with your diagram file path
            Diagram diagram = new Diagram(inputPath);

            // Set up HTML save options and assign the custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new InlineSvgProvider();

            // Save the diagram as an HTML file with SVG content embedded inline.
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}