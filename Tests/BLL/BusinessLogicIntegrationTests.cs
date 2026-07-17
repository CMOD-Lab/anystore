using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Integration-style tests covering cross-BLL business logic scenarios.
    /// Tests simulate real-world workflows: purchase transactions, sales transactions,
    /// inventory management, user management, and category-product relationships.
    /// </summary>
    public class BusinessLogicIntegrationTests
    {
        #region Purchase Transaction Workflow Tests

        [Fact]
        public void PurchaseWorkflow_CreateTransaction_WithDetails_IsValid()
        {
            // Arrange - simulate a complete purchase transaction
            var dealer = new DeaCustBLL
            {
                id = 1,
                type = "Dealer",
                name = "ABC Wholesale",
                email = "abc@wholesale.com",
                contact = "555-1000",
                address = "100 Dealer Street"
            };

            var product = new productsBLL
            {
                id = 1,
                name = "Widget",
                category = "Hardware",
                rate = 50m,
                qty = 100m
            };

            var transactionDetail = new transactionDetailBLL
            {
                product_id = product.id,
                rate = product.rate,
                qty = 10m,
                dea_cust_id = dealer.id,
                added_date = DateTime.Now,
                added_by = 1
            };
            transactionDetail.total = transactionDetail.rate * transactionDetail.qty;

            var transaction = new transactionsBLL
            {
                type = "Purchase",
                dea_cust_id = dealer.id,
                grandTotal = transactionDetail.total,
                transaction_date = DateTime.Now,
                tax = 0m,
                discount = 0m,
                added_by = 1
            };

            // Assert
            Assert.Equal("Purchase", transaction.type);
            Assert.Equal(dealer.id, transaction.dea_cust_id);
            Assert.Equal(500m, transactionDetail.total);
            Assert.Equal(500m, transaction.grandTotal);
        }

        [Fact]
        public void PurchaseWorkflow_InventoryIncrease_AfterPurchase_IsCorrect()
        {
            // Arrange
            var product = new productsBLL { id = 1, name = "Widget", qty = 50m };
            decimal purchaseQty = 25m;

            // Act - simulate inventory increase after purchase
            decimal newQty = product.qty + purchaseQty;
            product.qty = newQty;

            // Assert
            Assert.Equal(75m, product.qty);
            Assert.True(product.qty > 50m);
        }

        [Fact]
        public void PurchaseWorkflow_MultipleItems_GrandTotalCalculated()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { product_id = 1, rate = 100m, qty = 5m },
                new transactionDetailBLL { product_id = 2, rate = 50m, qty = 10m },
                new transactionDetailBLL { product_id = 3, rate = 75m, qty = 4m }
            };

            // Act - calculate totals for each detail
            foreach (var td in details)
            {
                td.total = td.rate * td.qty;
            }

            decimal subtotal = details.Sum(td => td.total);
            decimal tax = subtotal * 0.10m;
            decimal discount = 50m;
            decimal grandTotal = subtotal + tax - discount;

            var transaction = new transactionsBLL
            {
                type = "Purchase",
                grandTotal = grandTotal,
                tax = tax,
                discount = discount
            };

            // Assert
            Assert.Equal(500m, details[0].total);
            Assert.Equal(500m, details[1].total);
            Assert.Equal(300m, details[2].total);
            Assert.Equal(1300m, subtotal);
            Assert.Equal(130m, tax);
            Assert.Equal(1380m, grandTotal);
            Assert.Equal(1380m, transaction.grandTotal);
        }

        #endregion

        #region Sales Transaction Workflow Tests

        [Fact]
        public void SalesWorkflow_CreateTransaction_WithCustomer_IsValid()
        {
            // Arrange
            var customer = new DeaCustBLL
            {
                id = 5,
                type = "Customer",
                name = "John Buyer",
                email = "john@buyer.com",
                contact = "555-2000"
            };

            var product = new productsBLL
            {
                id = 2,
                name = "Gadget",
                rate = 200m,
                qty = 30m
            };

            var transactionDetail = new transactionDetailBLL
            {
                product_id = product.id,
                rate = product.rate,
                qty = 2m,
                dea_cust_id = customer.id,
                added_date = DateTime.Now,
                added_by = 1
            };
            transactionDetail.total = transactionDetail.rate * transactionDetail.qty;

            var transaction = new transactionsBLL
            {
                type = "Sales",
                dea_cust_id = customer.id,
                grandTotal = transactionDetail.total,
                transaction_date = DateTime.Now,
                tax = 40m,
                discount = 0m,
                added_by = 1
            };

            // Assert
            Assert.Equal("Sales", transaction.type);
            Assert.Equal(customer.id, transaction.dea_cust_id);
            Assert.Equal(400m, transactionDetail.total);
        }

        [Fact]
        public void SalesWorkflow_InventoryDecrease_AfterSale_IsCorrect()
        {
            // Arrange
            var product = new productsBLL { id = 2, name = "Gadget", qty = 30m };
            decimal soldQty = 5m;

            // Act - simulate inventory decrease after sale
            decimal newQty = product.qty - soldQty;
            product.qty = newQty;

            // Assert
            Assert.Equal(25m, product.qty);
            Assert.True(product.qty < 30m);
        }

        [Fact]
        public void SalesWorkflow_InsufficientStock_QtyWouldBeNegative()
        {
            // Arrange
            var product = new productsBLL { id = 2, name = "Gadget", qty = 5m };
            decimal requestedQty = 10m;

            // Act
            bool hasSufficientStock = product.qty >= requestedQty;
            decimal potentialNewQty = product.qty - requestedQty;

            // Assert
            Assert.False(hasSufficientStock);
            Assert.True(potentialNewQty < 0);
        }

        #endregion

        #region User Authentication Workflow Tests

        [Fact]
        public void AuthWorkflow_AdminLogin_HasCorrectUserType()
        {
            // Arrange
            var loginAttempt = new loginBLL
            {
                username = "admin",
                password = "admin123",
                user_type = "Admin"
            };

            // Assert
            Assert.Equal("Admin", loginAttempt.user_type);
            Assert.False(string.IsNullOrEmpty(loginAttempt.username));
            Assert.False(string.IsNullOrEmpty(loginAttempt.password));
        }

        [Fact]
        public void AuthWorkflow_UserLogin_HasCorrectUserType()
        {
            // Arrange
            var loginAttempt = new loginBLL
            {
                username = "regularuser",
                password = "userpass",
                user_type = "User"
            };

            // Assert
            Assert.Equal("User", loginAttempt.user_type);
        }

        [Fact]
        public void AuthWorkflow_GetUserAfterLogin_UserBLLHasMatchingUsername()
        {
            // Arrange
            var loginAttempt = new loginBLL { username = "johndoe", password = "pass", user_type = "User" };
            var userRecord = new userBLL
            {
                id = 3,
                username = "johndoe",
                first_name = "John",
                last_name = "Doe",
                user_type = "User"
            };

            // Assert - username matches between login and user record
            Assert.Equal(loginAttempt.username, userRecord.username);
            Assert.Equal(loginAttempt.user_type, userRecord.user_type);
        }

        #endregion

        #region Category-Product Relationship Tests

        [Fact]
        public void CategoryProduct_ProductBelongsToCategory_IsValid()
        {
            // Arrange
            var category = new categoriesBLL { id = 1, title = "Electronics" };
            var product = new productsBLL
            {
                id = 1,
                name = "Laptop",
                category = category.title,
                rate = 999.99m,
                qty = 10m
            };

            // Assert
            Assert.Equal(category.title, product.category);
        }

        [Fact]
        public void CategoryProduct_MultipleProductsInCategory_FilteredCorrectly()
        {
            // Arrange
            var electronics = new categoriesBLL { id = 1, title = "Electronics" };
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Laptop", category = "Electronics", rate = 999m },
                new productsBLL { id = 2, name = "Phone", category = "Electronics", rate = 599m },
                new productsBLL { id = 3, name = "Shirt", category = "Clothing", rate = 29m },
                new productsBLL { id = 4, name = "Tablet", category = "Electronics", rate = 399m }
            };

            // Act
            var electronicsProducts = products.FindAll(p => p.category == electronics.title);

            // Assert
            Assert.Equal(3, electronicsProducts.Count);
        }

        [Fact]
        public void CategoryProduct_CategoryDeleted_ProductsOrphaned()
        {
            // Arrange
            var categories = new List<categoriesBLL>
            {
                new categoriesBLL { id = 1, title = "Electronics" },
                new categoriesBLL { id = 2, title = "Hardware" }
            };

            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Laptop", category = "Electronics" },
                new productsBLL { id = 2, name = "Hammer", category = "Hardware" }
            };

            // Act - simulate category deletion
            categories.RemoveAll(c => c.id == 1);
            var orphanedProducts = products.FindAll(p => !categories.Any(c => c.title == p.category));

            // Assert
            Assert.Single(orphanedProducts);
            Assert.Equal("Laptop", orphanedProducts[0].name);
        }

        #endregion

        #region Inventory Management Tests

        [Fact]
        public void Inventory_LowStockProducts_IdentifiedCorrectly()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Widget", qty = 5m },
                new productsBLL { id = 2, name = "Gadget", qty = 50m },
                new productsBLL { id = 3, name = "Doohickey", qty = 2m },
                new productsBLL { id = 4, name = "Thingamajig", qty = 100m }
            };

            decimal lowStockThreshold = 10m;

            // Act
            var lowStockProducts = products.FindAll(p => p.qty < lowStockThreshold);

            // Assert
            Assert.Equal(2, lowStockProducts.Count);
            Assert.Contains(lowStockProducts, p => p.name == "Widget");
            Assert.Contains(lowStockProducts, p => p.name == "Doohickey");
        }

        [Fact]
        public void Inventory_OutOfStockProducts_IdentifiedCorrectly()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Widget", qty = 0m },
                new productsBLL { id = 2, name = "Gadget", qty = 50m },
                new productsBLL { id = 3, name = "Doohickey", qty = 0m }
            };

            // Act
            var outOfStockProducts = products.FindAll(p => p.qty == 0m);

            // Assert
            Assert.Equal(2, outOfStockProducts.Count);
        }

        [Fact]
        public void Inventory_TotalInventoryValue_CalculatedCorrectly()
        {
            // Arrange
            var products = new List<productsBLL>
            {
                new productsBLL { id = 1, name = "Widget", rate = 10m, qty = 100m },
                new productsBLL { id = 2, name = "Gadget", rate = 50m, qty = 20m },
                new productsBLL { id = 3, name = "Doohickey", rate = 25m, qty = 40m }
            };

            // Act
            decimal totalValue = products.Sum(p => p.rate * p.qty);

            // Assert
            Assert.Equal(3000m, totalValue); // 1000 + 1000 + 1000
        }

        #endregion

        #region Transaction Summary Tests

        [Fact]
        public void TransactionSummary_TotalPurchases_CalculatedCorrectly()
        {
            // Arrange
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { type = "Sales", grandTotal = 500m },
                new transactionsBLL { type = "Purchase", grandTotal = 750m },
                new transactionsBLL { type = "Sales", grandTotal = 300m }
            };

            // Act
            decimal totalPurchases = transactions
                .Where(t => t.type == "Purchase")
                .Sum(t => t.grandTotal);

            // Assert
            Assert.Equal(1750m, totalPurchases);
        }

        [Fact]
        public void TransactionSummary_TotalSales_CalculatedCorrectly()
        {
            // Arrange
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { type = "Sales", grandTotal = 500m },
                new transactionsBLL { type = "Purchase", grandTotal = 750m },
                new transactionsBLL { type = "Sales", grandTotal = 300m }
            };

            // Act
            decimal totalSales = transactions
                .Where(t => t.type == "Sales")
                .Sum(t => t.grandTotal);

            // Assert
            Assert.Equal(800m, totalSales);
        }

        [Fact]
        public void TransactionSummary_Profit_SalesMinusPurchases_IsCorrect()
        {
            // Arrange
            var transactions = new List<transactionsBLL>
            {
                new transactionsBLL { type = "Purchase", grandTotal = 1000m },
                new transactionsBLL { type = "Sales", grandTotal = 1500m }
            };

            // Act
            decimal totalPurchases = transactions.Where(t => t.type == "Purchase").Sum(t => t.grandTotal);
            decimal totalSales = transactions.Where(t => t.type == "Sales").Sum(t => t.grandTotal);
            decimal profit = totalSales - totalPurchases;

            // Assert
            Assert.Equal(500m, profit);
            Assert.True(profit > 0);
        }

        #endregion

        #region User Management Tests

        [Fact]
        public void UserManagement_AdminCanAddUser_UserHasAddedBySet()
        {
            // Arrange
            var admin = new userBLL { id = 1, username = "admin", user_type = "Admin" };
            var newUser = new userBLL
            {
                first_name = "New",
                last_name = "User",
                username = "newuser",
                password = "pass123",
                user_type = "User",
                added_by = admin.id,
                added_date = DateTime.Now
            };

            // Assert
            Assert.Equal(admin.id, newUser.added_by);
            Assert.Equal("User", newUser.user_type);
        }

        [Fact]
        public void UserManagement_UserList_ContainsAdminAndUsers()
        {
            // Arrange
            var users = new List<userBLL>
            {
                new userBLL { id = 1, username = "admin", user_type = "Admin" },
                new userBLL { id = 2, username = "user1", user_type = "User" },
                new userBLL { id = 3, username = "user2", user_type = "User" }
            };

            // Act
            var admins = users.FindAll(u => u.user_type == "Admin");
            var regularUsers = users.FindAll(u => u.user_type == "User");

            // Assert
            Assert.Single(admins);
            Assert.Equal(2, regularUsers.Count);
        }

        [Fact]
        public void UserManagement_FindUserByUsername_ReturnsCorrectUser()
        {
            // Arrange
            var users = new List<userBLL>
            {
                new userBLL { id = 1, username = "admin", first_name = "Admin" },
                new userBLL { id = 2, username = "johndoe", first_name = "John" },
                new userBLL { id = 3, username = "janedoe", first_name = "Jane" }
            };

            // Act
            var found = users.Find(u => u.username == "johndoe");

            // Assert
            Assert.NotNull(found);
            Assert.Equal(2, found.id);
            Assert.Equal("John", found.first_name);
        }

        #endregion

        #region Dealer/Customer Management Tests

        [Fact]
        public void DeaCustManagement_DealerList_FilteredFromCustomers()
        {
            // Arrange
            var entities = new List<DeaCustBLL>
            {
                new DeaCustBLL { id = 1, type = "Dealer", name = "Dealer A" },
                new DeaCustBLL { id = 2, type = "Customer", name = "Customer B" },
                new DeaCustBLL { id = 3, type = "Dealer", name = "Dealer C" },
                new DeaCustBLL { id = 4, type = "Customer", name = "Customer D" }
            };

            // Act
            var dealers = entities.FindAll(dc => dc.type == "Dealer");
            var customers = entities.FindAll(dc => dc.type == "Customer");

            // Assert
            Assert.Equal(2, dealers.Count);
            Assert.Equal(2, customers.Count);
            Assert.Equal(4, entities.Count);
        }

        [Fact]
        public void DeaCustManagement_TransactionLinkedToDealer_IsValid()
        {
            // Arrange
            var dealer = new DeaCustBLL { id = 3, type = "Dealer", name = "Supplier Co" };
            var transaction = new transactionsBLL
            {
                type = "Purchase",
                dea_cust_id = dealer.id,
                grandTotal = 2500m
            };

            // Assert
            Assert.Equal(dealer.id, transaction.dea_cust_id);
            Assert.Equal("Purchase", transaction.type);
        }

        [Fact]
        public void DeaCustManagement_TransactionLinkedToCustomer_IsValid()
        {
            // Arrange
            var customer = new DeaCustBLL { id = 7, type = "Customer", name = "Buyer Inc" };
            var transaction = new transactionsBLL
            {
                type = "Sales",
                dea_cust_id = customer.id,
                grandTotal = 800m
            };

            // Assert
            Assert.Equal(customer.id, transaction.dea_cust_id);
            Assert.Equal("Sales", transaction.type);
        }

        #endregion

        #region Data Integrity Tests

        [Fact]
        public void DataIntegrity_TransactionDetail_TotalMatchesRateTimesQty()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { product_id = 1, rate = 100m, qty = 5m, total = 500m },
                new transactionDetailBLL { product_id = 2, rate = 50m, qty = 8m, total = 400m },
                new transactionDetailBLL { product_id = 3, rate = 25m, qty = 12m, total = 300m }
            };

            // Assert - verify each total matches rate * qty
            foreach (var td in details)
            {
                Assert.Equal(td.rate * td.qty, td.total);
            }
        }

        [Fact]
        public void DataIntegrity_Transaction_GrandTotalMatchesSumOfDetails()
        {
            // Arrange
            var details = new List<transactionDetailBLL>
            {
                new transactionDetailBLL { rate = 100m, qty = 5m, total = 500m },
                new transactionDetailBLL { rate = 50m, qty = 8m, total = 400m }
            };

            decimal expectedGrandTotal = details.Sum(td => td.total);

            var transaction = new transactionsBLL
            {
                type = "Purchase",
                grandTotal = expectedGrandTotal,
                tax = 0m,
                discount = 0m
            };

            // Assert
            Assert.Equal(900m, transaction.grandTotal);
            Assert.Equal(expectedGrandTotal, transaction.grandTotal);
        }

        [Fact]
        public void DataIntegrity_User_AddedByReferencesExistingUser()
        {
            // Arrange
            var adminUser = new userBLL { id = 1, username = "admin", user_type = "Admin" };
            var newUser = new userBLL
            {
                id = 2,
                username = "newuser",
                added_by = adminUser.id
            };

            // Assert
            Assert.Equal(adminUser.id, newUser.added_by);
            Assert.True(newUser.added_by > 0);
        }

        #endregion
    }
}
