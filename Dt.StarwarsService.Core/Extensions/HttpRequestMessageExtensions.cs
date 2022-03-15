using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dt.StarwarsService.Core.Extensions
{
    /// <summary>
    /// Extensions to HttpRequestMessage
    ///     <seealso cref="HttpRequestMessage"/>
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Used for recreating an HTTP Request after it has already been sent for retry logic.
        /// </summary>
        /// <param name="originalRequest"></param>
        /// <returns></returns>
        public static HttpRequestMessage Clone(this HttpRequestMessage originalRequest)
        {
            var httpRequestMessage = new HttpRequestMessage(originalRequest.Method, originalRequest.RequestUri);
            
            foreach (var header in originalRequest.Headers)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            foreach (var item in originalRequest.Options)
            {
                httpRequestMessage.Options.Set(new HttpRequestOptionsKey<object?>(item.Key), item.Value);
            }

            if (originalRequest.Content == null)
            {
                return httpRequestMessage;
            }

            var stream = originalRequest.Content!.ReadAsStream();
            if (stream.CanSeek)
            {
                stream.Seek(0L, SeekOrigin.Begin);
                httpRequestMessage.Content = new StreamContent(stream);
            }

            return httpRequestMessage;
        }
    }
}
