using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;


namespace AmsmProject.Models
{
   
    public class ManageUserViewModel
    {
        [Required(ErrorMessage = "Внесете лозинка")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Внесете  нова лозинка")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Лозинките не се совпаѓаат")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Внесете корисничко име")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Внесете лозинка")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Внесете корисничко име")]
        //[Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Внесете лозинка")]
        [StringLength(30, ErrorMessage = "Лозинката треба да е со должина од најмалку шест карактери", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат")]
        public string ConfirmPassword { get; set; }

        [RegularExpression("^[0-9]{13}$", ErrorMessage = "Матичниот број има невалиден формат")]
        [Required(ErrorMessage = "Внесете го вашиот матичен број")]
        public string EMBG { get; set; }



        [StringLength(20, MinimumLength = 3, ErrorMessage = "Името е невалидно")]
        [Required(ErrorMessage = "Внесете го вашето име")]
        public string firstName { get; set; }




        [StringLength(20, MinimumLength = 3, ErrorMessage = "Татковото име е невалидно")]
        [Required(ErrorMessage = "Внесете татково име")]
        public string parentName { get; set; }




        [StringLength(30, MinimumLength = 3, ErrorMessage = "Презимето е невалидно")]
        [Required(ErrorMessage = "Внесете го вашето презиме")]
        public string lastName { get; set; }



        [Required(ErrorMessage = "Изберете категорија")]
        public string category { get; set; }




        [StringLength(30, MinimumLength = 3, ErrorMessage = "Името на авто школата е невалидно")]
        [Required(ErrorMessage = "Внесете го името на авто школа")]
        public string drivingSchool { get; set; }


        [StringLength(30, MinimumLength = 3, ErrorMessage = "Името на инструкторот е невалидно")]
        [Required(ErrorMessage = "Внесете го името на  инструкторот")]
        public string instructor { get; set; }

        [Required(ErrorMessage = "Внесете е-маил")]
        [EmailAddress(ErrorMessage = "Невалидна е-маил адреса")]
        public string mail { get; set; }


        [Required(ErrorMessage = "Внесете код")]
        public string code { get; set; }


        //ova e dodadeno


        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser()
            {
                UserName = this.UserName,
                firstName = this.firstName,
                lastName = this.lastName,
                mail = this.mail,
                parentName=this.parentName,
                instructor=this.instructor,
                drivingSchool=this.drivingSchool,
                EMBG=this.EMBG,
                code=this.code,
                category=this.category,

            };
            return user;
        }
    }


        public class EditUserViewModel
        {
            public EditUserViewModel() { }

            // Allow Initialization with an instance of ApplicationUser:
            public EditUserViewModel(ApplicationUser user)
            {
                 

                this.UserName = user.UserName;
                this.firstName = user.firstName;
                this.lastName = user.lastName;
                this.mail = user.mail;
                this.parentName=user.parentName;
                this.instructor=user.instructor;
                this.drivingSchool=user.drivingSchool;
                this.EMBG=user.EMBG;
                this.code=user.code;
                this.category=user.category;

            }

            [Required(ErrorMessage = "Внесете корисничко име")]
            //[Display(Name = "User name")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Внесете лозинка")]
            [StringLength(30, ErrorMessage = "Лозинката треба да е со должина од најмалку шест карактери", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат")]
            public string ConfirmPassword { get; set; }

           
            [Key]
            [Required(ErrorMessage = "Внесете го вашиот матичен број")]
            public string EMBG { get; set; }



           
            [Required(ErrorMessage = "Внесете го вашето име")]
            public string firstName { get; set; }




          
            [Required(ErrorMessage = "Внесете татково име")]
            public string parentName { get; set; }




           
            [Required(ErrorMessage = "Внесете го вашето презиме")]
            public string lastName { get; set; }



            [Required(ErrorMessage = "Изберете категорија")]
            public string category { get; set; }




           
            [Required(ErrorMessage = "Внесете го името на авто школа")]
            public string drivingSchool { get; set; }


            
            [Required(ErrorMessage = "Внесете го името на  инструкторот")]
            public string instructor { get; set; }

           
            [EmailAddress(ErrorMessage = "Невалидна е-маил адреса")]
            public string mail { get; set; }


            [Required(ErrorMessage = "Внесете код")]
            public string code { get; set; }

        }


        public class SelectUserRolesViewModel
        {
            public SelectUserRolesViewModel()
            {
                this.Roles = new List<SelectRoleEditorViewModel>();
            }


            // Enable initialization with an instance of ApplicationUser:
            public SelectUserRolesViewModel(ApplicationUser user): this()
            {
                this.UserName = user.UserName;
                this.FirstName = user.firstName;
                this.LastName = user.lastName;
               


                var Db = new ApplicationDbContext();

                // Add all available roles to the list of EditorViewModels:
                var allRoles = Db.Roles;
                foreach (var role in allRoles)
                {
                    // An EditorViewModel will be used by Editor Template:
                    var rvm = new SelectRoleEditorViewModel(role);
                    this.Roles.Add(rvm);
                }

                // Set the Selected property to true for those roles for 
                // which the current user is a member:
                foreach (var userRole in user.Roles)
                {
                    var checkUserRole =
                        this.Roles.Find(r => r.RoleName == userRole.Role.Name);
                    checkUserRole.Selected = true;
                }
            }

            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<SelectRoleEditorViewModel> Roles { get; set; }
        }

        // Used to display a single role with a checkbox, within a list structure:
        public class SelectRoleEditorViewModel
        {
            public SelectRoleEditorViewModel() { }
            public SelectRoleEditorViewModel(IdentityRole role)
            {
                this.RoleName = role.Name;
            }

            public bool Selected { get; set; }

            [Required]
            public string RoleName { get; set; }
        }
    }

