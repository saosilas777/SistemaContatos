using SistemaContatos.Models;

namespace SistemaContatos.Helper
{
	public interface ISection
	{
		void UserSectionCreate(UserModel user);
		void UserSectionRemove();
		UserModel GetUserSection();
	}
}
