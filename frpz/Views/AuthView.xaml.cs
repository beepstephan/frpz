using System.Windows;
using frpz.ViewModels;
using frpz.Helpers;
using frpz.Models;

namespace frpz.Views
{
    public partial class AuthView : Window
    {
        private UserVM _viewModel = new UserVM();

        public AuthView()
        {
            InitializeComponent();
        }

        private async void RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            var email = RegisterEmail.Text;
            var password = RegisterPassword.Password;
            var username = RegisterUsername.Text;

            await _viewModel.RegisterUserAsync(username, email, password);
            MessageBox.Show("Реєстрація успішна! Будь ласка, увійдіть.");

            SwitchToLogin(null, null);
        }

        private void LoginUser_Click(object sender, RoutedEventArgs e)
        {
            var email = LoginEmail.Text;
            var password = LoginPassword.Password;

            var user = _viewModel.AuthenticateUser(email, password);
            if (user != null)
            {
                // Зберігаємо дані користувача у глобальному стані
                CurrentUser.LoggedInUser = user;

                MessageBox.Show($"Ласкаво просимо, {user.Username} ({user.Role})!");

                if (user.Role == "Адміністратор")
                {
                    OpenAdminView();
                }
                else if (user.Role == "Менеджер")
                {
                    OpenManagerView();
                }
                else if (user.Role == "Користувач")
                {
                    OpenTestSelectionView();
                }
            }
            else
            {
                MessageBox.Show("Неправильний email або пароль.");
            }
        }

        private void SwitchToRegister(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }

        private void SwitchToLogin(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void OpenAdminView()
        {
            var adminView = new AdminView();
            adminView.Show();
            this.Close();
        }

        private void OpenManagerView()
        {
            var managerView = new ManagerView();
            managerView.Show();
            this.Close();
        }

        private void OpenTestSelectionView()
        {
            var testSelectionView = new TestSelectionView();
            testSelectionView.Show();
            this.Close();
        }
    }
}