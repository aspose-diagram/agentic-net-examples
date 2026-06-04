using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioConversion
{
    static void Main()
    {
        try
        {

            // Path to the encrypted Visio file
            const string inputPath = "encrypted_input.vsd";
            // Path for the converted output file
            const string outputPath = "converted_output.vdx";

            // Open the encrypted Visio file as a stream
            using (FileStream inputStream = File.OpenRead(inputPath))
            {
                // Prepare load options – specify format if needed and set the password
                LoadOptions loadOptions = new LoadOptions(LoadFileFormat.Vsd);
                // If the LoadOptions class supports a Password property, set it here
                // loadOptions.Password = "yourPassword"; // Uncomment and set the actual password

                // Load the diagram from the encrypted stream using the load options
                Diagram diagram = new Diagram(inputStream, loadOptions);

                // Save the diagram in the desired format (e.g., VDX)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
