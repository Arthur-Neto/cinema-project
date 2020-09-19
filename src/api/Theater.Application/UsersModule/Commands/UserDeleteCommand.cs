﻿using AutoMapper;
using FluentValidation;
using Theater.Domain.UsersModule;

namespace Theater.Application.UsersModule.Commands
{
    public class UserDeleteCommand
    {
        public int ID { get; set; }
    }

    public class UserDeleteCommandMapping : Profile
    {
        public UserDeleteCommandMapping()
        {
            CreateMap<UserDeleteCommand, User>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID));
        }
    }

    public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand>
    {
        public UserDeleteCommandValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0);
        }
    }
}
