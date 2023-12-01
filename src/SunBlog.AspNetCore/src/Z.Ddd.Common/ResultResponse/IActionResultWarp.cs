using Microsoft.AspNetCore.Mvc.Filters;

namespace Z.Ddd.Common.ResultResponse;

public interface IActionResultWarp
{
    void Wrap(FilterContext context);
}
