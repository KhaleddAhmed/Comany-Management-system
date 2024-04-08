using System.ComponentModel.DataAnnotations;

namespace RouteC41.G02.PL.ViewModels.User
{
	public class SignUpViewModel
	{

		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }


		[Required(ErrorMessage ="Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage ="First Name is Required")]
		[Display(Name = "First Name")]

        public string FName { get; set; }

		[Required(ErrorMessage = "Last Name is Required")]
		[Display(Name = "Last Name")]

		public string LName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		//[MinLength(5,ErrorMessage ="Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Confirm Password doesn't Match With Password")]
		public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }


    }
}
