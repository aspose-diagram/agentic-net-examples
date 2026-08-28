using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (template)
            string visioPath = "template.vsdx";

            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Measure memory before loading the diagram
            long beforeDiagramLoad = GC.GetTotalMemory(true);

            // Load diagram using the constructor (lifecycle rule)
            Diagram diagram = new Diagram(visioPath);

            // Measure memory after loading the diagram
            long afterDiagramLoad = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used to load diagram: {afterDiagramLoad - beforeDiagramLoad} bytes");

            // Measure memory before loading the external dataset
            long beforeDataLoad = GC.GetTotalMemory(true);

            // Load a large external dataset into a DataRecordSet
            DataRecordSet dataRecordSet = LoadLargeDataRecordSet("largeData.csv");

            // Measure memory after loading the dataset
            long afterDataLoad = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used to load dataset: {afterDataLoad - beforeDataLoad} bytes");

            // Add the DataRecordSet to the diagram
            diagram.DataRecordSets.Add(dataRecordSet);

            // Refresh the diagram to apply any automatic linking (optional)
            diagram.Refresh();

            // Measure memory before saving the diagram
            long beforeSave = GC.GetTotalMemory(true);

            // Save diagram using the Save method (lifecycle rule)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Measure memory after saving the diagram
            long afterSave = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory change during save: {afterSave - beforeSave} bytes");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to load a CSV file into a DataRecordSet
    static DataRecordSet LoadLargeDataRecordSet(string csvPath)
    {
        // Create a new DataRecordSet instance
        DataRecordSet drs = new DataRecordSet();

        // Assign a friendly name
        drs.Name = Path.GetFileNameWithoutExtension(csvPath);

        // Read all lines from the CSV (for demonstration; streaming is preferable for very large files)
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0)
            return drs;

        // First line contains column headers
        string[] headers = lines[0].Split(',');

        // Build ADO XML representation required by Aspose.Diagram
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<xml>");
        sb.AppendLine("<schema>");
        foreach (string header in headers)
        {
            sb.AppendLine($"<column name=\"{header}\" type=\"string\"/>");
        }
        sb.AppendLine("</schema>");
        sb.AppendLine("<data>");
        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(',');
            sb.Append("<row>");
            for (int j = 0; j < headers.Length; j++)
            {
                // Escape XML special characters
                string value = System.Security.SecurityElement.Escape(fields[j]);
                sb.Append($"<field>{value}</field>");
            }
            sb.AppendLine("</row>");
        }
        sb.AppendLine("</data>");
        sb.AppendLine("</xml>");

        // Assign the generated XML to the DataRecordSet
        drs.ADOData = sb.ToString();

        return drs;
    }
}
