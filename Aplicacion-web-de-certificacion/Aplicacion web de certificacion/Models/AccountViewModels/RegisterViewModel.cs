using Aplicacion_web_de_certificacion.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion_web_de_certificacion.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Permisos")]
        [UIHint("List")]
        public List<SelectListItem> Roles { get; set; }
        public string Role { get; set; }

        public RegisterViewModel()
        {
            Roles = new List<SelectListItem>();
            //Roles.Add(new SelectListItem()
            //{
            //    Value = "1",
            //    Text = "Admin"
            //});
            //Roles.Add(new SelectListItem()
            //{
            //    Value = "2",
            //    Text = "OCP"
            //});

            //Roles.Add(new SelectListItem()
            //{
            //    Value = "3",
            //    Text = "Jefe de Laboratorios"
            //});

            //Roles.Add(new SelectListItem()
            //{
            //    Value = "4",
            //    Text = "Tecnico"
            //});

            //Roles.Add(new SelectListItem()
            //{
            //    Value = "5",
            //    Text = "Jefe OCP"
            //});

            //Roles.Add(new SelectListItem()
            //{
            //    Value = "6",
            //    Text = "Comercial"
            //});
        }
        public void getRoles(ApplicationDbContext _context)
        {
            var roles = from r in _context.identityRole select r;
            var listRole = roles.ToList();

            foreach (var Data in listRole) {
                Roles.Add(new SelectListItem()
                {
                    Value = Data.Id,
                    Text = Data.Name
                });
            }
        }
        
    }

 }

