﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Configurations
{
    public class DatabaseSettings
    {
        public string DBProvder {  get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
