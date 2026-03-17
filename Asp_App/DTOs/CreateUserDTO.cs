using System.ComponentModel.DataAnnotations;

namespace Asp_App.DTOs
{
    public class CreateUserDTO
    {
        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [Length(minimumLength: 2, maximumLength: 100, ErrorMessage = "Nome deve conter entre 2 e 100 caractéres!")]
        public string UserName {get;set;}
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email é obrigatório!")]
        [MaxLength(100, ErrorMessage = "Email deve conter no máximo 100 caracteres!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é obrigatória!")]
        [MinLength(8, ErrorMessage = "Senha deve conter no mínimo 8 caractéres!")]
        [MaxLength(150, ErrorMessage = "Senha deve conter no máximo 150 caractéres!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.")]
        public string UserPassword {get;set;}
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "CPF é obrigatório!")]
        [Length(minimumLength: 11, maximumLength: 14, ErrorMessage = "CPF deve conter entre 11 e 14 dígitos!")]
        public string Cpf {get;set;}
        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Data de nascimento é obrigatória!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        // [Range(typeof(DateOnly), "1920-01-01", "2008-01-01", ErrorMessage = "Data fora do intervalo válido")]
        public DateOnly BirthDate {get;set;}
    }
}