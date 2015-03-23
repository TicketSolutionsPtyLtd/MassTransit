// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Context
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Internals.Extensions;
    using Pipeline;


    /// <summary>
    /// Converts the object type message to the appropriate generic type and invokes the send method with that
    /// generic overload.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SendEndpointConverter<T> :
        ISendEndpointConverter
        where T : class
    {
        async Task ISendEndpointConverter.Send(ISendEndpoint endpoint, object message, CancellationToken cancellationToken)
        {
            if (endpoint == null)
                throw new ArgumentNullException("endpoint");
            if (message == null)
                throw new ArgumentNullException("message");

            var msg = message as T;
            if (msg == null)
                throw new ArgumentException("Unexpected message type: " + message.GetType().GetTypeName());

            await endpoint.Send(msg, cancellationToken);
        }

        async Task ISendEndpointConverter.Send(ISendEndpoint endpoint, object message, IPipe<SendContext> pipe,
            CancellationToken cancellationToken)
        {
            if (endpoint == null)
                throw new ArgumentNullException("endpoint");
            if (message == null)
                throw new ArgumentNullException("message");
            if (pipe == null)
                throw new ArgumentNullException("pipe");

            var msg = message as T;
            if (msg == null)
                throw new ArgumentException("Unexpected message type: " + message.GetType().GetTypeName());

            await endpoint.Send(msg, pipe, cancellationToken);
        }
    }
}