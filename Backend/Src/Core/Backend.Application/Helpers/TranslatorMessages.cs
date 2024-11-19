using Backend.Application.DTOs;

namespace Backend.Application.Helpers;

public static class TranslatorMessages
{
    public static class AccountMessages
    {
        public static TranslatorMessageDto Account_NotFound_with_UserName(string userName)
        {
            return new TranslatorMessageDto(nameof(Account_NotFound_with_UserName), [userName]);
        }

        public static TranslatorMessageDto Username_is_already_taken(string userName)
        {
            return new TranslatorMessageDto(nameof(Username_is_already_taken), [userName]);
        }

        public static string Invalid_password()
        {
            return nameof(Invalid_password);
        }
    }

    public static class ProductMessages
    {
        public static TranslatorMessageDto Product_NotFound_with_id(long id)
        {
            return new TranslatorMessageDto(nameof(Product_NotFound_with_id), [id.ToString()]);
        }
    }
}