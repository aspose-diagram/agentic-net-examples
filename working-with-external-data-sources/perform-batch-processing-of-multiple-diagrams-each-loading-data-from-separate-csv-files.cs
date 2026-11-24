using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder containing diagrams and CSV files, and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Get all Visio VSDX files in the input folder.
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No diagram files found in the input folder.");
            return;
        }

        foreach (string diagramPath in diagramFiles)
        {
            // Guard for diagram file existence (should always be true here).
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
                continue;
            }

            // Determine corresponding CSV file (same base name with .csv extension).
            string baseName = Path.GetFileNameWithoutExtension(diagramPath);
            string csvPath = Path.Combine(inputFolder, baseName + ".csv");

            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"CSV file not found for diagram '{baseName}'. Expected at: {csvPath}");
                continue; // Skip this diagram.
            }

            // Load CSV data into a DataTable.
            DataTable csvTable = LoadCsvToDataTable(csvPath);
            if (csvTable == null || csvTable.Rows.Count == 0)
            {
                Console.Error.WriteLine($"CSV file '{csvPath}' is empty or could not be parsed.");
                continue;
            }

            // Load the Visio diagram inside a using block.
            try
            {
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Process each page and shape.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Match shape name with CSV column names (case‑insensitive).
                            foreach (System.Data.DataColumn column in csvTable.Columns)
                            {
                                if (string.Equals(shape.Name, column.ColumnName, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Get the value from the first row.
                                    string newText = csvTable.Rows[0][column].ToString();

                                    // Update shape's text: clear existing runs and add a new one.
                                    shape.Text.Value.Clear();
                                    shape.Text.Value.Add(new Txt(newText));
                                }
                            }
                        }
                    }

                    // Optional: remove VBA project data if present.
                    diagram.VbProjectData = null;

                    // Save the modified diagram as PDF.
                    string outputPdfPath = Path.Combine(outputFolder, baseName + ".pdf");
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        DefaultFont = "Arial"
                    };

                    diagram.Save(outputPdfPath, pdfOptions);
                    Console.WriteLine($"Processed and saved PDF: {outputPdfPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. Assumes the first line contains headers.
    /// </summary>
    /// <param name="csvPath">Path to the CSV file.</param>
    /// <returns>DataTable with CSV data, or null on failure.</returns>
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return null;
        }

        DataTable table = new DataTable();
        try
        {
            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
                return table; // Empty table.

            // Parse header.
            string[] headers = SplitCsvLine(lines[0]);
            foreach (string header in headers)
            {
                table.Columns.Add(header.Trim());
            }

            // Parse data rows.
            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = SplitCsvLine(lines[i]);
                // Ensure the row has the same number of columns.
                if (fields.Length != table.Columns.Count)
                    continue; // Skip malformed rows.

                DataRow row = table.NewRow();
                for (int j = 0; j < fields.Length; j++)
                {
                    row[j] = fields[j].Trim();
                }
                table.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read CSV file '{csvPath}': {ex.Message}");
            return null;
        }

        return table;
    }

    /// <summary>
    /// Simple CSV line splitter handling commas. Does not handle quoted commas.
    /// </summary>
    private static string[] SplitCsvLine(string line)
    {
        return line.Split(new char[] { ',' }, StringSplitOptions.None);
    }
}