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

        // TODO(matell): Should we be modeling the body parameter as "dynamic" and not "object" in the input case like this?  Will we have to use DynamicSchema?
        // TOOD(matell): This schema modesl the "success" case, since in an error case you get an error object in the body instead of the resource you requested.
        //               how do we encode that?
        [return: DynamicSchema(Schemas.DetectEntireSeriesOutputSchema)]
        public virtual async Task<DynamicResponse> DetectEntireSeriesAsync([DynamicSchema(Schemas.DetectEntireSeriesInputSchema)] object body, CancellationToken cancellationToken = default) {
            DynamicRequest req = DetectEntireSeriesRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return await req.SendAsync(cancellationToken).ConfigureAwait(false);
        }

        [return: DynamicSchema(Schemas.DetectEntireSeriesOutputSchema)]
        public virtual  DynamicResponse DetectEntireSeries([DynamicSchema(Schemas.DetectEntireSeriesInputSchema)] object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectEntireSeriesRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return req.Send(cancellationToken);
        }

        // NOTE(matell): The `body` parameter here is elided, because the expectation is when you are using this overload to construct a DynamicRequest, you're always
        // go to set the body explicitly on the object (to take advantage of the IntelliSense magic that comes from DynamicRequest). If there were query or path parameters,
        // we should include them them as arguments to this method, to build out as much of the request as we can.
        //
        // TODO(matell): When you invoke the returned dynamic request you're going to get a response who's dynamic parameter follows the schmea of Schemas.DetectLastPointOutputSchema
        //               but how to we actually encode that in a way that it can be used by tooling??
        [return: DynamicSchema(Schemas.DetectEntireSeriesInputSchema)]
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

        [return: DynamicSchema(Schemas.DetectLastPointOutputSchema)]
        public virtual  async Task<DynamicResponse> DetectLastPointAsync([DynamicSchema(Schemas.DetectLastPointInputSchema)] object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectLastPointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return await req.SendAsync(cancellationToken).ConfigureAwait(false);
        }

        [return: DynamicSchema(Schemas.DetectLastPointOutputSchema)]
        public virtual DynamicResponse DetectLastPoint([DynamicSchema(Schemas.DetectLastPointInputSchema)] object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectLastPointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return req.Send(cancellationToken);
        }

        [return: DynamicSchema(Schemas.DetectLastPointInputSchema)]
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

        [return: DynamicSchema(Schemas.DetectChangePointOutputSchema)]
        public virtual  async Task<DynamicResponse> DetectChangePointAsync([DynamicSchema(Schemas.DetectChangePointInputSchema)] object body, CancellationToken cancellationToken = default)
        {
            DynamicRequest req = DetectChangePointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return await req.SendAsync(cancellationToken).ConfigureAwait(false);
        }

        [return: DynamicSchema(Schemas.DetectChangePointOutputSchema)]
        public virtual  DynamicResponse DetectChangePoint([DynamicSchema(Schemas.DetectChangePointInputSchema)] object body, CancellationToken cancellationToken = default) {
            DynamicRequest req = DetectChangePointRequest();
            req.Content = RequestContent.Create(Utf8NoBom.GetBytes(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions)));

            return req.Send(cancellationToken);
        }

        [return: DynamicSchema(Schemas.DetectChangePointInputSchema)]
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

        // TODO(matell): There may be some bugs in these schemas, rebuild them from what's in the swagger in GitHub vs what was shown in the API docs (which did not have models for responses!)
        private static class Schemas 
        {
            public const string DetectLastPointInputSchema = "{\"type\": \"object\",\"required\": [\"granularity\",\"series\"],\"properties\": {\"series\": {\"type\": \"array\",\"items\": {\"type\": \"object\",\"required\": [\"timestamp\",\"value\"],\"properties\": {\"timestamp\": {\"type\": \"string\",\"format\": \"date-time\"},\"value\": {\"type\": \"number\",\"format\": \"float\"}}}},\"granularity\": {\"type\": \"string\"},\"customInterval\": {\"type\": \"integer\"},\"period\": {\"type\": \"integer\",\"format\": \"int32\"},\"maxAnomalyRatio\": {\"type\": \"number\",\"format\": \"float\"},\"sensitivity\": {\"type\": \"number\",\"format\": \"integer\"}}}";
            public const string DetectLastPointOutputSchema = "{ \"type\": \"object\", \"required\": [ \"isAnomaly\", \"isPositiveAnomaly\", \"isNegativeAnomaly\", \"period\", \"expectedValue\", \"upperMargin\", \"lowerMargin\", \"suggestedWindow\" ], \"properties\": { \"isAnomaly\": { \"type\": \"boolean\" }, \"isPositiveAnomaly\": { \"type\": \"boolean\" }, \"isNegativeAnomaly\": { \"type\": \"boolean\" }, \"period\": { \"type\": \"integer\" }, \"expectedValue\": { \"type\": \"number\" }, \"upperMargin\": { \"type\": \"number\" }, \"lowerMargin\": { \"type\": \"number\" }, \"suggestedWindow\": { \"type\": \"integer\" } } }";
            public const string DetectEntireSeriesInputSchema = "{ \"type\": \"object\", \"required\": [ \"granularity\", \"series\" ], \"properties\": { \"series\": { \"type\": \"array\", \"items\": { \"type\": \"object\", \"required\": [ \"timestamp\", \"value\" ], \"properties\": { \"timestamp\": { \"type\": \"string\", \"format\": \"date-time\" }, \"value\": { \"type\": \"number\", \"format\": \"float\" } } } }, \"granularity\": { \"type\": \"string\" }, \"customInterval\": { \"type\": \"integer\" }, \"period\": { \"type\": \"integer\", \"format\": \"int32\" }, \"maxAnomalyRatio\": { \"type\": \"number\", \"format\": \"float\" }, \"sensitivity\": { \"type\": \"number\", \"format\": \"integer\" } } }";
            public const string DetectEntireSeriesOutputSchema = "{\"type\": \"object\",\"required\": [\"expectedValues\",\"upperMargins\",\"lowerMargins\",\"isAnomaly\",\"isPositiveAnomaly\",\"isNegativeAnomaly\",\"period\"],\"properties\": {\"expectedValues\": {\"type\": \"array\",\"items\":{\"type\": \"number\"}},\"upperMargins\": {\"type\": \"array\",\"items\":{\"type\": \"number\"}},\"lowerMargins\": {\"type\": \"array\",\"items\":{\"type\": \"number\"}},\"isAnomaly\": {\"type\": \"array\",\"items\":{\"type\": \"boolean\"}},\"isPositiveAnomaly\": {\"type\": \"array\",\"items\":{\"type\": \"boolean\"}},\"isNegativeAnomaly\": {\"type\": \"array\",\"items\":{\"type\": \"boolean\"}},\"period\": {\"type\": \"integer\"}}}";
            public const string DetectChangePointInputSchema = "{\"type\": \"object\",\"required\": [\"granularity\",\"series\"],\"properties\": {\"series\": {\"type\": \"array\",\"items\": {\"type\": \"object\",\"required\": [\"timestamp\",\"value\"],\"properties\": {\"timestamp\": {\"type\": \"string\",\"format\": \"date-time\"},\"value\": {\"type\": \"number\",\"format\": \"float\"}}}},\"granularity\": {\"type\": \"string\",\"enum\": [\"yearly\",\"monthly\",\"weekly\",\"daily\",\"hourly\",\"minutely\",\"secondly\"]},\"customInterval\": {\"type\": \"integer\",\"format\": \"int32\"},\"period\": {\"type\": \"integer\",\"format\": \"int32\",},\"stableTrendWindow\": {\"type\": \"integer\",\"format\": \"int32\",},\"threshold\": {\"type\": \"number\",\"format\": \"float\",}}}";
            public const string DetectChangePointOutputSchema = "{\"type\": \"object\",\"required\": [\"isChangePoint\",\"confidenceScores\",\"period\"],\"properties\": {\"period\": {\"type\": \"integer\",\"format\": \"int32\"},\"isChangePoint\": {\"type\": \"array\",\"items\": {\"type\": \"boolean\"}},\"confidenceScores\": {\"type\": \"array\",\"items\": {\"type\": \"number\",\"format\": \"float\"}}}}";
        }
    }
}
