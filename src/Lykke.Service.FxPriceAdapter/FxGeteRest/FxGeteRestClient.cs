using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lykke.Service.FxPriceAdapter.FxGeteRest
{
    public class FxGeteRestClient: IDisposable
    {
        private readonly string _dataurl;
        private readonly HttpClient _client;

        public FxGeteRestClient(string dataurl)
        {
            _dataurl = dataurl;
            _client = new HttpClient();
        }

        public async Task<MarketProfileResult> GetMarketProfileAsync()
        {
            var json = await _client.GetStringAsync(_dataurl);
            var data = JsonConvert.DeserializeObject<MarketProfile>(json);
            if (data?.Result == null)
            {
                throw new FxGeteRestException("Receive incorect data from FxGateway", json);
            }
            return data.Result;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }

    public interface IFxGeteRestClient
    {
        Task<MarketProfileResult> GetMarketProfileAsync();
    }
}
