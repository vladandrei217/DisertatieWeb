namespace DisertatieWeb.Shared.Services
{
    public class AuthenticationService
    {
        public bool IsAuthenticated { get; private set; } = false;
        public string Username { get; private set; }

        public event Action OnChange;

        public async Task LoginAsync(string username)
        {
            IsAuthenticated = true;
            Username = username;
            //await NotifyStateChanged();
        }

        public async Task LogoutAsync()
        {
            IsAuthenticated = false;
            Username = null;
            //await NotifyStateChanged();
        }

        private async Task NotifyStateChanged()
        {
            await Task.Yield(); // IMPORTANT: readuce contextul pe Dispatcher
            OnChange?.Invoke();
        }
    }
}
