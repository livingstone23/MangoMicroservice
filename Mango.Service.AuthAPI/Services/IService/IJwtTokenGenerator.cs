﻿using Mango.Service.AuthAPI.Models;


namespace Mango.Service.AuthAPI.Service.IService;


public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<String> roles);

}

