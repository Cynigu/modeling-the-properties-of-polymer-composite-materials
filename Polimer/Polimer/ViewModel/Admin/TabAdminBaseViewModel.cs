using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Polimer.Data.Models;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin;

public abstract class TabAdminBaseViewModel<TEntity, TModelAsEntity> : ViewModelBase
    where TEntity : class, IEntity
    where TModelAsEntity : ViewModelBase, IModelAsEntity
{
    protected readonly RepositoryBase<TEntity> _repository;
    protected readonly IMapper _mapper;

    private ObservableCollection<TModelAsEntity>? _models;
    private TModelAsEntity? _selectedModel;
    private TModelAsEntity _changingModel;

    protected TabAdminBaseViewModel(RepositoryBase<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

        AddCommand = new AsyncCommand(AddAsync, CanAdd);
        RemoveCommand = new AsyncCommand(RemoveAsync, CanRemove);
        EditCommand = new AsyncCommand(EditAsync, CanEdit);
    }

    #region Properties

    public ObservableCollection<TModelAsEntity>? Models
    {
        get => _models;
        set => SetField(ref _models, value);
    }

    public TModelAsEntity? SelectedModel
    {
        get => _selectedModel;
        set
        {
            SetField(ref _selectedModel, value);
            if (_selectedModel != null)
            {
                ChangingModel = _mapper.Map<TModelAsEntity>(_selectedModel);
                ChangingModel.Id = null;
            }
           
        }
    }

    public TModelAsEntity ChangingModel
    {
        get => _changingModel;
        set => SetField(ref _changingModel, value);
    }

    #endregion

    #region Command

    public ICommand AddCommand { get; set; }
    public ICommand RemoveCommand { get; set; }
    public ICommand EditCommand { get; set; }
    #endregion

    #region Methods

    private async Task AddAsync()
    {
        if (!CanAdd())
        {
            throw new ArgumentNullException("Поля должны быть заполнены!");
        }

        if (await CheckingForExistenceAsync())
        {
            throw new ArgumentException("Такая сущность уже существует!");
        }

        var model = _mapper.Map<TEntity>(ChangingModel);
        await _repository.AddAsync(model);

        await UpdateEntitiesAsync();
    }

    private async Task EditAsync()
    {
        if (!CanEdit())
        {
            throw 
                new ArgumentNullException($"Поля должны быть заполнены и выбрана строка для редактирования!");
        }

        if (await CheckingForExistenceAsync())
        {
            throw new ArgumentException("Такая сущность уже существует!");
        }

        var model = _mapper.Map<TModelAsEntity>(ChangingModel);
        model.Id = SelectedModel?.Id;
        await _repository.UpdateAsync(_mapper.Map<TEntity>(model));

        await UpdateEntitiesAsync();
    }

    private async Task RemoveAsync()
    {
        if (SelectedModel == null)
            throw new ArgumentNullException("Не выбрана строка!");

        await _repository.RemoveRangeAsync(x => x.Id == SelectedModel.Id);

        await UpdateEntitiesAsync();
    }

    /// <summary>
    /// Обновить таблицу сущности
    /// </summary>
    /// <returns></returns>
    public async Task UpdateEntitiesAsync()
    {
        var models = (await _repository.GetEntitiesAsync()).Select(x 
            => _mapper.Map<TModelAsEntity>(x));

        Models = new ObservableCollection<TModelAsEntity>(models);
    }

    #endregion


    #region AbstractMethods

    /// <summary>
    /// Проверка на существование такой же строки
    /// </summary>
    /// <returns></returns>
    protected abstract Task<bool> CheckingForExistenceAsync();

    /// <summary>
    /// Есть ли возможность добавить сущность в базу
    /// </summary>
    /// <returns></returns>
    protected abstract bool CanAdd();

    /// <summary>
    /// Есть ли возможность удалить сущность
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanRemove() => SelectedModel != null;

    /// <summary>
    /// Есть ли возможность редактировать сущнсть
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanEdit() => SelectedModel != null && CanAdd();
        
    #endregion

}