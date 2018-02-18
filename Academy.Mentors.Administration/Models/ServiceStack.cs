using Academy.Mentors.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Academy.Mentors.Administration.Models
{
    /// <summary>
    /// Maintain the list of calling service for returning up the stack
    /// </summary>
    public class ServiceStack
    {
        // Note this feature is added to the Admin Tool when return navigation from drill
        // downs became problematic. When this technique works, the implementation
        // of Select processing should be updated to use ServiceStack, as the current implementation
        // is a bit inelegant 

        private string compressedStack;
        private Stack<String> _callStack;
        private Tokenator _tokenator = new Tokenator();
        private string defaultReturnUrl;

        /// <summary>
        /// Construct a call stack from its compressed serialization
        /// </summary>
        /// <param name="compressedStack"></param>
        /// <param name="defaultReturnUrl"></param>
        public ServiceStack(string compressedStack, string defaultReturnUrl = "/")
        {
            this.compressedStack = compressedStack;
            if (!String.IsNullOrEmpty(compressedStack))
                _callStack = JsonConvert.DeserializeObject<Stack<string>>(_tokenator.Decompress64(compressedStack));
            else
                _callStack = new Stack<string>();
            this.defaultReturnUrl = defaultReturnUrl;
        }

        /// <summary>
        /// Return the top of the call stack, or the default if it is empty
        /// </summary>
        /// <returns></returns>
        public string GetReturnUrl()
        {
            if (_callStack.Count > 0)
                return _callStack.Peek();
            return defaultReturnUrl;
        }

        /// <summary>
        /// Add a referal URL to the stack, and return a compressed image of the new stack
        /// </summary>
        /// <param name="referer"></param>
        /// <returns></returns>
        public string AddReturnUrl(string referer)
        {
            // Allow a page to call itself without "incrementing" the call back stack
            var returnUrl = _callStack.Peek().Split('?');
            if (!referer.StartsWith(returnUrl[0]) && !referer.Contains(compressedStack))
            {
                _callStack.Push(referer);
                compressedStack = _tokenator.Compress64(JsonConvert.SerializeObject(_callStack));
            }
            return compressedStack;
        }

        /// <summary>
        /// Return the current service stack in compressed form.
        /// </summary>
        /// <returns></returns>
        public string GetServiceStack()
        {
            return compressedStack;
        }
    }
}
