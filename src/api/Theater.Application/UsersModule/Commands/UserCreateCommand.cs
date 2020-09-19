﻿using AutoMapper;
using FluentValidation;
using Theater.Domain.UsersModule;

namespace Theater.Application.UsersModule.Commands
{
    public class UserCreateCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserCreateCommandMapping : Profile
    {
        public UserCreateCommandMapping()
        {
            CreateMap<UserCreateCommand, User>()
                .ForMember(m => m.Username, opts => opts.MapFrom(src => src.Username))
                .ForMember(m => m.Password, opts => opts.MapFrom(src => src.Password));
        }
    }

    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(1, 50);
            RuleFor(x => x.Password).NotEmpty().Length(1, 50);
        }
    }
}
