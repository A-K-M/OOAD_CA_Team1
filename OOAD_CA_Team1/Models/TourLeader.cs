﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOAD_CA_Team1.Models
{
    public class TourLeader
    {
        public int TourleaderId {get; set;}
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public Fulltime FulltimeLeader { get; set; }
        public Parttime ParttimeLeader { get; set; }

        public virtual double GetDailyRate()
        {
            return 0.0;
        }
    }
}