namespace Z.Ddd.Common.Entities.Permission
{
    public class ZPermissions : PermissionsBase
    {
        public ZPermissions() { }
        public ZPermissions(string id, string name, string code, string parentcode, bool group, bool page, bool button)
        {
            Id = id;
            Name = name;
            Code = code;
            ParentCode = parentcode;
            Group = group;
            Page = page;
            Button = button;
        }
    }
}
