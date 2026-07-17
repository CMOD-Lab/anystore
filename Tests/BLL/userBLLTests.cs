using System;
using System.Collections.Generic;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the userBLL class.
    /// Tests cover property assignment, default values, boundary conditions, and data validation.
    /// </summary>
    public class userBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void UserBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var user = new userBLL();

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void UserBLL_DefaultConstructor_IdDefaultsToZero()
        {
            // Arrange & Act
            var user = new userBLL();

            // Assert
            Assert.Equal(0, user.id);
        }

        [Fact]
        public void UserBLL_DefaultConstructor_AddedByDefaultsToZero()
        {
            // Arrange & Act
            var user = new userBLL();

            // Assert
            Assert.Equal(0, user.added_by);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void UserBLL_SetId_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.id = 42;

            // Assert
            Assert.Equal(42, user.id);
        }

        [Fact]
        public void UserBLL_SetFirstName_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.first_name = "John";

            // Assert
            Assert.Equal("John", user.first_name);
        }

        [Fact]
        public void UserBLL_SetLastName_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.last_name = "Doe";

            // Assert
            Assert.Equal("Doe", user.last_name);
        }

        [Fact]
        public void UserBLL_SetEmail_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.email = "john.doe@example.com";

            // Assert
            Assert.Equal("john.doe@example.com", user.email);
        }

        [Fact]
        public void UserBLL_SetUsername_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.username = "johndoe";

            // Assert
            Assert.Equal("johndoe", user.username);
        }

        [Fact]
        public void UserBLL_SetPassword_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.password = "SecurePass123!";

            // Assert
            Assert.Equal("SecurePass123!", user.password);
        }

        [Fact]
        public void UserBLL_SetContact_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.contact = "555-1234";

            // Assert
            Assert.Equal("555-1234", user.contact);
        }

        [Fact]
        public void UserBLL_SetAddress_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.address = "123 Main Street";

            // Assert
            Assert.Equal("123 Main Street", user.address);
        }

        [Fact]
        public void UserBLL_SetGender_Male_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.gender = "Male";

            // Assert
            Assert.Equal("Male", user.gender);
        }

        [Fact]
        public void UserBLL_SetGender_Female_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.gender = "Female";

            // Assert
            Assert.Equal("Female", user.gender);
        }

        [Fact]
        public void UserBLL_SetUserType_Admin_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.user_type = "Admin";

            // Assert
            Assert.Equal("Admin", user.user_type);
        }

        [Fact]
        public void UserBLL_SetUserType_User_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.user_type = "User";

            // Assert
            Assert.Equal("User", user.user_type);
        }

        [Fact]
        public void UserBLL_SetAddedDate_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();
            var expectedDate = new DateTime(2024, 1, 15, 10, 30, 0);

            // Act
            user.added_date = expectedDate;

            // Assert
            Assert.Equal(expectedDate, user.added_date);
        }

        [Fact]
        public void UserBLL_SetAddedBy_ReturnsCorrectValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.added_by = 5;

            // Assert
            Assert.Equal(5, user.added_by);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void UserBLL_FullInitialization_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 6, 1);

            // Act
            var user = new userBLL
            {
                id = 1,
                first_name = "Alice",
                last_name = "Smith",
                email = "alice@example.com",
                username = "alicesmith",
                password = "password123",
                contact = "555-0001",
                address = "456 Oak Avenue",
                gender = "Female",
                user_type = "Admin",
                added_date = expectedDate,
                added_by = 1
            };

            // Assert
            Assert.Equal(1, user.id);
            Assert.Equal("Alice", user.first_name);
            Assert.Equal("Smith", user.last_name);
            Assert.Equal("alice@example.com", user.email);
            Assert.Equal("alicesmith", user.username);
            Assert.Equal("password123", user.password);
            Assert.Equal("555-0001", user.contact);
            Assert.Equal("456 Oak Avenue", user.address);
            Assert.Equal("Female", user.gender);
            Assert.Equal("Admin", user.user_type);
            Assert.Equal(expectedDate, user.added_date);
            Assert.Equal(1, user.added_by);
        }

        [Fact]
        public void UserBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var user1 = new userBLL { id = 1, first_name = "Alice", username = "alice" };
            var user2 = new userBLL { id = 2, first_name = "Bob", username = "bob" };

            // Assert
            Assert.NotEqual(user1.id, user2.id);
            Assert.NotEqual(user1.first_name, user2.first_name);
            Assert.NotEqual(user1.username, user2.username);
        }

        #endregion

        #region Boundary and Edge Case Tests

        [Fact]
        public void UserBLL_SetId_NegativeValue_IsAllowed()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.id = -1;

            // Assert
            Assert.Equal(-1, user.id);
        }

        [Fact]
        public void UserBLL_SetId_MaxInt_IsAllowed()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.id = int.MaxValue;

            // Assert
            Assert.Equal(int.MaxValue, user.id);
        }

        [Fact]
        public void UserBLL_SetFirstName_EmptyString_IsAllowed()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.first_name = string.Empty;

            // Assert
            Assert.Equal(string.Empty, user.first_name);
        }

        [Fact]
        public void UserBLL_SetEmail_WithSpecialChars_IsAllowed()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.email = "user+tag@sub.domain.com";

            // Assert
            Assert.Equal("user+tag@sub.domain.com", user.email);
        }

        [Fact]
        public void UserBLL_SetAddedDate_MinValue_IsAllowed()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.added_date = DateTime.MinValue;

            // Assert
            Assert.Equal(DateTime.MinValue, user.added_date);
        }

        [Fact]
        public void UserBLL_SetAddedDate_Today_IsAllowed()
        {
            // Arrange
            var user = new userBLL();
            var today = DateTime.Today;

            // Act
            user.added_date = today;

            // Assert
            Assert.Equal(today, user.added_date);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void UserBLL_Username_NotNullOrEmpty_IsValid()
        {
            // Arrange
            var user = new userBLL { username = "validuser" };

            // Assert
            Assert.False(string.IsNullOrEmpty(user.username));
        }

        [Fact]
        public void UserBLL_Password_NotNullOrEmpty_IsValid()
        {
            // Arrange
            var user = new userBLL { password = "validpassword" };

            // Assert
            Assert.False(string.IsNullOrEmpty(user.password));
        }

        [Fact]
        public void UserBLL_UserType_IsAdminOrUser_IsValid()
        {
            // Arrange
            var adminUser = new userBLL { user_type = "Admin" };
            var regularUser = new userBLL { user_type = "User" };

            // Assert
            Assert.True(adminUser.user_type == "Admin" || adminUser.user_type == "User");
            Assert.True(regularUser.user_type == "Admin" || regularUser.user_type == "User");
        }

        [Fact]
        public void UserBLL_AddedBy_PositiveValue_IsValid()
        {
            // Arrange
            var user = new userBLL { added_by = 1 };

            // Assert
            Assert.True(user.added_by > 0);
        }

        [Fact]
        public void UserBLL_Id_PositiveValue_IsValidForUpdate()
        {
            // Arrange
            var user = new userBLL { id = 10 };

            // Assert
            Assert.True(user.id > 0);
        }

        [Fact]
        public void UserBLL_FullName_ConcatenationWorks()
        {
            // Arrange
            var user = new userBLL { first_name = "John", last_name = "Doe" };

            // Act
            string fullName = user.first_name + " " + user.last_name;

            // Assert
            Assert.Equal("John Doe", fullName);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void UserBLL_ListOfUsers_CanBeCreated()
        {
            // Arrange & Act
            var users = new List<userBLL>
            {
                new userBLL { id = 1, username = "user1", user_type = "Admin" },
                new userBLL { id = 2, username = "user2", user_type = "User" },
                new userBLL { id = 3, username = "user3", user_type = "User" }
            };

            // Assert
            Assert.Equal(3, users.Count);
            Assert.Equal("user1", users[0].username);
            Assert.Equal("Admin", users[0].user_type);
        }

        [Fact]
        public void UserBLL_FilterByUserType_Admin_ReturnsOnlyAdmins()
        {
            // Arrange
            var users = new List<userBLL>
            {
                new userBLL { id = 1, username = "admin1", user_type = "Admin" },
                new userBLL { id = 2, username = "user1", user_type = "User" },
                new userBLL { id = 3, username = "admin2", user_type = "Admin" }
            };

            // Act
            var admins = users.FindAll(u => u.user_type == "Admin");

            // Assert
            Assert.Equal(2, admins.Count);
        }

        [Fact]
        public void UserBLL_FilterByUserType_User_ReturnsOnlyUsers()
        {
            // Arrange
            var users = new List<userBLL>
            {
                new userBLL { id = 1, username = "admin1", user_type = "Admin" },
                new userBLL { id = 2, username = "user1", user_type = "User" },
                new userBLL { id = 3, username = "user2", user_type = "User" }
            };

            // Act
            var regularUsers = users.FindAll(u => u.user_type == "User");

            // Assert
            Assert.Equal(2, regularUsers.Count);
        }

        #endregion
    }
}
