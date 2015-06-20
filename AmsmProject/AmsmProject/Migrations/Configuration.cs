namespace AmsmProject.Migrations
{
    using AmsmProject.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AmsmProject.DAL.AmsmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AmsmProject.DAL.AmsmContext context)
        {
            this.AddUserAndRoles();

            var candidates = new List<Candidate>
            {
            new Candidate{firstName="������",parentName="����",lastName="���������" ,category="�", 
drivingSchool="����",instructor="���� �������",EMBG="1005993455011",registered=false},
            new Candidate{firstName="���",parentName="�����",lastName="��������" ,category="�", 
drivingSchool="��������������",instructor="��� ������������",EMBG="1685934512123",registered=false},
            new Candidate{firstName="����������",parentName="�����",lastName="��������" ,category="�", 
drivingSchool="����",instructor="����� ������",EMBG="0808081111111",registered=false},
            new Candidate{firstName="������",parentName="����",lastName="���������" ,category="�", 
drivingSchool="����",instructor="����� �������",EMBG="2608990455000",registered=false},
            new Candidate{firstName="��������",parentName="���������",lastName="�������" ,category="�", 
drivingSchool="����",instructor="���� ����������",EMBG="0705993889966",registered=false},
            new Candidate{firstName="�����",parentName="����",lastName="���������" ,category="�", 
drivingSchool="�����",instructor="����� ������",EMBG="0909990458912",registered=false},
            new Candidate{firstName="�����",parentName="�����",lastName="�����������" ,category="�", 
drivingSchool="�����",instructor="����� ���������",EMBG="0303991458520",registered=false},
            new Candidate{firstName="���",parentName="����",lastName="��������" ,category="�", 
drivingSchool="����",instructor="���� �������",EMBG="0808992457841",registered=false},
            new Candidate{firstName="�����",parentName="������",lastName="���������" ,category="�", 
drivingSchool="����",instructor="���� ����������",EMBG="1108993451020",registered=false},
            new Candidate{firstName="������",parentName="����������",lastName="��������" ,category="�", 
drivingSchool="�����",instructor="����� ������",EMBG="0107986122011",registered=false},
            new Candidate{firstName="������",parentName="����",lastName="���������" ,category="�", 
drivingSchool="����",instructor="���� ����������",EMBG="2506991457896",registered=false},
            new Candidate{firstName="������",parentName="����",lastName="�������" ,category="�", 
drivingSchool="����",instructor="���� ����������",EMBG="2205993451022",registered=false}};

            candidates.ForEach(s => context.Candidates.Add(s));
            context.SaveChanges();
        }

            


        bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("User");
            if (!success) return success;


            var newUser = new ApplicationUser()
            {
                UserName = "Administrator",
                firstName = "Angela",
                lastName = "Trpkovska",
                mail = "a.trpkovska@yahoo.com"
            };

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            success = idManager.CreateUser(newUser, "password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;



            return success;
        }
        }
    }

