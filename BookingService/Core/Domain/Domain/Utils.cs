namespace Domain;

public class Utils
{
    public static bool ValidateEmail(string email)
    {
        if(email.Contains("@"))
            return true;
        
        return false;
    }
}