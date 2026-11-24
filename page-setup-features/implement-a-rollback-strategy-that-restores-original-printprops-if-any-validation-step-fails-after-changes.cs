using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_modified.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Backup original PrintProps for each page
                var backups = new Dictionary<long, PrintPropsBackup>();
                foreach (Page page in diagram.Pages)
                {
                    // Store backup using the page ID as key
                    backups[page.ID] = new PrintPropsBackup(page.PageSheet.PrintProps);
                }

                // Apply desired modifications to PrintProps
                foreach (Page page in diagram.Pages)
                {
                    PrintProps props = page.PageSheet.PrintProps;

                    // Example modifications
                    props.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    props.ScaleX.Value = 0.8; // 80% scaling
                    props.ScaleY.Value = 0.8;
                    props.OnPage.Value = BOOL.True;
                    props.PagesX.Value = 1;
                    props.PagesY.Value = 1;
                    // Set margins to 0.5 inches (Visio uses points; 1 inch = 72 points)
                    double marginInPoints = 0.5 * 72.0;
                    props.PageTopMargin.Value = marginInPoints;
                    props.PageBottomMargin.Value = marginInPoints;
                    props.PageLeftMargin.Value = marginInPoints;
                    props.PageRightMargin.Value = marginInPoints;
                }

                // Perform validation; if any check fails, rollback changes
                bool validationFailed = false;
                foreach (Page page in diagram.Pages)
                {
                    PrintProps props = page.PageSheet.PrintProps;

                    // Validate scaling range (0.1 to 1.0)
                    if (props.ScaleX.Value < 0.1 || props.ScaleX.Value > 1.0 ||
                        props.ScaleY.Value < 0.1 || props.ScaleY.Value > 1.0)
                    {
                        Console.Error.WriteLine($"Validation failed: Scale out of range on page ID {page.ID}");
                        validationFailed = true;
                        break;
                    }

                    // Validate margins are non‑negative
                    if (props.PageTopMargin.Value < 0 || props.PageBottomMargin.Value < 0 ||
                        props.PageLeftMargin.Value < 0 || props.PageRightMargin.Value < 0)
                    {
                        Console.Error.WriteLine($"Validation failed: Negative margin on page ID {page.ID}");
                        validationFailed = true;
                        break;
                    }

                    // If Fit‑to‑Sheet is enabled, ensure PagesX and PagesY are positive
                    if (props.OnPage.Value == BOOL.True)
                    {
                        if (props.PagesX.Value <= 0 || props.PagesY.Value <= 0)
                        {
                            Console.Error.WriteLine($"Validation failed: PagesX/PagesY must be > 0 on page ID {page.ID}");
                            validationFailed = true;
                            break;
                        }
                    }
                }

                // Rollback if validation failed
                if (validationFailed)
                {
                    foreach (Page page in diagram.Pages)
                    {
                        if (backups.TryGetValue(page.ID, out PrintPropsBackup backup))
                        {
                            backup.Restore(page.PageSheet.PrintProps);
                        }
                    }
                    Console.WriteLine("Changes have been rolled back due to validation failure.");
                }
                else
                {
                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Helper class to backup and restore PrintProps values
class PrintPropsBackup
{
    public BOOL OnPage { get; }
    public double ScaleX { get; }
    public double ScaleY { get; }
    public PrintPageOrientationValue Orientation { get; }
    public double PageTopMargin { get; }
    public double PageBottomMargin { get; }
    public double PageLeftMargin { get; }
    public double PageRightMargin { get; }
    public int PagesX { get; }
    public int PagesY { get; }

    public PrintPropsBackup(PrintProps props)
    {
        // Capture current values
        OnPage = props.OnPage.Value;
        ScaleX = props.ScaleX.Value;
        ScaleY = props.ScaleY.Value;
        Orientation = props.PrintPageOrientation.Value;
        PageTopMargin = props.PageTopMargin.Value;
        PageBottomMargin = props.PageBottomMargin.Value;
        PageLeftMargin = props.PageLeftMargin.Value;
        PageRightMargin = props.PageRightMargin.Value;
        PagesX = props.PagesX.Value;
        PagesY = props.PagesY.Value;
    }

    // Restore the saved values back to the provided PrintProps instance
    public void Restore(PrintProps props)
    {
        props.OnPage.Value = OnPage;
        props.ScaleX.Value = ScaleX;
        props.ScaleY.Value = ScaleY;
        props.PrintPageOrientation.Value = Orientation;
        props.PageTopMargin.Value = PageTopMargin;
        props.PageBottomMargin.Value = PageBottomMargin;
        props.PageLeftMargin.Value = PageLeftMargin;
        props.PageRightMargin.Value = PageRightMargin;
        props.PagesX.Value = PagesX;
        props.PagesY.Value = PagesY;
    }
}