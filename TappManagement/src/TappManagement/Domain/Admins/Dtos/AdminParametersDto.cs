namespace TappManagement.Domain.Admins.Dtos;

using SharedKernel.Dtos;

public sealed class AdminParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
