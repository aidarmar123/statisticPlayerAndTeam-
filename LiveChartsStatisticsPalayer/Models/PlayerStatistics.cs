//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiveChartsStatisticsPalayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayerStatistics
    {
        public int Id { get; set; }
        public int MatchupId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public int IsStarting { get; set; }
        public decimal Min { get; set; }
        public int Point { get; set; }
        public int Assist { get; set; }
        public int FieldGoalMade { get; set; }
        public int FieldGoalMissed { get; set; }
        public int C3_PointFieldGoalMade { get; set; }
        public int C3_PointFieldGoalMissed { get; set; }
        public int FreeThrowMade { get; set; }
        public int FreeThrowMissed { get; set; }
        public int Rebound { get; set; }
        public int OFFR { get; set; }
        public int DFFR { get; set; }
        public int Steal { get; set; }
        public int Block { get; set; }
        public int Turnover { get; set; }
        public int Foul { get; set; }
    
        public virtual Matchup Matchup { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
