using Polimer.App.ViewModel;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal interface IAdminEntityProfile
{
    void MapEntityToModel();
    void MapModelToEntity();
    void MapModelToModel();
}