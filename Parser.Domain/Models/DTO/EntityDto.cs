namespace Parser.Domain.Models.DTO
{
    public class EntityDto<T>
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public T Entity { get; set; }

        public EntityDto(Guid id, string action, T entity)
        {
            Id = id;
            Action = action;
            Entity = entity;
        }
    }
}
