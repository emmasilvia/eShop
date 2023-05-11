﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eShop.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Nume Restaurante")]
        [Required(ErrorMessage = "Numele restaurantului este obligatorie")]
        public string Nume { get; set; }

        [Display(Name = "Imagine Restaurante")]
        [Required(ErrorMessage = "Imaginea pt restaurant este obligatorie")]
        public string Poza { get; set; }

        [Display(Name = "Descriere")]
        [Required(ErrorMessage = "Descrierea restaurantului este obligatorie")]
        public string Descriere { get; set; }

        public List<Produs> listaProduse { get; set; }

        public Adresa Adresa { get; set; }

    }
}
