using System;
using Azure;
using Azure.AI.AnomalyDetector.Protocol;
using Azure.Core;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AnomalyDetectorClient client = new AnomalyDetectorClient(new Uri("https://<your-resource-name-here>.cognitiveservices.azure.com/"), new AzureKeyCredential("<your-key-here>"));

            // ref: https://westus2.dev.cognitive.microsoft.com/docs/services/AnomalyDetector/operations/post-timeseries-entire-detect, the request here matches the sample.
            DynamicResponse res = client.DetectEntireSeriesAsync(new
            {
                series = new[] {
                      new {
                        timestamp = "1972-01-01T00:00:00Z",
                        value = 826
                      },
                      new {
                        timestamp = "1972-02-01T00:00:00Z",
                        value = 799
                      },
                      new {
                        timestamp = "1972-03-01T00:00:00Z",
                        value = 890
                      },
                      new {
                        timestamp = "1972-04-01T00:00:00Z",
                        value = 900
                      },
                      new {
                        timestamp = "1972-05-01T00:00:00Z",
                        value = 961
                      },
                      new {
                        timestamp = "1972-06-01T00:00:00Z",
                        value = 935
                      },
                      new {
                        timestamp = "1972-07-01T00:00:00Z",
                        value = 894
                      },
                      new {
                        timestamp = "1972-08-01T00:00:00Z",
                        value = 855
                      },
                      new {
                        timestamp = "1972-09-01T00:00:00Z",
                        value = 809
                      },
                      new {
                        timestamp = "1972-10-01T00:00:00Z",
                        value = 810
                      },
                      new {
                        timestamp = "1972-11-01T00:00:00Z",
                        value = 766
                      },
                      new {
                        timestamp = "1972-12-01T00:00:00Z",
                        value = 805
                      },
                      new {
                        timestamp = "1973-01-01T00:00:00Z",
                        value = 821
                      },
                      new {
                        timestamp = "1973-02-01T00:00:00Z",
                        value = 773
                      },
                      new {
                        timestamp = "1973-03-01T00:00:00Z",
                        value = 883
                      },
                      new {
                        timestamp = "1973-04-01T00:00:00Z",
                        value = 898
                      },
                      new {
                        timestamp = "1973-05-01T00:00:00Z",
                        value = 957
                      },
                      new {
                        timestamp = "1973-06-01T00:00:00Z",
                        value = 924
                      },
                      new {
                        timestamp = "1973-07-01T00:00:00Z",
                        value = 881
                      },
                      new {
                        timestamp = "1973-08-01T00:00:00Z",
                        value = 837
                      },
                      new {
                        timestamp = "1973-09-01T00:00:00Z",
                        value = 784
                      },
                      new {
                        timestamp = "1973-10-01T00:00:00Z",
                        value = 791
                      },
                      new {
                        timestamp = "1973-11-01T00:00:00Z",
                        value = 760
                      },
                      new {
                        timestamp = "1973-12-01T00:00:00Z",
                        value = 802
                      },
                      new {
                        timestamp = "1974-01-01T00:00:00Z",
                        value = 828
                      },
                      new {
                        timestamp = "1974-02-01T00:00:00Z",
                        value = 1030
                      },
                      new {
                        timestamp = "1974-03-01T00:00:00Z",
                        value = 889
                      },
                      new {
                        timestamp = "1974-04-01T00:00:00Z",
                        value = 902
                      },
                      new {
                        timestamp = "1974-05-01T00:00:00Z",
                        value = 969
                      },
                      new {
                        timestamp = "1974-06-01T00:00:00Z",
                        value = 947
                      },
                      new {
                        timestamp = "1974-07-01T00:00:00Z",
                        value = 908
                      },
                      new {
                        timestamp = "1974-08-01T00:00:00Z",
                        value = 867
                      },
                      new {
                        timestamp = "1974-09-01T00:00:00Z",
                        value = 815
                      },
                      new {
                        timestamp = "1974-10-01T00:00:00Z",
                        value = 812
                      },
                      new {
                        timestamp = "1974-11-01T00:00:00Z",
                        value = 773
                      },
                      new {
                        timestamp = "1974-12-01T00:00:00Z",
                        value = 813
                      },
                      new {
                        timestamp = "1975-01-01T00:00:00Z",
                        value = 834
                      },
                      new {
                        timestamp = "1975-02-01T00:00:00Z",
                        value = 782
                      },
                      new {
                        timestamp = "1975-03-01T00:00:00Z",
                        value = 892
                      },
                      new {
                        timestamp = "1975-04-01T00:00:00Z",
                        value = 903
                      },
                      new {
                        timestamp = "1975-05-01T00:00:00Z",
                        value = 966
                      },
                      new {
                        timestamp = "1975-06-01T00:00:00Z",
                        value = 937
                      },
                      new {
                        timestamp = "1975-07-01T00:00:00Z",
                        value = 896
                      },
                      new {
                        timestamp = "1975-08-01T00:00:00Z",
                        value = 858
                      },
                      new {
                        timestamp = "1975-09-01T00:00:00Z",
                        value = 817
                      },
                      new {
                        timestamp = "1975-10-01T00:00:00Z",
                        value = 827
                      },
                      new {
                        timestamp = "1975-11-01T00:00:00Z",
                        value = 797
                      },
                      new {
                        timestamp = "1975-12-01T00:00:00Z",
                        value = 843
                      }
                },
                granularity = "monthly",
                maxAnomalyRatio = 0.25,
                sensitivity = 95,
            }).GetAwaiter().GetResult();

            // TODO(matell): This is kinda tedious, I think we need to be able to make this cleaner.
            if (res.GetRawResponse().Status == 200)
            {
                foreach (DynamicJson isAnomaly in res.Content.isAnomaly)
                {
                    Console.WriteLine(isAnomaly.GetBoolean());
                }
            }
            else if (res.GetRawResponse().Status == 400)
            {
                Console.WriteLine($"Bad request code: {res.Content.code}, message: {res.Content.message}");
            }
        }
    }
}
