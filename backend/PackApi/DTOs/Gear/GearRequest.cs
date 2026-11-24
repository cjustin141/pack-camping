

namespace PackApi.DTOs.Gear
{
    public record GearRequest(
        int Id,
        int OwnerId,
        string Name,
        string Category
    );
}