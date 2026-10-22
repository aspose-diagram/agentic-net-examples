using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [UserName]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine the replacement name: from argument or console prompt.
        string userName;
        if (args.Length >= 3 && !string.IsNullOrWhiteSpace(args[2]))
        {
            userName = args[2];
        }
        else
        {
            Console.Write("Enter the name to replace \"[Name]\": ");
            userName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.Error.WriteLine("Error: No name provided for replacement.");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each text run (FormatTxt) within the shape.
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Cast to Txt to access the actual text string.
                        Txt txtRun = fmt as Txt;
                        if (txtRun != null && txtRun.Text != null && txtRun.Text.Contains("[Name]"))
                        {
                            // Replace the placeholder with the actual user name.
                            txtRun.Text = txtRun.Text.Replace("[Name]", userName);
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}