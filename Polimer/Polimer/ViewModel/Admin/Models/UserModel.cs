using Polimer.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Polimer.App.ViewModel.Admin.Models
{
    public class UserModel : ViewModelBase, IModelAsEntity
    {
        private int? _id;
        private string? _login;
        private string? _password;
        private string? _role;

        public int? Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string? Login
        {
            get => _login;
            set => SetField(ref _login, value);
        }

        public string? Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }

        public string? Role
        {
            get => _role;
            set => SetField(ref _role, value);
        }
    }
}
