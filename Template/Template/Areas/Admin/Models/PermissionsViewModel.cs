using System.Collections.Generic;
using Template.Admin.Reusable;

namespace Template.Admin.Models
{
    public class PermissionsViewModel
    {
        public PermissionsTreeViewModel TreeViewModel { get; set; }

        public class PermissionsTreeViewModel : TreeListVeiwModelBase
        {
            public List<PermissionTreeItem> TreeItems { get; set; }

            public class PermissionTreeItem
            {
                public int? ID { get; set; }
                public int? ParentID { get; set; }
                public string Caption { get; set; }
                public string Url { get; set; }
                public string IconName { get; set; }
                public bool IsMenuItem { get; set; }
                public string Code { get; set; }
                public int? SortIndex { get; set; }
            }

        }

    }




}