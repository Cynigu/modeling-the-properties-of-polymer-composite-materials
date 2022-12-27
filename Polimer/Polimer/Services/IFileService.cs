using Polimer.App.ViewModel.Admin.Models;
using Polimer.App.ViewModel.Technology.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Polimer.App.Services;

public interface IFileService
{
    void Save(string filename, ComputeRecipeParametersModel data, ComputeRecipeParametersModel? dataI = null);
}