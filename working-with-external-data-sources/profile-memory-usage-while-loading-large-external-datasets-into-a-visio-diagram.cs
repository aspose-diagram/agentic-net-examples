using System.IO;
using System;
using System.Data;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths (adjust as needed)
            string diagramPath = "largeDiagram.vsdx";
            string outputPath = "largeDiagram_Processed.vsdx";

            // Helper to get current memory usage (in MB)
            long GetMemoryUsage()
            {
                Process proc = Process.GetCurrentProcess();
                proc.Refresh();
                return proc.PrivateMemorySize64 / (1024 * 1024);
            }

            // Initial memory snapshot
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memBefore = GetMemoryUsage();
            Console.WriteLine($"Memory before loading data: {memBefore} MB");

            // Simulate loading a large external dataset (e.g., 200,000 rows)
            DataTable dataTable = new DataTable("LargeData");
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Value", typeof(double));

            const int rowCount = 200_000;
            for (int i = 1; i <= rowCount; i++)
            {
                dataTable.Rows.Add(i, $"Item_{i}", i * 0.1);
            }

            // Memory after dataset creation
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memAfterData = GetMemoryUsage();
            Console.WriteLine($"Memory after creating dataset ({rowCount} rows): {memAfterData} MB");

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Memory after diagram load
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memAfterDiagram = GetMemoryUsage();
            Console.WriteLine($"Memory after loading diagram: {memAfterDiagram} MB");

            // Example: add a shape for each data row on the first page (limited to first 1000 for demo)
            Page page = diagram.Pages[0];
            double startX = 1.0;
            double startY = 1.0;
            double offsetX = 2.0;
            double offsetY = 1.0;
            int maxShapes = 1000; // avoid excessive shape count in demo

            for (int i = 0; i < Math.Min(maxShapes, dataTable.Rows.Count); i++)
            {
                // Simple rectangle shape
                long shapeId = page.DrawRectangle(startX + (i % 10) * offsetX,
                                                  startY + (i / 10) * offsetY,
                                                  1.5, 0.8);
                Shape shape = page.Shapes.GetShape(shapeId);
                // Add text from dataset
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"{dataTable.Rows[i]["Name"]}: {dataTable.Rows[i]["Value"]}"));
            }

            // Memory after adding shapes
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memAfterShapes = GetMemoryUsage();
            Console.WriteLine($"Memory after adding shapes: {memAfterShapes} MB");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

            // Final memory snapshot
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long memFinal = GetMemoryUsage();
            Console.WriteLine($"Final memory usage: {memFinal} MB");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
