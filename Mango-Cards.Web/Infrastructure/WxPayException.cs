﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Infrastructure
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg) : base(msg)
        {

        }
    }
}