using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to an existing Visio diagram (template)
            string diagramPath = "template.vsdx";

            // Measure memory before loading the diagram
            long memBeforeDiagram = GC.GetTotalMemory(true);
            Diagram diagram = new Diagram(diagramPath);
            long memAfterDiagram = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory used to load diagram: {memAfterDiagram - memBeforeDiagram} bytes");

            // Path to a large external CSV file
            string csvPath = "largeData.csv";

            // Create a DataRecordSet and fill it with ADO XML built from the CSV
            DataRecordSet dataRecordSet = new DataRecordSet
            {
                Name = "LargeDataSet",
                ID = 1,
                ADOData = BuildAdoXmlFromCsv(csvPath)
            };

            // Measure memory before adding the dataset to the diagram
            long memBeforeAdd = GC.GetTotalMemory(true);
            diagram.DataRecordSets.Add(dataRecordSet);
            long memAfterAdd = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory added by dataset: {memAfterAdd - memBeforeAdd} bytes");

            // Optional: save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to convert a CSV file into simple ADO XML required by DataRecordSet.ADOData
    static string BuildAdoXmlFromCsv(string csvPath)
    {
        using (var reader = new StreamReader(csvPath))
        {
            string headerLine = reader.ReadLine();
            if (headerLine == null) throw new InvalidOperationException("CSV file is empty.");

            string[] columns = headerLine.Split(',');

            var xmlBuilder = new StringBuilder();
            xmlBuilder.AppendLine("<xml>");
            xmlBuilder.AppendLine("<schema>");
            foreach (var col in columns)
            {
                xmlBuilder.AppendLine($"<column name=\"{col}\" type=\"string\"/>");
            }
            xmlBuilder.AppendLine("</schema>");
            xmlBuilder.AppendLine("<data>");

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                xmlBuilder.AppendLine("<row>");
                for (int i = 0; i < columns.Length; i++)
                {
                    string safeValue = i < values.Length ? values[i] : string.Empty;
                    xmlBuilder.AppendLine($"<{columns[i]}>{SecurityElement.Escape(safeValue)}</{columns[i]}>");
                }
                xmlBuilder.AppendLine("</row>");
            }

            xmlBuilder.AppendLine("</data>");
            xmlBuilder.AppendLine("</xml>");

            return xmlBuilder.ToString();
        }
    }
}
