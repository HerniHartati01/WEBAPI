using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WEBAPI.Contracts
{
    public interface IMapper<TModel, TViewModel>
    {
        TViewModel Map(TModel model);
        TModel Map (TViewModel viewModel);
    }
}
