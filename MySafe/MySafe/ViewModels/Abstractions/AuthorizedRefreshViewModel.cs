using System.Threading.Tasks;
using AutoMapper;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Abstractions;
using MySafe.Presentation.Models.Abstractions;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedRefreshViewModel<TEntity, TModel> : AuthorizedViewModelBase, INavigatedAware
        where TEntity : IEntity
        where TModel : IPresentationModel
    {
        protected AuthorizedRefreshViewModel(INavigationService navigationService, IMapper mapper) : base(
            navigationService)
        {
            RefreshCommand = new AsyncCommand(async () =>
            {
                var entity = await _refreshTask;
                var presentationModel = mapper.Map<TModel>(entity);
                var hasError = HandleRefreshResult(presentationModel);
                if (!hasError) RefillObservableCollection(presentationModel);
            });
        }

        public AsyncCommand RefreshCommand { get; }

        protected abstract Task<TEntity> _refreshTask { get; }
        protected abstract void RefillObservableCollection(TModel mediatorResponse);

        protected virtual bool HandleRefreshResult(TModel mediatorResponse)
        {
            Error = mediatorResponse.Error;

            return mediatorResponse.HasError;
        }

        protected override void DoAfterNavigatedTo()
        {
            RefreshCommand.Execute(null);
        }
    }
}