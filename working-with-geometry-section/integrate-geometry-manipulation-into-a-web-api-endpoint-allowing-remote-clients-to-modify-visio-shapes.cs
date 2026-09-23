using System;
using System.Net;
using System.Text;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace VisioGeometryApi
{
    // DTO for incoming JSON payload
    public class ShapeGeometryRequest
    {
        public long ShapeId { get; set; }
        public double? PinX { get; set; }
        public double? PinY { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Angle { get; set; } // degrees
    }

    class Program
    {
        // Path to the Visio file to be manipulated
        private const string DiagramPath = "input.vsdx";
        // Path where the modified diagram will be saved
        private const string OutputPath = "output.vsdx";

        static void Main()
        {
            // Load the diagram (create if not exists)
            Diagram diagram;
            try
            {
                diagram = new Diagram(DiagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Start a simple HTTP listener
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/modify/");
            try
            {
                listener.Start();
                Console.WriteLine("Visio Geometry API listening on http://localhost:5000/modify/");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start HttpListener: {ex.Message}");
                return;
            }

            // Process requests asynchronously
            while (true)
            {
                HttpListenerContext context = listener.GetContext(); // blocking call
                _ = ProcessRequestAsync(context, diagram);
            }
        }

        private static async System.Threading.Tasks.Task ProcessRequestAsync(HttpListenerContext context, Diagram diagram)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request.HttpMethod != "POST")
            {
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                response.Close();
                return;
            }

            // Read request body
            string requestBody;
            using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            ShapeGeometryRequest payload;
            try
            {
                payload = JsonSerializer.Deserialize<ShapeGeometryRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (payload == null)
                    throw new Exception("Deserialized payload is null.");
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                byte[] errorBytes = Encoding.UTF8.GetBytes($"Invalid JSON payload: {ex.Message}");
                response.OutputStream.Write(errorBytes, 0, errorBytes.Length);
                response.Close();
                return;
            }

            // Find the shape across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    targetShape = page.Shapes.GetShape(payload.ShapeId);
                    if (targetShape != null)
                        break;
                }
                catch
                {
                    // Shape not on this page, continue searching
                }
            }

            if (targetShape == null)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                byte[] notFoundBytes = Encoding.UTF8.GetBytes($"Shape with ID {payload.ShapeId} not found.");
                response.OutputStream.Write(notFoundBytes, 0, notFoundBytes.Length);
                response.Close();
                return;
            }

            // Apply geometry changes if provided
            if (payload.PinX.HasValue)
                targetShape.XForm.PinX.Value = payload.PinX.Value;
            if (payload.PinY.HasValue)
                targetShape.XForm.PinY.Value = payload.PinY.Value;
            if (payload.Width.HasValue)
                targetShape.XForm.Width.Value = payload.Width.Value;
            if (payload.Height.HasValue)
                targetShape.XForm.Height.Value = payload.Height.Value;
            if (payload.Angle.HasValue)
                targetShape.XForm.Angle.Value = payload.Angle.Value; // degrees

            // Save the updated diagram
            try
            {
                diagram.Save(OutputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                byte[] saveErrorBytes = Encoding.UTF8.GetBytes($"Failed to save diagram: {ex.Message}");
                response.OutputStream.Write(saveErrorBytes, 0, saveErrorBytes.Length);
                response.Close();
                return;
            }

            // Respond with success
            response.StatusCode = (int)HttpStatusCode.OK;
            byte[] successBytes = Encoding.UTF8.GetBytes($"Shape {payload.ShapeId} updated successfully.");
            response.OutputStream.Write(successBytes, 0, successBytes.Length);
            response.Close();
        }
    }
}