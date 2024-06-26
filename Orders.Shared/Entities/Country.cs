﻿using Orders.Shared.Interfaces;
using Orders.Shared.Responses;
using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities
{
    public class Country : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [MaxLength(100, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; } = null!;


        public ICollection<State>? States { get; set; }

        [Display(Name = "Departamento/Estado")]
        public int StatesNumber => States== null || States.Count==0 ? 0 : States.Count;
    }
}