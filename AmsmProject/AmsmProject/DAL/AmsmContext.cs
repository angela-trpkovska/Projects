using AmsmProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AmsmProject.DAL
{
    public class AmsmContext: DbContext
    {

        public AmsmContext(): base("AmsmContext")
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
      
        public DbSet<TheoryPart> CandidatesTP { get; set; }

        public DbSet<PracticalPart1> CandidatesPP1 { get; set; }

        public DbSet<PracticalPart2> CandidatesPP2 { get; set; }

        public DbSet<AmsmInfo> Informations { get; set; }

        public DbSet<CreditCardInfo> CreditCards { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<AmsmProject.Models.EditUserViewModel> EditUserViewModels { get; set; }

        public System.Data.Entity.DbSet<AmsmProject.Models.AmsmInfoPP2> AmsmInfoPP2 { get; set; }

    }
}