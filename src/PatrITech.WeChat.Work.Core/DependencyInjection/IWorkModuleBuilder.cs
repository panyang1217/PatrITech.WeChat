﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.DependencyInjection
{
    public interface IWorkModuleBuilder
    {
        IServiceCollection Services { get; }
        IConfiguration Config { get; }
    }
}
