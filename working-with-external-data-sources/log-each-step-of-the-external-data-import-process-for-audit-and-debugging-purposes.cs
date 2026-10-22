using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output paths
        string outputPdfPath = "output.pdf";
        string outputVsdxPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Console.WriteLine($"Loading diagram from '{inputPath}'...");
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine($"Diagram loaded. Page count: {diagram.Pages.Count}");

            // Log external data connections before modification
            Console.WriteLine($"External data connections count: {diagram.DataConnections.Count}");

            if (diagram.DataConnections.Count > 0)
            {
                // Update the first data connection as an example
                var dataConn = diagram.DataConnections[0];
                Console.WriteLine("Updating first external data connection...");

                // Example connection string and command (replace with real values as needed)
                string newConnectionString = "Data Source=ServerName;Initial Catalog=DatabaseName;Integrated Security=True";
                string newCommand = "SELECT * FROM SampleTable";

                Console.WriteLine($"Setting ConnectionString to: {newConnectionString}");
                dataConn.ConnectionString = newConnectionString;

                Console.WriteLine($"Setting Command to: {newCommand}");
                dataConn.Command = newCommand;
            }
            else
            {
                Console.WriteLine("No external data connections found to update.");
            }

            // Refresh the diagram to apply data changes
            Console.WriteLine("Refreshing diagram to synchronize record sets with updated data source...");
            diagram.Refresh();
            Console.WriteLine("Diagram refresh completed.");

            // Save to PDF with page saving callback for detailed logging
            Console.WriteLine($"Saving diagram to PDF at '{outputPdfPath}' with page saving callback...");
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new PageSavingLogger();
            diagram.Save(outputPdfPath, pdfOptions);
            Console.WriteLine("PDF save completed.");

            // Save the updated diagram back to VSDX format
            Console.WriteLine($"Saving updated diagram to VSDX at '{outputVsdxPath}'...");
            diagram.Save(outputVsdxPath, SaveFileFormat.Vsdx);
            Console.WriteLine("VSDX save completed.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Implementation of IPageSavingCallback to log page saving progress
class PageSavingLogger : IPageSavingCallback
{
    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page index {args.PageIndex} of pages {args.PageCount}");
    }

    // Called after a page has finished saving
    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"End saving page index {args.PageIndex} of pages {args.PageCount}");
    }
}