using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroRabbit.Infra.Bus
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly ICollection<Type> _eventTypes;

        public RabbitMqBus(IMediator mediator)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T command) where T : Command
            => _mediator.Send(command);

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory(){ HostName = "localhost"};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes((message));
            channel.BasicPublish("", eventName, null, body);
        }

        public void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            
            if(!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));
            
            if(_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Any(x => x.GetType() == handlerType))
                throw new ArgumentException($"Handler type {handlerType.Name} already registered for '${eventName}'");
            
            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory {HostName = "localhost", DispatchConsumersAsync = true};
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEventAsync(eventName, message).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
            }
        }

        private async Task ProcessEventAsync(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = Activator.CreateInstance(subscription);
                    if(handler is null) continue;
                    var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task) concreteType.GetMethod("Handle").Invoke(handler, new[] { @event });
                }
            }
                
                
        }
    }
}