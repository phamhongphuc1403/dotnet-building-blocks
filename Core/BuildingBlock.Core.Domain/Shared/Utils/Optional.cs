namespace BuildingBlock.Core.Domain.Shared.Utils;

public class Optional<T>
{
    private readonly T? _instance;

    private Optional(T? instance)
    {
        _instance = instance;
    }

    public static Optional<T> Of(T? instance)
    {
        return new Optional<T>(instance);
    }

    public Optional<T> ThrowIfExist(Exception exception)
    {
        if ((_instance is bool && _instance.Equals(true)) || (_instance is not bool && _instance != null))
            throw exception;

        return this;
    }

    public Optional<T> ThrowIfNotExist(Exception exception)
    {
        if ((_instance is bool && _instance.Equals(false)) || _instance == null) throw exception;

        return this;
    }

    public T Get()
    {
        CheckIfExist();
        return _instance!;
    }

    public Optional<T> ThrowIfNotEqual(T? value, Exception exception)
    {
        if (Equals(_instance, value)) return this;

        throw exception;
    }

    public Optional<T> ThrowIfEqual(T? value, Exception exception)
    {
        if (!Equals(_instance, value)) return this;

        throw exception;
    }

    private void CheckIfExist()
    {
        ThrowIfNotExist(new InvalidOperationException("Instance is null."));
    }
}