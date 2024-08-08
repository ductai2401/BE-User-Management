using System;

namespace UserManagement.Data.Dto
{
    public class AppSettingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
