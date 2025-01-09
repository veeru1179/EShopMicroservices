﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exception
{
    public class NotFoundException : FormatException
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string name, object key) : base($"Entity\"{name}\"({key}) not found")
        {

        }
    }
}
