﻿using System;

namespace UserManagement.Data
{
    public class EmailTemplate: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
