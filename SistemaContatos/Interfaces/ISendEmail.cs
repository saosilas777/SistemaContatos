namespace SistemaContatos.Interfaces
{
	public interface ISendEmail
	{
		bool SendEmail(string email,string subject, string message);
	}
}
