using BuildingBlock.Core.Application.EventBus.Abstractions;
using BuildingBlock.Core.Application.IntegrationEvents.Events;
using BuildingBlock.Core.Application.IntegrationEvents.Handlers;

namespace BuildingBlock.Core.Application.EventBus.Implementations;

public class EventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    private readonly List<Type> _eventTypes = [];
    private readonly Dictionary<string, List<Type>> _handlers = new();

    public bool IsEmpty => _handlers is { Count: 0 };

    public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();

        DoAddSubscription(typeof(TH), eventName);

        if (!_eventTypes.Contains(typeof(T))) _eventTypes.Add(typeof(T));
    }

    public void AddSubscription(Type eventType, Type eventHandlerType)
    {
        var eventName = eventType.Name;

        DoAddSubscription(eventHandlerType, eventName);

        if (!_eventTypes.Contains(eventType)) _eventTypes.Add(eventType);
    }

    public IEnumerable<Type> GetHandlersForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return GetHandlersForEvent(key);
    }

    public IEnumerable<Type> GetHandlersForEvent(string eventName)
    {
        return _handlers[eventName];
    }

    public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return HasSubscriptionsForEvent(key);
    }

    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }

    public Type? GetEventTypeByName(string eventName)
    {
        return _eventTypes.SingleOrDefault(t => t.Name == eventName);
    }

    public string GetEventKey<T>()
    {
        return typeof(T).Name;
    }

    private void DoAddSubscription(Type handlerType, string eventName)
    {
        if (!HasSubscriptionsForEvent(eventName)) _handlers.Add(eventName, []);

        if (_handlers[eventName].Any(type => type == handlerType))
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));

        _handlers[eventName].Add(handlerType);
    }
}