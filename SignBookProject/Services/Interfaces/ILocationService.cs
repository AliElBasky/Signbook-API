﻿using SignBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Services.Interfaces
{
    public interface ILocationService
    {
        BundleModel isEligible(PointModel point, string userId);
    }
}
