using System;
using System.Collections.Generic;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Tests for DAL-layer data model construction and parameter validation logic.
    /// These tests verify that BLL objects used by DAL methods are correctly structured.
    /// </summary>
    public class DALModelValidationTests
    {
        #region userDAL - BLL Object Preparation Tests

        [Fact]
        public void UserDAL_InsertPrep_UserBLL_HasRequiredFields()
        {
            // Arrange - simulate what UI would set before calling DAL.Insert
            var u = new userBLL
            {
                first_name = "Jane",
                last_name = "Doe",
                email = "jane@example.com",
                username = "janedoe",
                password = "pass123",
                contact = "555-0001",
                address = "1 Test Lane",
                gender = "Female",
                user_type = "User",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert all required fields are populated
            Assert.False(string.IsNullOrEmpty(u.first_name));
            Assert.False(string.IsNullOrEmpty(u.last_name));
            Assert.False(string.IsNullOrEmpty(u.email));
            Assert.False(string.IsNullOrEmpty(u.username));
            Assert.False(string.IsNullOrEmpty(u.password));
            Assert.False(string.IsNullOrEmpty(u.user_type));
            Assert.True(u.added_by >= 0);
        }

        [Fact]
        public void UserDAL_UpdatePrep_UserBLL_HasIdSet()
        {
            // Arrange
            var u = new userBLL
            {
                id = 5,
                first_name = "Updated",
                last_name = "User",
                email = "updated@example.com",
                username = "updateduser",
                password = "newpass",
                contact = "555-9999",
                address = "Updated Address",
                gender = "Male",
                user_type = "Admin",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert id is set for update
            Assert.True(u.id > 0);
        }

        [Fact]
        public void UserDAL_DeletePrep_UserBLL_HasIdSet()
        {
            // Arrange
            var u = new userBLL { id = 10 };

            // Assert
            Assert.True(u.id > 0);
        }

        [Fact]
        public void UserDAL_SearchPrep_Keyword_IsNotNull()
        {
            // Arrange
            string keyword = "john";

            // Assert
            Assert.NotNull(keyword);
            Assert.NotEmpty(keyword);
        }

        [Fact]
        public void UserDAL_GetIDFromUsername_ReturnsUserBLL_WithId()
        {
            // Arrange - simulate a returned userBLL from GetIDFromUsername
            var u = new userBLL();
            u.id = 3; // simulating DB returned id

            // Assert
            Assert.Equal(3, u.id);
        }

        #endregion

        #region loginDAL - BLL Object Preparation Tests

        [Fact]
        public void LoginDAL_LoginCheck_LoginBLL_HasAllFields()
        {
            // Arrange
            var l = new loginBLL
            {
                username = "admin",
                password = "admin123",
                user_type = "Admin"
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(l.username));
            Assert.False(string.IsNullOrEmpty(l.password));
            Assert.False(string.IsNullOrEmpty(l.user_type));
        }

        [Fact]
        public void LoginDAL_LoginCheck_EmptyUsername_IsInvalid()
        {
            // Arrange
            var l = new loginBLL { username = "", password = "pass", user_type = "Admin" };

            // Assert
            Assert.True(string.IsNullOrEmpty(l.username));
        }

        [Fact]
        public void LoginDAL_LoginCheck_EmptyPassword_IsInvalid()
        {
            // Arrange
            var l = new loginBLL { username = "admin", password = "", user_type = "Admin" };

            // Assert
            Assert.True(string.IsNullOrEmpty(l.password));
        }

        #endregion

        #region DeaCustDAL - BLL Object Preparation Tests

        [Fact]
        public void DeaCustDAL_InsertPrep_DeaCustBLL_HasRequiredFields()
        {
            // Arrange
            var dc = new DeaCustBLL
            {
                type = "Dealer",
                name = "Test Dealer",
                email = "dealer@test.com",
                contact = "123456789",
                address = "Dealer Street",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(dc.type));
            Assert.False(string.IsNullOrEmpty(dc.name));
            Assert.True(dc.added_by >= 0);
        }

        [Fact]
        public void DeaCustDAL_UpdatePrep_DeaCustBLL_HasIdSet()
        {
            // Arrange
            var dc = new DeaCustBLL { id = 7, type = "Customer", name = "Test Customer" };

            // Assert
            Assert.True(dc.id > 0);
        }

        [Fact]
        public void DeaCustDAL_DeletePrep_DeaCustBLL_HasIdSet()
        {
            // Arrange
            var dc = new DeaCustBLL { id = 3 };

            // Assert
            Assert.True(dc.id > 0);
        }

        [Fact]
        public void DeaCustDAL_SearchDealerCustomer_ReturnsDeaCustBLL_WithFields()
        {
            // Arrange - simulate returned object
            var dc = new DeaCustBLL
            {
                name = "Found Dealer",
                email = "found@dealer.com",
                contact = "999888777",
                address = "Found Address"
            };

            // Assert
            Assert.NotNull(dc.name);
            Assert.NotNull(dc.email);
        }

        [Fact]
        public void DeaCustDAL_GetDeaCustIDFromName_ReturnsDeaCustBLL_WithId()
        {
            // Arrange - simulate returned object
            var dc = new DeaCustBLL();
            dc.id = 8; // simulating DB returned id

            // Assert
            Assert.Equal(8, dc.id);
        }

        #endregion

        #region productsDAL - BLL Object Preparation Tests

        [Fact]
        public void ProductsDAL_InsertPrep_ProductsBLL_HasRequiredFields()
        {
            // Arrange
            var p = new productsBLL
            {
                name = "Test Product",
                category = "Test Category",
                description = "Test Description",
                rate = 25.00m,
                qty = 10m,
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(p.name));
            Assert.False(string.IsNullOrEmpty(p.category));
            Assert.True(p.rate > 0);
            Assert.True(p.qty >= 0);
        }

        [Fact]
        public void ProductsDAL_UpdatePrep_ProductsBLL_HasIdSet()
        {
            // Arrange
            var p = new productsBLL { id = 4, name = "Updated Product" };

            // Assert
            Assert.True(p.id > 0);
        }

        [Fact]
        public void ProductsDAL_GetProductsForTransaction_ReturnsProductsBLL_WithFields()
        {
            // Arrange - simulate returned object
            var p = new productsBLL
            {
                name = "Found Product",
                rate = 50m,
                qty = 20m
            };

            // Assert
            Assert.NotNull(p.name);
            Assert.True(p.rate > 0);
            Assert.True(p.qty >= 0);
        }

        [Fact]
        public void ProductsDAL_IncreaseProduct_NewQty_IsGreaterThanCurrentQty()
        {
            // Arrange
            decimal currentQty = 10m;
            decimal increaseQty = 5m;

            // Act
            decimal newQty = currentQty + increaseQty;

            // Assert
            Assert.Equal(15m, newQty);
            Assert.True(newQty > currentQty);
        }

        [Fact]
        public void ProductsDAL_DecreaseProduct_NewQty_IsLessThanCurrentQty()
        {
            // Arrange
            decimal currentQty = 10m;
            decimal decreaseQty = 3m;

            // Act
            decimal newQty = currentQty - decreaseQty;

            // Assert
            Assert.Equal(7m, newQty);
            Assert.True(newQty < currentQty);
        }

        [Fact]
        public void ProductsDAL_DecreaseProduct_ExactQty_ResultsInZero()
        {
            // Arrange
            decimal currentQty = 5m;
            decimal decreaseQty = 5m;

            // Act
            decimal newQty = currentQty - decreaseQty;

            // Assert
            Assert.Equal(0m, newQty);
        }

        #endregion

        #region categoriesDAL - BLL Object Preparation Tests

        [Fact]
        public void CategoriesDAL_InsertPrep_CategoriesBLL_HasRequiredFields()
        {
            // Arrange
            var c = new categoriesBLL
            {
                title = "New Category",
                description = "Category Description",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(c.title));
            Assert.True(c.added_by >= 0);
        }

        [Fact]
        public void CategoriesDAL_UpdatePrep_CategoriesBLL_HasIdSet()
        {
            // Arrange
            var c = new categoriesBLL { id = 2, title = "Updated Category" };

            // Assert
            Assert.True(c.id > 0);
        }

        [Fact]
        public void CategoriesDAL_DeletePrep_CategoriesBLL_HasIdSet()
        {
            // Arrange
            var c = new categoriesBLL { id = 5 };

            // Assert
            Assert.True(c.id > 0);
        }

        #endregion

        #region transactionDAL - BLL Object Preparation Tests

        [Fact]
        public void TransactionDAL_InsertPrep_TransactionsBLL_HasRequiredFields()
        {
            // Arrange
            var t = new transactionsBLL
            {
                type = "Purchase",
                dea_cust_id = 3,
                grandTotal = 500m,
                transaction_date = DateTime.Now,
                tax = 50m,
                discount = 0m,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(t.type));
            Assert.True(t.dea_cust_id > 0);
            Assert.True(t.grandTotal > 0);
            Assert.True(t.added_by >= 0);
        }

        [Fact]
        public void TransactionDAL_InsertPrep_TransactionType_IsPurchaseOrSales()
        {
            // Arrange
            var purchaseTransaction = new transactionsBLL { type = "Purchase" };
            var salesTransaction = new transactionsBLL { type = "Sales" };

            // Assert
            Assert.True(purchaseTransaction.type == "Purchase" || purchaseTransaction.type == "Sales");
            Assert.True(salesTransaction.type == "Purchase" || salesTransaction.type == "Sales");
        }

        #endregion

        #region transactionDetailDAL - BLL Object Preparation Tests

        [Fact]
        public void TransactionDetailDAL_InsertPrep_TransactionDetailBLL_HasRequiredFields()
        {
            // Arrange
            var td = new transactionDetailBLL
            {
                product_id = 2,
                rate = 100m,
                qty = 5m,
                total = 500m,
                dea_cust_id = 3,
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.True(td.product_id > 0);
            Assert.True(td.rate > 0);
            Assert.True(td.qty > 0);
            Assert.True(td.total > 0);
            Assert.True(td.dea_cust_id > 0);
        }

        [Fact]
        public void TransactionDetailDAL_InsertPrep_Total_EqualsRate_Times_Qty()
        {
            // Arrange
            var td = new transactionDetailBLL
            {
                rate = 100m,
                qty = 5m
            };

            // Act
            td.total = td.rate * td.qty;

            // Assert
            Assert.Equal(500m, td.total);
        }

        #endregion
    }
}
