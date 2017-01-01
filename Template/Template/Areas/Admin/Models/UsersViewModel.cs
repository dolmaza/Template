using Service.Utilities;
using System.Collections.Generic;
using Template.Admin.Reusable;

namespace Template.Admin.Models
{
    public class UsersViewModel
    {
        public UsersGridViewModel GridViewModel { get; set; }

        public class UsersGridViewModel : GridViewModelBase
        {
            public List<UserGridItem> GridItems { get; set; }
            public List<SimpleKeyValue<int?, string>> Roles { get; set; }

            public class UserGridItem
            {
                public int? ID { get; set; }
                public int? RoleID { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public string Firstname { get; set; }
                public string Lastname { get; set; }
                public bool IsActive { get; set; }
            }

        }
    }

}