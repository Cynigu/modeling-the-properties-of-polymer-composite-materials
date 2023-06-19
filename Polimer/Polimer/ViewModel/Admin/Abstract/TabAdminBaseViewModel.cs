using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Polimer.Data.Models;
using Polimer.Data.Repository.Abstract;

namespace Polimer.App.ViewModel.Admin.Abstract;

public abstract class TabAdminBaseViewModel<TEntity, TModelAsEntity> : ViewModelBase
    where TEntity : class, IEntity
    where TModelAsEntity : ViewModelBase, IModelAsEntity
{
    protected readonly RepositoryBase<TEntity> _repository; // Репозиторий для работы с бд
    protected readonly IMapper _mapper;

    private ObservableCollection<TModelAsEntity>? _models; // коллекция хранящая все сущности таблицы
    private TModelAsEntity? _selectedModel; // выбранная из таблицы сущность
    private TModelAsEntity _changingModel; // модель, хранящая информацию для изенениия бд
    private string _nameTab;

    protected TabAdminBaseViewModel(RepositoryBase<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

        AddCommand = new AsyncCommand(AddAsync, CanAdd);
        RemoveCommand = new AsyncCommand(RemoveAsync, CanRemove);
        EditCommand = new AsyncCommand(EditAsync, CanEdit);
    }

    #region Properties

    /// <summary>
    /// Название таблицы
    /// </summary>
    public string NameTab
    {
        get => _nameTab;
        set => SetField(ref _nameTab, value);
    }

    /// <summary>
    /// Коллекця хранящая все сущности таблицы
    /// </summary>
    public ObservableCollection<TModelAsEntity>? Models
    {
        get => _models;
        set => SetField(ref _models, value);
    }

    /// <summary>
    /// Выбранная из таблицы сущность
    /// </summary>
    public TModelAsEntity? SelectedModel
    {
        get => _selectedModel;
        set
        {
            SetField(ref _selectedModel, value);
            if (_selectedModel != null)
            {
                CopySelectedModelToChanging();
            }

        }
    }

    /// <summary>
    /// модель, хранящая информацию для изенениия бд
    /// </summary>
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

    /// <summary>
    /// Функция добавляющая строку ChangingModel в бд
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private async Task AddAsync()
    {
        if (!CanAdd())
        {
            throw new ArgumentNullException("Поля должны быть заполнены!");
        }

        if (!await CheckingForExistenceAsync())
        {
            throw new ArgumentException("Такая сущность уже существует!");
        }

        var chModel = _mapper.Map<TModelAsEntity>(ChangingModel);
        chModel.Id = 0;
        var model = _mapper.Map<TEntity>(chModel);
        await _repository.AddAsync(model);

        await UpdateEntitiesAsync();
    }

    /// <summary>
    /// Функция редактирующая строку SelectedModel в соответствии ChangingModel в бд
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private async Task EditAsync()
    {
        if (!CanEdit())
        {
            throw
                new ArgumentNullException($"Поля должны быть заполнены и выбрана строка для редактирования!");
        }

        if (!await CheckingForExistenceAsync())
        {
            throw new ArgumentException("Такая сущность уже существует!");
        }

        var model = _mapper.Map<TModelAsEntity>(ChangingModel);
        model.Id = SelectedModel?.Id;
        await _repository.UpdateAsync(_mapper.Map<TEntity>(model));

        await UpdateEntitiesAsync();
    }

    /// <summary>
    /// Ф-я удалиющая строку SelectedModel
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
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
    public virtual async Task UpdateEntitiesAsync()
    {
        var models = (await _repository.GetEntitiesAsync()).Select(x
            => _mapper.Map<TModelAsEntity>(x));

        Models = new ObservableCollection<TModelAsEntity>(models);
    }

    #endregion


    #region AbstractMethods

    /// <summary>
    /// копирование полей в модель для изменениия из выбраннй строки
    /// </summary>
    protected virtual void CopySelectedModelToChanging()
    {
        ChangingModel = _mapper.Map<TModelAsEntity>(_selectedModel);
    }

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