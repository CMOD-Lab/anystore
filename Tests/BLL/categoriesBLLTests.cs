using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AnyStore.BLL.Tests
{
    /// <summary>
    /// Comprehensive unit tests for the categoriesBLL class.
    /// Tests cover property assignment, default values, validation logic, and edge cases.
    /// </summary>
    public class categoriesBLLTests
    {
        #region Constructor Tests

        [Fact]
        public void CategoriesBLL_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            var category = new categoriesBLL();

            // Assert
            Assert.NotNull(category);
        }

        [Fact]
        public void CategoriesBLL_DefaultConstructor_IdDefaultsToZero()
        {
            // Arrange & Act
            var category = new categoriesBLL();

            // Assert
            Assert.Equal(0, category.id);
        }

        [Fact]
        public void CategoriesBLL_DefaultConstructor_AddedByDefaultsToZero()
        {
            // Arrange & Act
            var category = new categoriesBLL();

            // Assert
            Assert.Equal(0, category.added_by);
        }

        #endregion

        #region Property Assignment Tests

        [Fact]
        public void CategoriesBLL_SetId_ReturnsCorrectValue()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.id = 7;

            // Assert
            Assert.Equal(7, category.id);
        }

        [Fact]
        public void CategoriesBLL_SetTitle_ReturnsCorrectValue()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.title = "Electronics";

            // Assert
            Assert.Equal("Electronics", category.title);
        }

        [Fact]
        public void CategoriesBLL_SetDescription_ReturnsCorrectValue()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.description = "Electronic devices and accessories";

            // Assert
            Assert.Equal("Electronic devices and accessories", category.description);
        }

        [Fact]
        public void CategoriesBLL_SetAddedDate_ReturnsCorrectValue()
        {
            // Arrange
            var category = new categoriesBLL();
            var expectedDate = new DateTime(2024, 2, 14);

            // Act
            category.added_date = expectedDate;

            // Assert
            Assert.Equal(expectedDate, category.added_date);
        }

        [Fact]
        public void CategoriesBLL_SetAddedBy_ReturnsCorrectValue()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.added_by = 4;

            // Assert
            Assert.Equal(4, category.added_by);
        }

        #endregion

        #region Full Object Initialization Tests

        [Fact]
        public void CategoriesBLL_FullInitialization_AllPropertiesSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 9, 1);

            // Act
            var category = new categoriesBLL
            {
                id = 3,
                title = "Clothing",
                description = "Apparel and fashion items",
                added_date = expectedDate,
                added_by = 1
            };

            // Assert
            Assert.Equal(3, category.id);
            Assert.Equal("Clothing", category.title);
            Assert.Equal("Apparel and fashion items", category.description);
            Assert.Equal(expectedDate, category.added_date);
            Assert.Equal(1, category.added_by);
        }

        [Fact]
        public void CategoriesBLL_MultipleInstances_AreIndependent()
        {
            // Arrange & Act
            var cat1 = new categoriesBLL { id = 1, title = "Electronics" };
            var cat2 = new categoriesBLL { id = 2, title = "Hardware" };

            // Assert
            Assert.NotEqual(cat1.id, cat2.id);
            Assert.NotEqual(cat1.title, cat2.title);
        }

        #endregion

        #region Validation Logic Tests

        [Fact]
        public void CategoriesBLL_Title_NotNullOrEmpty_IsValid()
        {
            // Arrange
            var category = new categoriesBLL { title = "Valid Category" };

            // Assert
            Assert.False(string.IsNullOrEmpty(category.title));
        }

        [Fact]
        public void CategoriesBLL_Id_PositiveValue_IsValidForUpdate()
        {
            // Arrange
            var category = new categoriesBLL { id = 2 };

            // Assert
            Assert.True(category.id > 0);
        }

        [Fact]
        public void CategoriesBLL_Id_PositiveValue_IsValidForDelete()
        {
            // Arrange
            var category = new categoriesBLL { id = 5 };

            // Assert
            Assert.True(category.id > 0);
        }

        [Fact]
        public void CategoriesBLL_AddedBy_PositiveValue_IsValid()
        {
            // Arrange
            var category = new categoriesBLL { added_by = 1 };

            // Assert
            Assert.True(category.added_by > 0);
        }

        [Fact]
        public void CategoriesBLL_InsertPrep_AllRequiredFieldsPopulated()
        {
            // Arrange
            var category = new categoriesBLL
            {
                title = "New Category",
                description = "Category Description",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.False(string.IsNullOrEmpty(category.title));
            Assert.True(category.added_by > 0);
        }

        [Fact]
        public void CategoriesBLL_UpdatePrep_HasIdAndTitle()
        {
            // Arrange
            var category = new categoriesBLL
            {
                id = 3,
                title = "Updated Category",
                description = "Updated Description",
                added_date = DateTime.Now,
                added_by = 1
            };

            // Assert
            Assert.True(category.id > 0);
            Assert.False(string.IsNullOrEmpty(category.title));
        }

        #endregion

        #region Edge Case Tests

        [Fact]
        public void CategoriesBLL_SetTitle_EmptyString_IsAllowed()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.title = string.Empty;

            // Assert
            Assert.Equal(string.Empty, category.title);
        }

        [Fact]
        public void CategoriesBLL_SetDescription_EmptyString_IsAllowed()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.description = string.Empty;

            // Assert
            Assert.Equal(string.Empty, category.description);
        }

        [Fact]
        public void CategoriesBLL_SetId_MaxInt_IsAllowed()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.id = int.MaxValue;

            // Assert
            Assert.Equal(int.MaxValue, category.id);
        }

        [Fact]
        public void CategoriesBLL_SetTitle_WithSpecialChars_IsAllowed()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.title = "Category & Sub-Category (2024)";

            // Assert
            Assert.Equal("Category & Sub-Category (2024)", category.title);
        }

        [Fact]
        public void CategoriesBLL_SetAddedDate_MinValue_IsAllowed()
        {
            // Arrange
            var category = new categoriesBLL();

            // Act
            category.added_date = DateTime.MinValue;

            // Assert
            Assert.Equal(DateTime.MinValue, category.added_date);
        }

        [Fact]
        public void CategoriesBLL_SetDescription_LongText_IsAllowed()
        {
            // Arrange
            var category = new categoriesBLL();
            var longDesc = new string('d', 1000);

            // Act
            category.description = longDesc;

            // Assert
            Assert.Equal(1000, category.description.Length);
        }

        #endregion

        #region Collection Tests

        [Fact]
        public void CategoriesBLL_ListOfCategories_CanBeCreated()
        {
            // Arrange & Act
            var categories = new List<categoriesBLL>
            {
                new categoriesBLL { id = 1, title = "Electronics" },
                new categoriesBLL { id = 2, title = "Hardware" },
                new categoriesBLL { id = 3, title = "Clothing" },
                new categoriesBLL { id = 4, title = "Food" }
            };

            // Assert
            Assert.Equal(4, categories.Count);
        }

        [Fact]
        public void CategoriesBLL_FindByTitle_ReturnsCorrectCategory()
        {
            // Arrange
            var categories = new List<categoriesBLL>
            {
                new categoriesBLL { id = 1, title = "Electronics" },
                new categoriesBLL { id = 2, title = "Hardware" },
                new categoriesBLL { id = 3, title = "Clothing" }
            };

            // Act
            var found = categories.Find(c => c.title == "Hardware");

            // Assert
            Assert.NotNull(found);
            Assert.Equal(2, found.id);
        }

        [Fact]
        public void CategoriesBLL_FindById_ReturnsCorrectCategory()
        {
            // Arrange
            var categories = new List<categoriesBLL>
            {
                new categoriesBLL { id = 1, title = "Electronics" },
                new categoriesBLL { id = 2, title = "Hardware" },
                new categoriesBLL { id = 3, title = "Clothing" }
            };

            // Act
            var found = categories.Find(c => c.id == 3);

            // Assert
            Assert.NotNull(found);
            Assert.Equal("Clothing", found.title);
        }

        [Fact]
        public void CategoriesBLL_SortByTitle_AlphabeticalOrder_IsCorrect()
        {
            // Arrange
            var categories = new List<categoriesBLL>
            {
                new categoriesBLL { id = 1, title = "Clothing" },
                new categoriesBLL { id = 2, title = "Electronics" },
                new categoriesBLL { id = 3, title = "Appliances" }
            };

            // Act
            var sorted = categories.OrderBy(c => c.title).ToList();

            // Assert
            Assert.Equal("Appliances", sorted[0].title);
            Assert.Equal("Clothing", sorted[1].title);
            Assert.Equal("Electronics", sorted[2].title);
        }

        [Fact]
        public void CategoriesBLL_EmptyList_HasZeroCount()
        {
            // Arrange & Act
            var categories = new List<categoriesBLL>();

            // Assert
            Assert.Empty(categories);
        }

        #endregion
    }
}
