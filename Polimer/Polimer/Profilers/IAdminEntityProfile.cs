namespace Polimer.App.Profilers;

internal interface IAdminEntityProfile
{
    void MapModelToEntity();
    void MapEntityToModel();
    void MapModelToModel();
}