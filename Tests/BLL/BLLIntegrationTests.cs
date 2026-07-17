using System;
using System.Collections.Generic;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Tests for BLL model interactions and business logic validation.
    /// These tests cover cross-model scenarios and data integrity checks.
    /// </summary>
    public class BLLIntegrationTests
    {
        #region userBLL and loginBLL Interaction Tests

        [Fact]
        public void UserBLL_UsernameMatchesLoginBLL_Username()
        {
            // Arrange
            var user = new userBLL { username = "testadmin" };
            var login = new loginBLL { username = "testadmin" };

            // Act & Assert
            Assert.Equal(user.username, login.username);
        }

        [Fact]
        public void UserBLL_PasswordMatchesLoginBLL_Password()
        {
            // Arrange
            var user = new userBLL { password = "secret123" };
            var login = new loginBLL { password = "secret123" };

            // Act & Assert
            Assert.Equal(user.password, login.password);
        }

        [Fact]
        public void UserBLL_UserTypeMatchesLoginBLL_UserType()
        {
            // Arrange
            var user = new userBLL { user_type = "Admin" };
            var login = new loginBLL { user_type = "Admin" };

            // Act & Assert
            Assert.Equal(user.user_type, login.user_type);
        }

        #endregion

        #region transactionsBLL and transactionDetailBLL Interaction Tests

        [Fact]
        public void TransactionsBLL_GrandTotal_EqualsSum_Of_TransactionDetails()
        {
            // Arrange
            var t = new transactionsBLL();
            var td1 = new transactionDetailBLL { rate = 100m, qty = 2m, total = 200m };
            var td2 = new transactionDetailBLL { rate = 50m, qty = 3m, total = 150m };

            // Act
            t.grandTotal = td1.total + td2.total;

            // Assert
            Assert.Equal(350m, t.grandTotal);
        }

        [Fact]
        public void TransactionDetailBLL_Total_EqualsRate_Times_Qty()
        {
            // Arrange
            var td = new transactionDetailBLL { rate = 75m, qty = 4m };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(300m, td.total);
        }

        [Fact]
        public void TransactionsBLL_GrandTotal_WithTaxAndDiscount_CalculatesCorrectly()
        {
            // Arrange
            var t = new transactionsBLL
            {
                grandTotal = 1000m,
                tax = 100m,
                discount = 50m
            };

            // Act
            decimal netTotal = t.grandTotal + t.tax - t.discount;

            // Assert
            Assert.Equal(1050m, netTotal);
        }

        #endregion

        #region productsBLL and categoriesBLL Interaction Tests

        [Fact]
        public void ProductsBLL_Category_MatchesCategoriesBLL_Title()
        {
            // Arrange
            var category = new categoriesBLL { title = "Electronics" };
            var product = new productsBLL { category = "Electronics" };

            // Act & Assert
            Assert.Equal(category.title, product.category);
        }

        [Fact]
        public void ProductsBLL_AddedBy_MatchesUserBLL_Id()
        {
            // Arrange
            var user = new userBLL { id = 1 };
            var product = new productsBLL { added_by = 1 };

            // Act & Assert
            Assert.Equal(user.id, product.added_by);
        }

        #endregion

        #region DeaCustBLL Interaction Tests

        [Fact]
        public void DeaCustBLL_AddedBy_MatchesUserBLL_Id()
        {
            // Arrange
            var user = new userBLL { id = 2 };
            var dc = new DeaCustBLL { added_by = 2 };

            // Act & Assert
            Assert.Equal(user.id, dc.added_by);
        }

        [Fact]
        public void TransactionsBLL_DeaCustId_MatchesDeaCustBLL_Id()
        {
            // Arrange
            var dc = new DeaCustBLL { id = 5 };
            var t = new transactionsBLL { dea_cust_id = 5 };

            // Act & Assert
            Assert.Equal(dc.id, t.dea_cust_id);
        }

        #endregion

        #region Collection / List Tests

        [Fact]
        public void UserBLL_ListOfUsers_CanBeCreatedAndPopulated()
        {
            // Arrange
            var users = new List<userBLL>();

            // Act
            users.Add(new userBLL { id = 1, username = "user1" });
            users.Add(new userBLL { id = 2, username = "user2" });
            users.Add(new userBLL { id = 3, username = "user3" });

            // Assert
            Assert.Equal(3, users.Count);
            Assert.Equal("user1", users[0].username);
            Assert.Equal("user2", users[1].username);
            Assert.Equal("user3", users[2].username);
        }

        [Fact]
        public void ProductsBLL_ListOfProducts_CanBeCreatedAndPopulated()
        {
            // Arrange
            var products = new List<productsBLL>();

            // Act
            products.Add(new productsBLL { id = 1, name = "Product A", rate = 10m });
            products.Add(new productsBLL { id = 2, name = "Product B", rate = 20m });

            // Assert
            Assert.Equal(2, products.Count);
            Assert.Equal("Product A", products[0].name);
            Assert.Equal(20m, products[1].rate);
        }

        [Fact]
        public void CategoriesBLL_ListOfCategories_CanBeCreatedAndPopulated()
        {
            // Arrange
            var categories = new List<categoriesBLL>();

            // Act
            categories.Add(new categoriesBLL { id = 1, title = "Electronics" });
            categories.Add(new categoriesBLL { id = 2, title = "Clothing" });
            categories.Add(new categoriesBLL { id = 3, title = "Food" });

            // Assert
            Assert.Equal(3, categories.Count);
            Assert.Equal("Electronics", categories[0].title);
        }

        [Fact]
        public void DeaCustBLL_ListOfDealersAndCustomers_CanBeFiltered()
        {
            // Arrange
            var list = new List<DeaCustBLL>
            {
                new DeaCustBLL { id = 1, type = "Dealer", name = "Dealer A" },
                new DeaCustBLL { id = 2, type = "Customer", name = "Customer B" },
                new DeaCustBLL { id = 3, type = "Dealer", name = "Dealer C" }
            };

            // Act
            var dealers = list.FindAll(dc => dc.type == "Dealer");
            var customers = list.FindAll(dc => dc.type == "Customer");

            // Assert
            Assert.Equal(2, dealers.Count);
            Assert.Single(customers);
        }

        #endregion

        #region Date Validation Tests

        [Fact]
        public void UserBLL_AddedDate_CanBeSetToToday()
        {
            // Arrange
            var user = new userBLL();
            var today = DateTime.Today;

            // Act
            user.added_date = today;

            // Assert
            Assert.Equal(today, user.added_date);
        }

        [Fact]
        public void TransactionsBLL_TransactionDate_CanBeSetToNow()
        {
            // Arrange
            var t = new transactionsBLL();
            var now = DateTime.Now;

            // Act
            t.transaction_date = now;

            // Assert
            Assert.Equal(now, t.transaction_date);
        }

        [Fact]
        public void CategoriesBLL_AddedDate_IsNotDefaultMinValue_WhenSet()
        {
            // Arrange
            var c = new categoriesBLL();

            // Act
            c.added_date = DateTime.Now;

            // Assert
            Assert.NotEqual(DateTime.MinValue, c.added_date);
        }

        #endregion

        #region Boundary Value Tests

        [Fact]
        public void ProductsBLL_Rate_CanHandleVerySmallDecimal()
        {
            // Arrange
            var p = new productsBLL();

            // Act
            p.rate = 0.01m;

            // Assert
            Assert.Equal(0.01m, p.rate);
        }

        [Fact]
        public void TransactionDetailBLL_Qty_CanHandleFractionalValue()
        {
            // Arrange
            var td = new transactionDetailBLL();

            // Act
            td.qty = 0.5m;

            // Assert
            Assert.Equal(0.5m, td.qty);
        }

        [Fact]
        public void UserBLL_Id_CanHandleMaxIntValue()
        {
            // Arrange
            var user = new userBLL();

            // Act
            user.id = int.MaxValue;

            // Assert
            Assert.Equal(int.MaxValue, user.id);
        }

        [Fact]
        public void TransactionsBLL_GrandTotal_CanHandleLargeDecimal()
        {
            // Arrange
            var t = new transactionsBLL();

            // Act
            t.grandTotal = 9999999.99m;

            // Assert
            Assert.Equal(9999999.99m, t.grandTotal);
        }

        #endregion
    }
}
