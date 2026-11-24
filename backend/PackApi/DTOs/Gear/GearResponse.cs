

namespace PackApi.DTOs.Gear
{
    public record GearResponse(
        int Id,
        int OwnerId,
        string Name,
        string Category
    );
}