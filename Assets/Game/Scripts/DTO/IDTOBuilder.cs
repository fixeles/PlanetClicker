namespace Game.Scripts.DTO
{
    public interface IDTOBuilder<T>
    {
        T DTO { get; }
        void InitAsNew();
        void InitByDTO(T dto);
    }
}