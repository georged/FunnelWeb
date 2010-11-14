﻿using System.Collections.Generic;
using FunnelWeb.Model;

namespace FunnelWeb.Web.Features.Admin.Views
{
    public class PingbacksModel
    {
        public PingbacksModel(IEnumerable<Pingback> pingbacks)
        {
            Pingbacks = pingbacks;
        }

        public IEnumerable<Pingback> Pingbacks { get; set; }
    }
}