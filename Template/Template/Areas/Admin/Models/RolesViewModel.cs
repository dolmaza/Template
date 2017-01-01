using System.Collections.Generic;
using Template.Admin.Reusable;

namespace Template.Admin.Models
{
    public class RolesViewModel
    {
        public RolesGridViewModel GridViewModel { get; set; }

        public class RolesGridViewModel : GridViewModelBase
        {
            public List<RoleGridItem> GridItems { get; set; }

            public class RoleGridItem
            {
                public int? ID { get; set; }
                public string Caption { get; set; }
                public int? Code { get; set; }
            }

        }
    }




}