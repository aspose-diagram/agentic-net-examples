using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class MasterUsageReport
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold master ID -> usage count
            Dictionary<int, int> masterUsage = new Dictionary<int, int>();

            // Initialize counts for all masters present in the document
            foreach (Master master in diagram.Masters)
            {
                masterUsage[master.ID] = 0;
            }

            // Iterate through all pages and their shapes to count master usage
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shape may be a master instance; check if it references a master
                    if (shape.Master != null)
                    {
                        int masterId = shape.Master.ID;
                        if (masterUsage.ContainsKey(masterId))
                        {
                            masterUsage[masterId]++;
                        }
                    }
                }
            }

            // Build the report text
            var reportLines = new List<string>();
            reportLines.Add("Master Usage Frequency Report");
            reportLines.Add($"Generated on: {DateTime.Now}");
            reportLines.Add(string.Empty);
            reportLines.Add("Master Name\tMaster ID\tUsage Count");
            reportLines.Add("-----------\t---------\t-----------");

            foreach (Master master in diagram.Masters)
            {
                int count = masterUsage[master.ID];
                string line = $"{master.Name}\t{master.ID}\t{count}";
                reportLines.Add(line);
            }

            string reportContent = string.Join(Environment.NewLine, reportLines);

            // Write the report to a text file
            string reportPath = "MasterUsageReport.txt";
            File.WriteAllText(reportPath, reportContent);

            // Optionally, save the diagram (demonstrating the save rule)
            string outputDiagramPath = "output.vsdx";
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Report generated: " + reportPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
