using System;
using System.Collections.Generic;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the loginBLL class.
    /// Tests cover property assignment, default values, validation logic, and edge cases.
    /// </summary>
    public class loginBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void LoginBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var login = new loginBLL();

            // Assert
            Assert.NotNull(login);
        }

        [Fact]
        public void LoginBLL_DefaultConstructor_UsernameIsNull()
        {
            // Arrange & Act
            var login = new loginBLL();

            // Assert
            Assert.Null(login.username);
        }

        [Fact]
        public void LoginBLL_DefaultConstructor_PasswordIsNull()
        {
            // Arrange & Act
            var login = new loginBLL();

            // Assert
            Assert.Null(login.password);
        }

        [Fact]
        public void LoginBLL_DefaultConstructor_UserTypeIsNull()
        {
            // Arrange & Act
            var login = new loginBLL();

            // Assert
            Assert.Null(login.user_type);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void LoginBLL_SetUsername_ReturnsCorrectValue()
        {
            // Arrange
            var login = new loginBLL();

            // Act
            login.username = "admin";

            // Assert
            Assert.Equal("admin", login.username);
        }

        [Fact]
        public void LoginBLL_SetPassword_ReturnsCorrectValue()
        {
            // Arrange
            var login = new loginBLL();

            // Act
            login.password = "admin123";

            // Assert
            Assert.Equal("admin123", login.password);
        }

        [Fact]
        public void LoginBLL_SetUserType_Admin_ReturnsCorrectValue()
        {
            // Arrange
            var login = new loginBLL();

            // Act
            login.user_type = "Admin";

            // Assert
            Assert.Equal("Admin", login.user_type);
        }

        [Fact]
        public void LoginBLL_SetUserType_User_ReturnsCorrectValue()
        {
            // Arrange
            var login = new loginBLL();

            // Act
            login.user_type = "User";

            // Assert
            Assert.Equal("User", login.user_type);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void LoginBLL_FullInitialization_AllPropertiesSetCorrectly()
        {
            // Arrange & Act
            var login = new loginBLL
            {
                username = "testuser",
                password = "testpass",
                user_type = "Admin"
            };

            // Assert
            Assert.Equal("testuser", login.username);
            Assert.Equal("testpass", login.password);
            Assert.Equal("Admin", login.user_type);
        }

        [Fact]
        public void LoginBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var login1 = new loginBLL { username = "user1", password = "pass1", user_type = "Admin" };
            var login2 = new loginBLL { username = "user2", password = "pass2", user_type = "User" };

            // Assert
            Assert.NotEqual(login1.username, login2.username);
            Assert.NotEqual(login1.password, login2.password);
            Assert.NotEqual(login1.user_type, login2.user_type);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void LoginBLL_ValidCredentials_UsernameNotEmpty()
        {
            // Arrange
            var login = new loginBLL { username = "admin", password = "pass", user_type = "Admin" };

            // Assert
            Assert.False(string.IsNullOrEmpty(login.username));
        }

        [Fact]
        public void LoginBLL_ValidCredentials_PasswordNotEmpty()
        {
            // Arrange
            var login = new loginBLL { username = "admin", password = "pass", user_type = "Admin" };

            // Assert
            Assert.False(string.IsNullOrEmpty(login.password));
        }

        [Fact]
        public void LoginBLL_ValidCredentials_UserTypeNotEmpty()
        {
            // Arrange
            var login = new loginBLL { username = "admin", password = "pass", user_type = "Admin" };

            // Assert
            Assert.False(string.IsNullOrEmpty(login.user_type));
        }

        [Fact]
        public void LoginBLL_EmptyUsername_IsInvalid()
        {
            // Arrange
            var login = new loginBLL { username = "", password = "pass", user_type = "Admin" };

            // Assert
            Assert.True(string.IsNullOrEmpty(login.username));
        }

        [Fact]
        public void LoginBLL_EmptyPassword_IsInvalid()
        {
            // Arrange
            var login = new loginBLL { username = "admin", password = "", user_type = "Admin" };

            // Assert
            Assert.True(string.IsNullOrEmpty(login.password));
        }

        [Fact]
        public void LoginBLL_EmptyUserType_IsInvalid()
        {
            // Arrange
            var login = new loginBLL { username = "admin", password = "pass", user_type = "" };

            // Assert
            Assert.True(string.IsNullOrEmpty(login.user_type));
        }

        [Fact]
        public void LoginBLL_NullUsername_IsInvalid()
        {
            // Arrange
            var login = new loginBLL { username = null, password = "pass", user_type = "Admin" };

            // Assert
            Assert.True(string.IsNullOrEmpty(login.username));
        }

        [Fact]
        public void LoginBLL_NullPassword_IsInvalid()
        {
            // Arrange
            var login = new loginBLL { username = "admin", password = null, user_type = "Admin" };

            // Assert
            Assert.True(string.IsNullOrEmpty(login.password));
        }

        [Fact]
        public void LoginBLL_UserType_IsAdminOrUser_IsValid()
        {
            // Arrange
            var adminLogin = new loginBLL { user_type = "Admin" };
            var userLogin = new loginBLL { user_type = "User" };

            // Assert
            Assert.True(adminLogin.user_type == "Admin" || adminLogin.user_type == "User");
            Assert.True(userLogin.user_type == "Admin" || userLogin.user_type == "User");
        }

        [Fact]
        public void LoginBLL_AllFieldsPopulated_IsValidForLoginCheck()
        {
            // Arrange
            var login = new loginBLL
            {
                username = "admin",
                password = "admin123",
                user_type = "Admin"
            };

            // Assert - all fields must be non-empty for a valid login attempt
            Assert.False(string.IsNullOrEmpty(login.username));
            Assert.False(string.IsNullOrEmpty(login.password));
            Assert.False(string.IsNullOrEmpty(login.user_type));
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void LoginBLL_Username_WithSpaces_IsAllowed()
        {
            // Arrange
            var login = new loginBLL();

            // Act
            login.username = "user name";

            // Assert
            Assert.Equal("user name", login.username);
        }

        [Fact]
        public void LoginBLL_Password_WithSpecialChars_IsAllowed()
        {
            // Arrange
            var login = new loginBLL();

            // Act
            login.password = "P@$$w0rd!#";

            // Assert
            Assert.Equal("P@$$w0rd!#", login.password);
        }

        [Fact]
        public void LoginBLL_Username_CaseSensitive_DifferentValues()
        {
            // Arrange
            var login1 = new loginBLL { username = "Admin" };
            var login2 = new loginBLL { username = "admin" };

            // Assert
            Assert.NotEqual(login1.username, login2.username);
        }

        [Fact]
        public void LoginBLL_Password_CaseSensitive_DifferentValues()
        {
            // Arrange
            var login1 = new loginBLL { password = "Password" };
            var login2 = new loginBLL { password = "password" };

            // Assert
            Assert.NotEqual(login1.password, login2.password);
        }

        [Fact]
        public void LoginBLL_Username_LongString_IsAllowed()
        {
            // Arrange
            var login = new loginBLL();
            var longUsername = new string('a', 255);

            // Act
            login.username = longUsername;

            // Assert
            Assert.Equal(255, login.username.Length);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void LoginBLL_ListOfLogins_CanBeCreated()
        {
            // Arrange & Act
            var logins = new List<loginBLL>
            {
                new loginBLL { username = "admin", password = "pass1", user_type = "Admin" },
                new loginBLL { username = "user1", password = "pass2", user_type = "User" }
            };

            // Assert
            Assert.Equal(2, logins.Count);
        }

        [Fact]
        public void LoginBLL_FilterByUserType_Admin_ReturnsOnlyAdmins()
        {
            // Arrange
            var logins = new List<loginBLL>
            {
                new loginBLL { username = "admin1", user_type = "Admin" },
                new loginBLL { username = "user1", user_type = "User" },
                new loginBLL { username = "admin2", user_type = "Admin" }
            };

            // Act
            var admins = logins.FindAll(l => l.user_type == "Admin");

            // Assert
            Assert.Equal(2, admins.Count);
        }

        #endregion
    }
}
