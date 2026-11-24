using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the diagram.
            Console.WriteLine("Loading diagram...");
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded successfully.");

            // Log existing data connections.
            Console.WriteLine($"Diagram contains {diagram.DataConnections.Count} data connection(s).");

            // Iterate over each data connection and update it.
            for (int i = 0; i < diagram.DataConnections.Count; i++)
            {
                var dataConn = diagram.DataConnections[i];
                Console.WriteLine($"Updating DataConnection[{i}]...");

                // Example: set a new connection string and command.
                // In a real scenario, these values would come from configuration or user input.
                string newConnectionString = "Data Source=MyServer;Initial Catalog=MyDatabase;Integrated Security=True";
                string newCommand = "SELECT * FROM MyTable";

                Console.WriteLine($"  Old ConnectionString: {dataConn.ConnectionString}");
                Console.WriteLine($"  Old Command: {dataConn.Command}");

                dataConn.ConnectionString = newConnectionString;
                dataConn.Command = newCommand;

                Console.WriteLine($"  New ConnectionString set to: {dataConn.ConnectionString}");
                Console.WriteLine($"  New Command set to: {dataConn.Command}");
            }

            // Refresh the diagram to apply the updated data connections.
            Console.WriteLine("Refreshing diagram to synchronize record sets...");
            diagram.Refresh();
            Console.WriteLine("Diagram refresh completed.");

            // Prepare PDF save options with a page‑saving callback for detailed logging.
            Console.WriteLine("Preparing PDF save options with page‑saving callback...");
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new CustomPageSavingCallback();

            // Save the diagram as PDF.
            Console.WriteLine($"Saving diagram to \"{outputPath}\"...");
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Custom callback to log page‑saving events during PDF export.
class CustomPageSavingCallback : IPageSavingCallback
{
    // Called before a page starts saving.
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page index {args.PageIndex} of {args.PageCount}");
    }

    // Called after a page has finished saving.
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"End saving page index {args.PageIndex} of {args.PageCount}");
        // Example: continue processing all pages (default behavior).
        // If you needed to stop early, you could set args.HasMorePages = false;
    }
}