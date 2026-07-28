using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace VisioGeometryWebApi
{
    // DTO for incoming JSON payload
    public class ShapeUpdateRequest
    {
        public long ShapeId { get; set; }
        public double? PinX { get; set; }
        public double? PinY { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Angle { get; set; } // Radians
    }

    class Program
    {
        // Path to the source Visio file (adjust as needed)
        private const string SourceDiagramPath = "sample.vsdx";
        // Path where the modified diagram will be saved
        private const string OutputDiagramPath = "modified.vsdx";

        static void Main()
        {
            // Simple HTTP listener acting as a minimal web API
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/modify/");
            listener.Start();
            Console.WriteLine("Listening for POST requests at http://localhost:8080/modify/ ...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if (request.HttpMethod != "POST")
                {
                    response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    WriteResponse(response, "Only POST method is supported.");
                    continue;
                }

                try
                {
                    // Read request body
                    string requestBody;
                    using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        requestBody = reader.ReadToEnd();
                    }

                    // Deserialize JSON payload
                    ShapeUpdateRequest update = JsonSerializer.Deserialize<ShapeUpdateRequest>(requestBody);
                    if (update == null)
                        throw new Exception("Invalid JSON payload.");

                    // Load the diagram
                    Diagram diagram = new Diagram(SourceDiagramPath);
                    // Use the first page (adjust if needed)
                    Page page = diagram.Pages[0];

                    // Retrieve the target shape by ID
                    Shape shape = page.Shapes.GetShape(update.ShapeId);
                    if (shape == null)
                        throw new Exception($"Shape with ID {update.ShapeId} not found.");

                    // Apply geometry modifications if values are provided
                    if (update.PinX.HasValue)
                        shape.XForm.PinX.Value = update.PinX.Value;
                    if (update.PinY.HasValue)
                        shape.XForm.PinY.Value = update.PinY.Value;
                    if (update.Width.HasValue)
                        shape.XForm.Width.Value = update.Width.Value;
                    if (update.Height.HasValue)
                        shape.XForm.Height.Value = update.Height.Value;
                    if (update.Angle.HasValue)
                        shape.XForm.Angle.Value = update.Angle.Value; // Angle in radians

                    // Save the modified diagram
                    diagram.Save(OutputDiagramPath, SaveFileFormat.Vsdx);

                    // Respond with success
                    response.StatusCode = (int)HttpStatusCode.OK;
                    WriteResponse(response, $"Shape {update.ShapeId} updated successfully.");
                }
                catch (Exception ex)
                {
                    // Return error details
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    WriteResponse(response, $"Error: {ex.Message}");
                }
            }
        }

        // Helper method to write plain text response
        private static void WriteResponse(HttpListenerResponse response, string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/plain; charset=utf-8";
            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }
        }
    }
}