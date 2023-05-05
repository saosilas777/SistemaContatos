using SistemaContatos.Models;

namespace SistemaContatos.Helper
{
	public interface ISection
	{
		void UserSectionCreate(string token);
		void UserSectionRemove();
		string GetUserSection();
	}
}
