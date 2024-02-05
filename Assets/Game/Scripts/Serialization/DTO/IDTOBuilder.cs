namespace Game.Scripts.Serialization.DTO
{
    public interface IDTOBuilder<T>
    {
        T DTO { get; }
        void InitAsNew();
        void InitByDTO(T dto);
    }
}