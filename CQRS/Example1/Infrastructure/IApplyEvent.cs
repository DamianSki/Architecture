namespace Infrastructure
{
    public interface IApplyEvent<TEvent>
    {
        void Apply(TEvent @event);
    }
}