namespace PackApi.Entities
{
    public class Gear
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // Todo: Make cateegory typed
    }
}