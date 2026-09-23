using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

// Simple POCO representing the OData entity
    public class ShapeData
    {
        public string ShapeId { get; set; }   // Corresponds to shape's NameU or ID
        public string DataField { get; set; } // Value to store in shape's data field
    }

    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Load existing diagram (lifecycle rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve external data from OData feed (free‑form implementation)
            List<ShapeData> externalData = await GetODataShapeDataAsync("https://example.com/odata/Shapes");

            // Synchronize external data with diagram shape data fields
            foreach (ShapeData item in externalData)
            {
                // Find shape by its NameU (or you could use ID)
                Shape shape = FindShapeByName(diagram, item.ShapeId);
                if (shape != null)
                {
                    // Set the first custom data field (Data1) – adjust as needed
                    shape.Data1 = item.DataField;
                }
            }

            // Save the updated diagram (lifecycle rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }

        // Helper: fetch OData feed and deserialize to a list of ShapeData
        private static async System.Threading.Tasks.Task<List<ShapeData>> GetODataShapeDataAsync(string requestUri)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            // Assuming OData returns an array of objects with properties matching ShapeData
            return JsonSerializer.Deserialize<List<ShapeData>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        // Helper: locate a shape by its NameU property
        private static Shape FindShapeByName(Diagram diagram, string nameU)
        {
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                if (string.Equals(shape.NameU, nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
            return null;
        }
    }