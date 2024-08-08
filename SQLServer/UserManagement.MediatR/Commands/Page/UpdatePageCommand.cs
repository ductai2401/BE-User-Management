﻿using UserManagement.Data.Dto;
using MediatR;
using System;
using UserManagement.Helper;

namespace UserManagement.MediatR.Commands
{
    public class UpdatePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
