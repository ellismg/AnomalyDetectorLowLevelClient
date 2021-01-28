using Azure.Core;

namespace Azure.AI.AnomalyDetector.Protocol
{
    // TODO(matell): `ClientOptions` is abstract today, so we have this type for ease of use.
    public class AnomalyDetectorClientOptions : ClientOptions
    {
        public AnomalyDetectorClientOptions() { }
    }
}
