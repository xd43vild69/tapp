namespace TappManagement.Domain.Customers.Dtos;

using SharedKernel.Dtos;

public sealed class CustomerParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
