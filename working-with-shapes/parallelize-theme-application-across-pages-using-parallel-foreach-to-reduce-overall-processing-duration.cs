using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the preset quick style to apply to each page
            PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle1;

            // Apply the preset quick style to all pages in parallel
            Parallel.ForEach(diagram.Pages, page =>
            {
                // Set the preset theme quick style for the current page
                page.PresetThemeQuickStyle = quickStyle;

                // Optionally apply additional style settings (text, line, fill)
                // Using -1 retains default values; adjust as needed
                page.ApplyStyle(-1, -1, -1);
            });

            // Save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
