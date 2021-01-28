using Azure.Core;
using Azure.Core.Pipeline;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.AI.AnomalyDetector.Protocol
{
    public class AnomalyDetectorClient
    {
        public virtual Uri Endpoint { get; }
        private HttpPipeline HttpPipeline { get; }

        private static readonly Encoding Utf8NoBom = new UTF8Encoding(false, true);
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private const string AuthorizationHeader = "Ocp-Apim-Subscription-Key";

        protected AnomalyDetectorClient()
        {
        }

        public AnomalyDetectorClient(Uri endpoint, AzureKeyCredential credential) : this(endpoint, credential, new AnomalyDetectorClientOptions())
        {
        }

        public AnomalyDetectorClient(Uri endpoint, AzureKeyCredential credential, AnomalyDetectorClientOptions options)
        {
            if (endpoint == null)
            { 
                throw new ArgumentNullException(nameof(endpoint)); 
            }
            
            if (credential == null)
            { 
                throw new ArgumentNullException(nameof(credential)); 
            }

            Endpoint = endpoint;
            HttpPipeline = HttpPipelineBuilder.Build(options, new AzureKeyCredentialPolicy(credential, AuthorizationHeader));
        }

        // DESIGN(matell): Should we be modeling the body parameter as "dynamic" and not "object" in the input case like this?
        public virtual async Task<DynamicResponse> DetectEntireSeriesAsync(object body, CancellationToken cancellationToken = default) {
            DynamicRequest req = DetectEntireSeriesRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return await req.SendAsync(cancellationToken).ConfigureAwait(false);
        }
        public virtual  DynamicResponse DetectEntireSeries(object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectEntireSeriesRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return req.Send(cancellationToken);
        }
        // NOTE(matell): The `body` parameter here is elided, because the expectation is when you are using this overload to construct a DynamicRequest, you're always
        // go to set the body explicitly on the object (to take advantage of the IntelliSense magic that comes from DynamicRequest). If there were query or path parameters,
        // we should include them them as arguments to this method, to build out as much of the request as we can.
        public virtual DynamicRequest DetectEntireSeriesRequest() 
        {
            Request req = HttpPipeline.CreateRequest();
            req.Method = RequestMethod.Post;

            RequestUriBuilder uriBuilder = new RequestUriBuilder();
            uriBuilder.Reset(Endpoint);
            uriBuilder.AppendPath("anomalydetector/v1.0/timeseries/entire/detect", false);

            req.Uri = uriBuilder;
            req.Headers.SetValue("Content-Type", "application/json");

            return new DynamicRequest(req, HttpPipeline);
        }

        public virtual  async Task<DynamicResponse> DetectLastPointAsync(object body, CancellationToken cancellationToken = default) 
        {
            DynamicRequest req = DetectLastPointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return await req.SendAsync(cancellationToken).ConfigureAwait(false);
        }
        public virtual DynamicResponse DetectLastPoint(object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectLastPointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return req.Send(cancellationToken);
        }
        public virtual DynamicRequest DetectLastPointRequest()
        {
            Request req = HttpPipeline.CreateRequest();
            req.Method = RequestMethod.Post;

            RequestUriBuilder uriBuilder = new RequestUriBuilder();
            uriBuilder.Reset(Endpoint);
            uriBuilder.AppendPath("anomalydetector/v1.0/timeseries/last/detect", false);

            req.Uri = uriBuilder;
            req.Headers.SetValue("Content-Type", "application/json");

            return new DynamicRequest(req, HttpPipeline);
        }

        public virtual  async Task<DynamicResponse> DetectChangePointAsync(object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectChangePointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return await req.SendAsync(cancellationToken).ConfigureAwait(false);
        }
        public virtual  DynamicResponse DetectChangePoint(object body, CancellationToken cancellationToken = default) {
            DynamicRequest req = DetectChangePointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return req.Send(cancellationToken);
        }
        public virtual DynamicRequest DetectChangePointRequest()
        {
            Request req = HttpPipeline.CreateRequest();
            req.Method = RequestMethod.Post;

            RequestUriBuilder uriBuilder = new RequestUriBuilder();
            uriBuilder.Reset(Endpoint);
            uriBuilder.AppendPath("anomalydetector/v1.0/timeseries/changepoint/detect", false);

            req.Uri = uriBuilder;
            req.Headers.SetValue("Content-Type", "application/json");

            return new DynamicRequest(req, HttpPipeline);
        }
    }
}
