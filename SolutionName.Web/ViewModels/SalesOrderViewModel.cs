﻿using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionName.Web.ViewModels
{
    public class SalesOrderViewModel : IObjectWithState
    {
        public int SalesOrderId { get; set; }

        public string CustomerName { get; set; }

        public string PONumber { get; set; }

        public string MessageToClient { get; set; }

        public ObjectState ObjectState { get; set; }

        //fixed something
    }
}