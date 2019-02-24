using FluentAssertions;
using KataPotter.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace KataPotter.Domain.Tests
{
    public class ShoppingCartPriceShould
    {
        const decimal UNIT_PRICE = 8m;

        [Test]
        public void BeZeroForNoBooks()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(0));
        }

        [Test]
        [TestCase("111")]
        [TestCase("222")]
        [TestCase("333")]
        [TestCase("444")]
        [TestCase("555")]
        public void BeUnitPriceForOneBook(string ISBNCode)
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book(ISBNCode));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE));
        }

        [Test]
        [TestCase("111")]
        [TestCase("222")]
        [TestCase("333")]
        [TestCase("444")]
        [TestCase("555")]
        public void BeRegularForShoppingCartWithTwoSameBooks(string ISBNCode)
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book(ISBNCode));
            shoppingCart.AddItem(new Book(ISBNCode));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(UNIT_PRICE * 2));
        }

        [Test]
        [TestCase("111", "222")]
        [TestCase("111", "333")]
        [TestCase("111", "444")]
        [TestCase("111", "555")]
        [TestCase("222", "111")]
        [TestCase("222", "333")]
        [TestCase("222", "444")]
        [TestCase("222", "555")]
        [TestCase("333", "111")]
        [TestCase("333", "222")]
        [TestCase("333", "444")]
        [TestCase("333", "555")]
        [TestCase("444", "111")]
        [TestCase("444", "222")]
        [TestCase("444", "333")]
        [TestCase("444", "555")]
        [TestCase("555", "111")]
        [TestCase("555", "222")]
        [TestCase("555", "333")]
        [TestCase("555", "444")]
        public void BeDiscountedForShoppingCartWithTwoDifferentBooks(string ISBNCode1, string ISBNCode2)
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book(ISBNCode1));
            shoppingCart.AddItem(new Book(ISBNCode2));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 2) * 0.95m));
        }

        [Test]
        [TestCase("111", "222", "333")]
        [TestCase("222", "333", "444")]
        [TestCase("222", "444", "555")]
        public void BeDiscountedForShoppingCartWithThreeDifferentBooks(string ISBNCode1, string ISBNCode2, string ISBNCode3)
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book(ISBNCode1));
            shoppingCart.AddItem(new Book(ISBNCode2));
            shoppingCart.AddItem(new Book(ISBNCode3));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 3) * 0.90m));
        }

        [Test]
        [TestCase("111", "222", "333", "444")]
        [TestCase("222", "333", "444", "555")]
        [TestCase("222", "444", "555", "111")]
        public void BeDiscountedForShoppingCartWithFourDifferentBooks(string ISBNCode1, string ISBNCode2, string ISBNCode3, string ISBNCode4)
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book(ISBNCode1));
            shoppingCart.AddItem(new Book(ISBNCode2));
            shoppingCart.AddItem(new Book(ISBNCode3));
            shoppingCart.AddItem(new Book(ISBNCode4));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 4) * 0.80m));
        }

        [Test]
        [TestCase("111", "222", "333", "444", "555")]
        [TestCase("555", "444", "333", "222", "111")]
        public void BeDiscountedForShoppingCartWithFiveDifferentBooks(string ISBNCode1, string ISBNCode2, string ISBNCode3, string ISBNCode4, string ISBNCode5)
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            shoppingCart.AddItem(new Book(ISBNCode1));
            shoppingCart.AddItem(new Book(ISBNCode2));
            shoppingCart.AddItem(new Book(ISBNCode3));
            shoppingCart.AddItem(new Book(ISBNCode4));
            shoppingCart.AddItem(new Book(ISBNCode5));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money((UNIT_PRICE * 5) * 0.75m));
        }

        [Test]
        [Ignore("Hay que conseguirlo")]
        public void BeBestDiscountedForShoppingCartWithEdgeCase001()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            // 2 del 1
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));

            // 2 del 2
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));

            // 2 del 3
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));

            //1 del 4
            shoppingCart.AddItem(new Book("444"));

            //1 del 5
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(2 * (UNIT_PRICE * 4 * 0.8m)));
            //resultPrice.Should().Be(new Money((UNIT_PRICE * 5 * 0.75m) + (8m * 3 * 0.9m)));
        }

        [Test]
        [Ignore("Hay que conseguirlo")]
        public void BeBestDiscountedForShoppingCartWithEdgeCase002()
        {
            var potterClassifier = new PotterClassifier();
            var shoppingCart = new ShoppingCart(new List<IClassifier> { potterClassifier });

            // 5 del 1
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));
            shoppingCart.AddItem(new Book("111"));

            // 5 del 2
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));
            shoppingCart.AddItem(new Book("222"));

            // 4 del 3
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));
            shoppingCart.AddItem(new Book("333"));

            // 5 del 4
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));
            shoppingCart.AddItem(new Book("444"));

            // 4 del 5
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));
            shoppingCart.AddItem(new Book("555"));

            var resultPrice = shoppingCart.GetCartTotalPrice();

            resultPrice.Should().Be(new Money(3 * (UNIT_PRICE * 5 * 0.75m) + 2 * (8m * 4 * 0.8m)));
            //resultPrice.Should().Be(new Money(4 * (UNIT_PRICE * 5 * 0.75m) + 1 * (8m * 3 * 0.9m)));
        }
    }
}