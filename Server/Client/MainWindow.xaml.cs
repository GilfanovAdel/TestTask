using Core.Dto;
using Core.Entity;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient;
        private Mode currentMode = Mode.Registration;
        private const string ServerUrl = "http://localhost:5242";
        private Dictionary<string, Guid> _organizationMap;

        public MainWindow()
        {
            InitializeComponent();
            OrganizationComboBox.SelectionChanged += OrganizationComboBox_SelectionChanged;
            _organizationMap = new Dictionary<string, Guid>();
            _httpClient = new HttpClient();
            UpdateMode();

            LoadOrganizations();
        }

        private async void OrganizationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrganizationComboBox.SelectedItem == null) return;

            string selectedOrganization = (string)OrganizationComboBox.SelectedItem;

            try
            {
                var users = await FetchUsersByOrganizationAsync(selectedOrganization);

                LoginComboBox.ItemsSource = users.Select(x => x.Login).ToList() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private async Task<List<UserDto>> FetchUsersByOrganizationAsync(string organization)
        {
          

            HttpResponseMessage response = await _httpClient.GetAsync($"{ServerUrl}/GetUsersbyOrganization?name={organization}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ошибка сервера: {response.StatusCode}");
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<UserDto>>(jsonResponse,options);
        }

        private void ToggleModeButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode == Mode.Registration)
                currentMode = Mode.Login;
            else
                currentMode = Mode.Registration;
            UpdateMode();
        }

        private void UpdateMode()
        {
            switch (currentMode)
            {
                case Mode.Registration:
                    LoginComboBox.Visibility = Visibility.Collapsed;
                    LoginTextBlock.Visibility = Visibility.Collapsed;
                    UsernameTextBlock.Visibility = Visibility.Visible;
                    UsernameTextBox.Visibility = Visibility.Visible;
                    OrganizationTextBlock.Visibility = Visibility.Visible;
                    OrganizationComboBox.Visibility = Visibility.Visible;
                    ToggleModeButton.Content = "Login Account";
                    RegisterButton.Visibility = Visibility.Visible;
                    LoginButton.Visibility = Visibility.Collapsed;
                    LoginWithOrganizationButton.Visibility = Visibility.Collapsed;
                    break;

                case Mode.Login:
                    LoginComboBox.Visibility = Visibility.Collapsed;
                    LoginTextBlock.Visibility = Visibility.Collapsed;
                    UsernameTextBlock.Visibility = Visibility.Visible;
                    UsernameTextBox.Visibility = Visibility.Visible;
                    OrganizationTextBlock.Visibility = Visibility.Collapsed;
                    OrganizationComboBox.Visibility = Visibility.Collapsed;
                    ToggleModeButton.Content = "Create Account";
                    RegisterButton.Visibility = Visibility.Collapsed;
                    LoginButton.Visibility = Visibility.Visible;
                    LoginWithOrganizationButton.Visibility = Visibility.Visible;
                    break;
                case Mode.LoginWithOrganization:
                    LoginComboBox.Visibility = Visibility.Visible;
                    LoginTextBlock.Visibility = Visibility.Visible;
                    UsernameTextBlock.Visibility = Visibility.Collapsed;
                    UsernameTextBox.Visibility = Visibility.Collapsed;
                    PasswordBox.Visibility = Visibility.Visible;
                    OrganizationComboBox.Visibility = Visibility.Visible;
                    OrganizationTextBlock.Visibility= Visibility.Visible;
                    LoginButton.Visibility = Visibility.Visible;
                    RegisterButton.Visibility = Visibility.Collapsed;
                    LoginWithOrganizationButton.Visibility = Visibility.Visible;
                    LoginWithOrganizationButton.Content = "login without Organization";
                    ToggleModeButton.Content = "Create Account";
                    break;
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            var organizationName = OrganizationComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(organizationName))
            {
                MessageBox.Show("поля пустые", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_organizationMap.TryGetValue(organizationName, out var organizationId))
            {
                MessageBox.Show("несуществующая организация", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new UserRegistrationDto() { Login = username, Password = password, Organizationid = organizationId };
            var jsonContent = new StringContent(JsonSerializer.Serialize<UserRegistrationDto>(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ServerUrl + "/register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("регистрация успешна", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadOrganizations();
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username;
            if (currentMode == Mode.LoginWithOrganization)
            {
                username = (string) LoginComboBox.SelectedItem;
            }
            else
            {
                username = UsernameTextBox.Text;
            }
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("поля пустые", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var loginData = new LoginDto { Username = username, Password = password };
            var jsonContent = new StringContent(JsonSerializer.Serialize<LoginDto>(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ServerUrl + "/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show(" вход успешен!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginWithOrganizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode == Mode.Login)
                currentMode = Mode.LoginWithOrganization;
            else
                currentMode = Mode.Login;
            UpdateMode();
        }

        private async void LoadOrganizations()
        {
            var response = await _httpClient.GetAsync(ServerUrl + "/GetallOrganizations");

            if (response.IsSuccessStatusCode)
            {
                var organizationsJson = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var organizations = JsonSerializer.Deserialize<List<Organization>>(organizationsJson, options);
                _organizationMap = organizations.ToDictionary(o => o.Name, o => o.Id);
                OrganizationComboBox.ItemsSource = _organizationMap.Keys.ToList();
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
