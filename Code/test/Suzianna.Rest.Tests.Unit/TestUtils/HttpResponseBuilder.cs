﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Suzianna.Rest.Tests.Unit.TestUtils
{
    internal class HttpResponseBuilder
    {
        private HttpResponseMessage _message;
        public HttpResponseBuilder()
        {
            this._message = new HttpResponseMessage();    
        }
        public HttpResponseBuilder WithContent(string content)
        {
            _message.Content = new StringContent(content);
            return this;
        }
        public HttpResponseBuilder WithHttpStatusCode(HttpStatusCode code)
        {
            _message.StatusCode = code;
            return this;
        }
        public HttpResponseBuilder WithHeader(string key, string value)
        {
            _message.Headers.Add(key, new List<string>(){ value});
            return this;
        }
        public HttpResponseMessage Build()
        {
            return _message;
        }
    }
}
