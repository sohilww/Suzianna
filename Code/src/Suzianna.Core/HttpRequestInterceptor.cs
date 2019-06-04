using System.Collections;
using System.Net.Http;

namespace Suzianna.Core
{
    public static class HttpRequestMessageExtension
    {
        public static void ProcessInterceptor(this HttpRequestMessage requestMessage)
        {
            var service = ServiceLocator.GetService<IHttpRequestInterceptor>();
            service?.Process(requestMessage);

        }
    }
    public interface IHttpRequestInterceptor
    {
        void Process(HttpRequestMessage requestMessage);
    }
}