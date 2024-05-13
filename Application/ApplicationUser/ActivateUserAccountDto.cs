namespace Application.ApplicationUser
{
    public class ActivateUserAccountDto
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
