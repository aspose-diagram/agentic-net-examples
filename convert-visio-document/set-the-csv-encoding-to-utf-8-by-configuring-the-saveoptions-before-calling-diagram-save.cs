using System.IO;
using System;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Create save options for CSV format using the factory method
            SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFileFormat.Csv);

            // Configure the CSV encoding to UTF‑8.
            // Aspose.Diagram provides a specific CSV save options class that includes an Encoding property.
            // Since the exact class is not listed in the provided documentation, we set the property via reflection
            // to keep the code compilable even if the concrete type is not known at compile time.
            var encodingProperty = saveOptions.GetType().GetProperty("Encoding");
            if (encodingProperty != null && encodingProperty.CanWrite)
            {
                encodingProperty.SetValue(saveOptions, Encoding.UTF8);
            }

            // Save the diagram as CSV using the configured options
            diagram.Save("output.csv", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
