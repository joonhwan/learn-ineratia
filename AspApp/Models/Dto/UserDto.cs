using JetBrains.Annotations;

namespace AspApp.Models.Dto;

[UsedImplicitly]
public record UserDto(int Id, string? Name, string? Email);